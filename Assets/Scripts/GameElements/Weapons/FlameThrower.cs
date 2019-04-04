/// Limerence Games
/// The Last Hope
/// Curator: Ilya Moskinskov
/// to be commented

using System.Collections;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public class FlameThrower : AEnergeticWeapon
    {
        [SerializeField]
        private float _damagePerSecond;

        [SerializeField, Header("Necessary objects")]
        private ParticleSystem _flameRenderer;

        private float _partCountInit = 0;
        private IEnumerator coroutine;
        private bool _isPlaying;

        public override void Init()
        {
            damagePerSecond = _damagePerSecond;
            _flameRenderer.emissionRate = 0;
            //_flameRenderer.Play();

            if (!_flameRenderer.isStopped)
                _flameRenderer.Stop();
            if (!effect.isStopped)
                effect.Stop();
            if (audioSource.isPlaying)
                audioSource.Stop();

            IsActive = true;
        }

        public override void WeaponUpdate()
        {
            delay.TimerUpdate();
            CoreChecks();
            LocalChecks();
            if (IsActive == false) effect.Stop();
        }

        public override void Fire(SceneData sceneData)
        {
            delay.Start(0.005f);
            if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit))
            {
                if (hit.distance <= maxRange && IsActive == true)
                {
                    WeaponMethod(hit);
                    FireUp(240);
                }

                else FireUp(0);
            }
        }

        private void FireUp(float particleCount)
        {
            if (particleCount != _partCountInit)
            {
                _flameRenderer.Play();
                _flameRenderer.emissionRate = Mathf.Lerp(_partCountInit, particleCount, Time.deltaTime);
                _partCountInit = _flameRenderer.emissionRate;
            }

            if (particleCount == 0)
            {
                _flameRenderer.emissionRate = Mathf.Lerp(_partCountInit, particleCount, Time.deltaTime);
                _partCountInit = _flameRenderer.emissionRate;
                //if (_partCountInit == 0) _flameRenderer.Stop();
            }
        }

        protected override void WeaponMethod(RaycastHit hit)
        {
            if (hit.transform.GetComponent<AEnemy>())
            {
                if (hit.distance <= maxRange)
                {
                    HitTheEnemy(hit);
                    effect.transform.SetPositionAndRotation(hit.point, Quaternion.Euler(hit.normal));
                    coroutine = Effect(2.0f, hit);

                    if (!_isPlaying)
                    {
                        _isPlaying = true;
                        StartCoroutine(coroutine);
                    }
                }
            }
        }

        protected void HitTheEnemy(RaycastHit hit)
        {
            hit.transform.GetComponent<AEnemy>().SetDamage(damagePerSecond * Time.deltaTime);
            CurrentAmmoInClip -= energyPerSecond * Time.deltaTime;
        }

        protected override void LocalChecks()
        {

        }

        private IEnumerator Effect(float waitTime, RaycastHit hit)
        {
            effect.transform.SetPositionAndRotation(hit.point, Quaternion.Euler(hit.normal));
            if (!effect.isPlaying)
                effect.Play();
            if (!audioSource.isPlaying)
                audioSource.Play();

            yield return new WaitForSeconds(audioSource.clip.length);

            _isPlaying = false;
        }
    }
}