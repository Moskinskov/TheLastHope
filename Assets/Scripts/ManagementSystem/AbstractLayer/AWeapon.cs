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
        [SerializeField, Header("Weapon's property")] protected AmmoType typeOfAmmo;
        /// <summary>
        /// The quantity of ammo available to shoot.
        /// </summary>
        [SerializeField] protected int currentAmmoInClip;
        /// <summary>
        /// The weapon's clip capacity. 
        /// </summary>
        [SerializeField] protected int clipSize;
        /// <summary>
        /// Time of weapon's reload
        /// </summary>
        [SerializeField] protected float reloadTime;

        /// <summary>
        /// 'timer'
        /// </summary>
        protected Timer delay = new Timer();

        #endregion

        #region Public properties
        /// <summary>
        /// Type of ammunitions that the weapon utilies.
        /// </summary>
        public AmmoType TypeOfAmmo { get { return typeOfAmmo; } set { typeOfAmmo = value; } }
        /// <summary>
        /// The state of the weapon (e.g. Active, Inactive, Empty, etc.).
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
        public int ClipSize { get { return clipSize; } set { clipSize = value; } }
        /// <summary>
        /// The quantity of ammo available to shoot.
        /// </summary>
        public float CurrentAmmoInClip { get { return currentAmmoInClip; } set { currentAmmoInClip = (int)value; } }
        #endregion

        #region Abstract methods
        public abstract void Fire(SceneData sceneData);
        public abstract void Reload(int ammoQuantity);
        public abstract void WeaponUpdate();
        public abstract void Init();
        #endregion
    }
}

