/// Limerence Games
/// The Last Hope
/// Curator: Ilya Moskinskov

using TheLastHope.Helpers;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Management.AbstractLayer
{
    /// <summary>
    ///  ARangedWeapon class
    ///  realizes basic bahaviour of the ranged weapon
    ///  Fire() calls Shot() method that is to be overriden in every 
    /// </summary>
    public abstract class ARangedWeapon : AWeapon //change to BaseObject when it is ready
    {
        #region Serializables
        [SerializeField] protected float rateOfFire = 1.0f;
        [SerializeField] private float force = 30.0f;

        [SerializeField, Header("Necessary objects")]
        protected AAmmo ammoPrefab;
        [SerializeField] private Transform muzzle; // Система координат для вылета пули
        [SerializeField] protected GameObject barrel;
        [SerializeField] protected GameObject muzzleFlash;

        #endregion

        #region Public Properties
        public float Force { get { return force; } set { force = value; } }
        public Transform Muzzle { get { return muzzle; } set { muzzle = value; } }
        #endregion

        #region Protected Variables
        protected Timer delay = new Timer();
        #endregion

        #region Abstract Functions
        /// <summary>
        /// Shooting logic
        /// </summary>
        /// <param name="sceneData"></param>
        public abstract void Shot(SceneData sceneData);
        #endregion

        #region Override methods

        /// <summary>
        /// ARanged 'Fire'
        /// </summary>
        /// <param name="sceneData"></param>
        public override void Fire(SceneData sceneData)
        {
            if (State != WeaponState.ReadyToFire || ammoPrefab == null)
                return;

            if (muzzleFlash) muzzleFlash.SetActive(true);
            Shot(sceneData);
            if (WeaponAudioSource) WeaponAudioSource.Play();
            delay.Start(rateOfFire);
            currentAmmoInClip--;
            State = WeaponState.Firing;
        }

        /// <summary>
        /// ARanged 'Update'
        /// </summary>
        public override void WeaponUpdate()
        {
            Checks();
            if (muzzleFlash) muzzleFlash.SetActive(false);
        }

        #endregion
    }
}
