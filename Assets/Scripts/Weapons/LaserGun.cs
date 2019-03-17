using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public class LaserGun : AEnergeticWeapon
    {
        [SerializeField, Header("Necessary resources")]
        private Transform _muzzle;
        [SerializeField]
        private AudioSource _laserAudioSource;
        [SerializeField]
        private LineRenderer _laserLineRenderer;
        [SerializeField]
        private ParticleSystem _laserEffect;

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

        private void Awake()
        {
            _coreDamagePerSecond = _damagePerSecond;
            _coreEnergyCapacity = _energyCapacity;
            _coreEnergyPerSecond = _energyPerSecond;
            _coreRecoveryPerSecond = _recoveryPerSecond;
            _coreMinActiveEnergy = _minActiveEnergy;
            _origLR = _laserLineRenderer;

            if (!_laserEffect.isStopped)
                _laserEffect.Stop();
        }

        private void Update()
        {
            _timerEndOfFire.Update();
            CoreChecks();
            LocalChecks();
        }

        public override void Fire(SceneData sceneData)
        {
            if (!_isLoadEnergy) return;
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
                    HitTheEnemy(hit);
                    SetLRToTarget(hit);

                    _laserEffect.transform.SetPositionAndRotation(hit.point, Quaternion.Euler(hit.normal));

                    if (!_laserEffect.isPlaying)
                        _laserEffect.Play();
                    if (!_laserAudioSource.isPlaying)
                        _laserAudioSource.Play();
                }
            }
        }

        protected override void SetLRToTarget(RaycastHit hit)
        {
            _origLR.enabled = true;
            _origLR.SetPosition(0, _muzzle.position);
            _origLR.SetPosition(1, new Vector3(hit.transform.position.x, hit.transform.position.y + 3, hit.transform.position.z));
        }

        protected void HitTheEnemy(RaycastHit hit)
        {
            hit.transform.GetComponent<AEnemy>().SetDamage(_coreDamagePerSecond * Time.deltaTime);
            _coreCurrentCharge -= _coreEnergyPerSecond * Time.deltaTime;
        }

        protected override void LocalChecks()
        {
            if (!_usingLaser)
            {
                if (!_laserEffect.isStopped)
                    _laserEffect.Stop();
                if (_laserAudioSource.isPlaying)
                    _laserAudioSource.Stop();
            }
        }
    }
}

#region 111

//[Header("Required components"), Tooltip("Muzzle. Laser emits from here.")]
//[SerializeField]
//private Transform _LGMuzzle;
//[Tooltip("Laser sound")]
//[SerializeField]
//private AudioSource LGAudioSource;
//[Tooltip("Laser line")]
//[SerializeField]
//private LineRenderer _LGLineRenderer;
//[Tooltip("Damage effect#1")]
//[SerializeField]
//private ParticleSystem _LGLaserEffect1;

//[Header("Laser damage"), Space(10)]
//[Tooltip("Damage set by laser")]
//[SerializeField]
//private float _LGDamagePerSecond;

//[Header("Laser energy capacity"), Space(10)]
//[Tooltip("The overall laser capacity")]
//[SerializeField]
//private float _LGEnergyCapacity;
//[Tooltip("Energy being consumed per second")]
//[SerializeField]
//private float _LGEnergyPerSecond;
//[Tooltip("Energy being recoveried every second")]
//[SerializeField]
//private float _LGRecoveryPerSecond;
//[Tooltip("Minimal amount of energy that allows to shoot")]
//[SerializeField]
//private float _LGMinActiveEnergy;


//private void Awake()
//{
//_energyCapacity = _LGEnergyCapacity;
//_coreEnergyPerSecond = _LGEnergyPerSecond;
//_coreDamagePerSecond = _LGDamagePerSecond;
//_coreRecoveryPerSecond = _LGRecoveryPerSecond;
//_muzzle = _LGMuzzle;
//_origLR = _LGLineRenderer;
//_minActiveEnergy = _LGMinActiveEnergy;
//_laserEffect1 = _LGLaserEffect1;
//}

#endregion