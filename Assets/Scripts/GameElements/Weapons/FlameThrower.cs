/// Limerence Games
/// The Last Hope
/// Curator: Ilya Moskinskov

using System.Collections;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Weapons
{
    /// <summary>
    /// 'AEnergeticWeapon' - class. FlameGun
    /// </summary>
    public class FlameThrower : AEnergeticWeapon
    {
        #region Serialized variables

        [SerializeField, Header("Necessary objects")]
        private ParticleSystem flameRenderer;

        #endregion

        #region Private variables

        private float partCountInit = 0;
        private IEnumerator coroutine;
        private bool isPlaying;

        #endregion

        #region Override methods

        public override void Init()
        {
            IsActive = true;

            if (!effect.isStopped)
                effect.Stop();
            if (WeaponAudioSource.isPlaying)
                WeaponAudioSource.Stop();

            TypeOfAmmo = AmmoType.Energy;
            State = WeaponState.ReadyToFire;

            flameRenderer.emissionRate = 0;

            if (!flameRenderer.isStopped)
                flameRenderer.Stop();
            if (!effect.isStopped)
                effect.Stop();
            if (WeaponAudioSource.isPlaying)
                audioSource.Stop();
        }

        public override void WeaponUpdate()
        {
            Checks();
        }

        public override void Fire(SceneData sceneData)
        {
            delay.Start(0.005f);
            if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit))
            {
                if (hit.distance <= maxRange && IsActive)
                {
                    WeaponMethod(hit);
                    FireUp(240);
                }

                else FireUp(0);
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

                    if (!isPlaying)
                    {
                        isPlaying = true;
                        StartCoroutine(coroutine);
                    }
                }
            }
        }

        protected override void Checks()
        {
            base.Checks();
            if (!IsActive)
                effect.Stop();
			if (flameRenderer.emissionRate == 0)
				flameRenderer.Stop();
		}
        #endregion

        #region Private methods

        private void FireUp(float particleCount)
        {
            if (particleCount != partCountInit)
            {
                flameRenderer.Play();
                flameRenderer.emissionRate = Mathf.Lerp(partCountInit, particleCount, Time.deltaTime);
                partCountInit = flameRenderer.emissionRate;
            }

            if (particleCount == 0)
            {
                flameRenderer.emissionRate = Mathf.Lerp(partCountInit, particleCount, Time.deltaTime);
                partCountInit = flameRenderer.emissionRate;
            }
        }

        protected void HitTheEnemy(RaycastHit hit)
        {
            hit.transform.GetComponent<AEnemy>().SetDamage(damagePerSecond * Time.deltaTime);
            CurrentAmmoInClip -= energyPerSecond * Time.deltaTime;
        }
        private IEnumerator Effect(float waitTime, RaycastHit hit)
        {
            effect.transform.SetPositionAndRotation(hit.point, Quaternion.Euler(hit.normal));
            if (!effect.isPlaying)
                effect.Play();
            if (!audioSource.isPlaying)
                audioSource.Play();

            yield return new WaitForSeconds(audioSource.clip.length);

            isPlaying = false;
        }

        #endregion

    }
}