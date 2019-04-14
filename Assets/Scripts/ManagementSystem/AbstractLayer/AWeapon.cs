/// Limerence Games
/// The Last Hope
/// Curator: Ilya Moskinskov

using TheLastHope.Helpers;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Management.AbstractLayer
{
    /// <summary>
    /// Abstract weapon class.
    /// </summary>
    /// <remarks>
    /// Use this class to create new types of weapons (e.g. projectiles, ranged, etc.).
    /// </remarks>
    public abstract class AWeapon : MonoBehaviour
    {
        #region Serializables
        /// <summary>
        /// Type of ammunitions that the weapon utilies.
        /// </summary>
        [Header("Weapon's property")] protected AmmoType ammo;
        /// <summary>
        /// The quantity of ammo available to shoot.
        /// </summary>
        [SerializeField] protected int currentAmmoInClip;
        /// <summary>
        /// The weapon's clip capacity. 
        /// </summary>
        //[SerializeField]
        protected int clipSize;
        /// <summary>
        /// Time of weapon's reload
        /// </summary>
        [SerializeField] protected float reloadTime;

        /// <summary>
        /// 'timer'
        /// </summary>
        protected Timer delay = new Timer();

        [SerializeField] protected AudioSource audioSource;

        #endregion

        #region Public properties
        /// <summary>
        /// Type of ammunitions that the weapon utilies.
        /// </summary>
        public AmmoType Ammo { get { return ammo; } set { ammo = value; } }
        /// <summary>
        /// The state of the weapon.
        /// </summary>
        /// <seealso cref="WeaponState">
        /// Enum that enumerates the weapon states.
        /// </seealso>
        public WeaponState State { get; set; }

        /// <summary>
        /// The general object state. 
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// The weapon's clip capacity. 
        /// </summary>
        public int ClipSize { get { return clipSize; } set { clipSize = value; } }   //УЗНАТЬ про CLIPSIZE
        /// <summary>
        /// The quantity of ammo available to shoot.
        /// </summary>
        public float CurrentAmmoInClip { get { return currentAmmoInClip; } set { currentAmmoInClip = (int)value; } }

        public AudioSource WeaponAudioSource
        {
            get { return audioSource; }
            set { audioSource = value; }
        }

        #endregion

        #region Abstract methods
        public abstract void Init();
        public abstract void WeaponUpdate();
        public abstract void Fire(SceneData sceneData);
        #endregion

        public void Reload(int ammoQuantity)
        {
            if (State != WeaponState.Empty)
                return;
            print(ammoQuantity);
            CurrentAmmoInClip = ammoQuantity;
            delay.Start(reloadTime);
        }

        protected virtual void Checks()
        {
            if (!IsActive)
                return;

            delay.TimerUpdate();
            if (CurrentAmmoInClip > 0 && delay.Elapsed < 0)
                State = WeaponState.ReadyToFire;

            if (CurrentAmmoInClip <= 0)
            {
                CurrentAmmoInClip = 0;
                State = WeaponState.Empty;
            }

            if (State == WeaponState.Firing && delay.Elapsed < 0)
                State = WeaponState.ReadyToFire;
        }
    }
}

