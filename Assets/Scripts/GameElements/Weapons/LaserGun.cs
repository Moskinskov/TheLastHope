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
        private bool isPlaying;

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

            Ammo = AmmoType.Energy;
            State = WeaponState.ReadyToFire;
            isPlaying = false;
            LR.enabled = false;
            ClipSize = 20;
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

            State = WeaponState.Firing;

            if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit))
            {
                if (hit.distance <= maxRange)
                {
                    if (hit.transform.GetComponentInChildren<AEnemy>())
                    {
                        WeaponMethod(hit);
                        Effects();
                    }
                }
            }
        }

        protected override void WeaponMethod(RaycastHit hit)
        {
            isPlaying = true;
            effect.transform.SetPositionAndRotation(hit.point, Quaternion.Euler(hit.normal));

            HitTheEnemy(hit);
            SetLRToTarget(hit);
        }

        protected override void Checks()
        {
            if (!IsActive)
            {
                WeaponAudioSource.Stop();
                effect.Stop();
            }

            base.Checks();
            if (State != WeaponState.Firing)
            {
                if (!effect.isStopped)
                    effect.Stop();
                if (WeaponAudioSource.isPlaying)
                    WeaponAudioSource.Stop();
                if (LR)
                    LR.enabled = false;
                isPlaying = false;
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

        /// <summary>
        /// All effects that need to play 'OnFire'
        /// </summary>
        private void Effects()
        {
            if (!isPlaying)
                return;
            effect.Play();
            audioSource.Play();
        }

        #endregion
    }
}