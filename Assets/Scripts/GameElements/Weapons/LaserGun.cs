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
    /// 'AEnergeticWeapon' - class. 
    /// </summary>
    public class LaserGun : AEnergeticWeapon
    {
        #region Serialised variables

        [SerializeField]
        private LineRenderer LR;

        #endregion

        #region Private variables

        private IEnumerator coroutine;
        private bool _isPlaying;

        #endregion

        #region Override methods

        /// <summary>
        /// LaserGun 'Init'
        /// </summary>
        public override void Init()
        {
            IsActive = true;

            if (!effect.isStopped)
                effect.Stop();
            if (WeaponAudioSource.isPlaying)
                WeaponAudioSource.Stop();

            TypeOfAmmo = AmmoType.Energy;
            State = WeaponState.ReadyToFire;

            LR.enabled = false;
        }
        /// <summary>
        /// LaserGun 'WeaponUpdate'
        /// </summary>
        public override void WeaponUpdate()
        {
            Checks();
        }
        /// <summary>
        /// LaserGun 'Fire'
        /// </summary>
        /// <param name="sceneData"></param>
        public override void Fire(SceneData sceneData)
        {
            if (State != WeaponState.ReadyToFire)
                return;

            delay.Start(0.005f);
            if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit))
            {
                if (hit.distance <= maxRange && hit.transform.tag == "Enemy")
                {
                    WeaponMethod(hit);
                }
            }
            State = WeaponState.Firing;
        }

        protected override void WeaponMethod(RaycastHit hit)
        {
            if (hit.transform.GetComponent<AEnemy>())
            {
                if (hit.distance <= maxRange)
                {
                    HitTheEnemy(hit);
                    SetLRToTarget(hit);

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

        protected override void Checks()
        {
            base.Checks();
            if (State != WeaponState.ReadyToFire)
            {
                if (!effect.isStopped)
                    effect.Stop();
                if (audioSource.isPlaying)
                    audioSource.Stop();
            }
        }

        #endregion

        #region Private methods

        protected void SetLRToTarget(RaycastHit hit)
        {
            LR.enabled = true;
            LR.SetPosition(0, muzzle.position);
            LR.SetPosition(1, new Vector3(hit.transform.position.x, hit.transform.position.y + 3, hit.transform.position.z));
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
            if (!WeaponAudioSource.isPlaying)
                audioSource.Play();

            yield return new WaitForSeconds(audioSource.clip.length);

            _isPlaying = false;
        }

        #endregion
    }
}