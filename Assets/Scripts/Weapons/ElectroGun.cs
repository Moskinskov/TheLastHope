using System.Collections.Generic;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public class ElectroGun : AEnergeticWeapon
    {
        [SerializeField, Header("Necessary resources")]
        private Transform _muzzle;
        [SerializeField]
        private AudioSource _electroAudioSource;
        [SerializeField]
        private LineRenderer _electroLineRenderer;
        [SerializeField]
        private ParticleSystem _electroEffect;

        [SerializeField, Header("Weapon options"), Space(10)]
        private float _damagePerSecond;
        [SerializeField]
        private float _energyCapacity;
        [SerializeField]
        private float _energyPerSecond;
        [SerializeField]
        private float _recoveryPerSecond;
        [SerializeField]
        private float _minActiveEnergy;
        //------------------------------------------------------//
        [SerializeField] private float _radiusElectro;
        private List<GameObject> _allEnemies;
        private List<AEnemy> _nearestEnemies;


        private void Awake()
        {
            _coreDamagePerSecond = _damagePerSecond;
            _coreEnergyCapacity = _energyCapacity;
            _coreEnergyPerSecond = _energyPerSecond;
            _coreRecoveryPerSecond = _recoveryPerSecond;
            _coreMinActiveEnergy = _minActiveEnergy;
            _origLR = _electroLineRenderer;

            if (!_electroEffect.isStopped)
                _electroEffect.Stop();
            _nearestEnemies = new List<AEnemy>();
        }

        private void Update()
        {
            _timerEndOfFire.Update();
            CoreChecks();
            LocalChecks();
        }



        public override void Fire(SceneData sceneData)
        {
            _allEnemies = sceneData.Enemies;

            if (!_isLoadEnergy)
                return;

            _usingLaser = true;
            _timerEndOfFire.Start(0.05f);
            WeaponMethod();
        }

        protected override void WeaponMethod()
        {
            if (Physics.Raycast(_muzzle.position, _muzzle.forward, out RaycastHit hit))
            {
                if (hit.transform.GetComponent<AEnemy>())
                {
                    FindTheNearestEnemies(hit.transform.GetComponent<AEnemy>());
                    HitTheEnemies();
                    SetLRToTarget(hit);

                    _electroEffect.transform.SetPositionAndRotation(hit.point, Quaternion.Euler(hit.normal));
                    if (!_electroEffect.isPlaying)
                        _electroEffect.Play();
                    if (!_electroAudioSource.isPlaying)
                        _electroAudioSource.Play();
                }
            }
        }

        protected override void SetLRToTarget(RaycastHit hit)
        {
            var enemyPosition = hit.point;
            _origLR.enabled = true;

            _origLR.SetPosition(0, _muzzle.position);
            _origLR.SetPosition(1, enemyPosition);

            _origLR.positionCount = _nearestEnemies.Count;
            if (_nearestEnemies.Count > 0)
            {
                foreach (var enemy in _nearestEnemies)
                {
                    _origLR.positionCount++;
                    _origLR.SetPosition(_origLR.positionCount - 1, new Vector3(enemy.transform.position.x,
                                                                               enemy.transform.position.y + 3,
                                                                               enemy.transform.position.z));
                }
            }
        }

        protected void HitTheEnemies()
        {
            if (_nearestEnemies.Count > 0)
            {
                foreach (var enemy in _nearestEnemies)
                {
                    enemy.SetDamage(_coreDamagePerSecond * Time.deltaTime);
                    _coreCurrentCharge -= _coreEnergyPerSecond * Time.deltaTime;
                }
            }
        }

        private void FindTheNearestEnemies(AEnemy firstEnemy)
        {
            foreach (var enemy in _allEnemies)
            {
                var tempEnemy = enemy?.GetComponent<AEnemy>();
                var tempDistance = Vector3.Distance(enemy.transform.position, firstEnemy.transform.position); //Если эта операция будет сильно напрягать память, можно считать дистанцию вручную.

                if (_radiusElectro >= tempDistance && !_nearestEnemies.Contains(tempEnemy))
                    _nearestEnemies.Add(tempEnemy);
                if (_radiusElectro < tempDistance && _nearestEnemies.Contains(tempEnemy))
                    _nearestEnemies.Remove(tempEnemy);
            }
        }

        protected override void LocalChecks()
        {
            if (!_usingLaser)
            {
                _origLR.positionCount = 2;
                foreach (var enemy in _nearestEnemies)
                {
                    _nearestEnemies.Remove(enemy);
                }

                if (!_electroEffect.isStopped)
                    _electroEffect.Stop();
                if (_electroAudioSource.isPlaying)
                    _electroAudioSource.Stop();
            }
        }
    }
}