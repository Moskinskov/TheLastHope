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

        [Header("Weapon's property"), SerializeField] protected AmmoType ammoType;
        [SerializeField] protected float damage;
        [SerializeField] protected float ammoPerShot;
        [SerializeField] protected float clipSize;
        [SerializeField] protected float reloadTime;
        [SerializeField] protected ParticleSystem damageEffect;
        [SerializeField] protected ParticleSystem fireEffect;
        [SerializeField] protected Transform muzzle;

        protected WeaponState weaponState;
        protected AudioSource audioSource;
        protected Timer delay = new Timer();
        protected float currentAmmoInClip;
        protected Animator animator;




        #endregion

        #region Public properties
        /// <summary>
        /// Type of ammunitions that the weapon utilies.
        /// </summary>
        public AmmoType AmmoType { get { return ammoType; } }
        /// <summary>
        /// The state of the weapon.
        /// </summary>
        /// <seealso cref="Data.WeaponState">
        /// Enum that enumerates the weapon states.
        /// </seealso>
        public WeaponState WeaponState { get { return weaponState; } }

        /// <summary>
        /// The general object state. 
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// The weapon's clip capacity. 
        /// </summary>
        public float ClipSize { get { return clipSize; } set { clipSize = value; } }
        /// <summary>
        /// Max range of weapon
        /// </summary>
        public float MaxRange { get; set; }
        /// <summary>
        /// Current ammo amount in weapon
        /// </summary>
        public float CurrentAmmoInClip { get { return currentAmmoInClip; } }

        #endregion

        #region Abstract methods
        protected abstract void WeaponMethod(RaycastHit hit);
        #endregion

        #region Virual methods
        /// <summary>
        /// Core weapon's 'Init'
        /// </summary>
        public virtual void Init()
        {
            IsActive = true;
            weaponState = WeaponState.ReadyToFire;
            audioSource = GetComponent<AudioSource>();
            currentAmmoInClip = clipSize;
            delay = new Timer();
            animator = GetComponentInParent<Animator>();
            damageEffect?.gameObject.SetActive(true);
            fireEffect?.gameObject.SetActive(true);
        }
        /// <summary>
        /// Core weapon 'Update'
        /// </summary>
        public virtual void WeaponUpdate()
        {
            Checks();
        }

        #endregion
        /// <summary>
        /// 'Reload' this weapon
        /// </summary>
        /// <param name="ammoQuantity"></param>
        public void Reload(int ammoQuantity)
        {
            if (WeaponState != WeaponState.Empty)
                return;
            print(ammoQuantity);
            currentAmmoInClip = ammoQuantity;
            delay.Start(reloadTime);
        }
        /// <summary>
        /// Core weapon's 'Fire'
        /// </summary>
        /// <param name="sceneData"></param>
        public virtual void Fire(SceneData sceneData)
        {
            if (WeaponState != WeaponState.ReadyToFire)
                return;

            weaponState = WeaponState.Firing;

            if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit))
            {
                if (hit.distance <= MaxRange)
                {
                    if (hit.transform.GetComponent<AEnemy>())
                    {
                        WeaponMethod(hit);
                    }
                }
            }
        }
        /// <summary>
        /// Core weapon checks
        /// </summary>
        protected virtual void Checks()
        {
            if (!IsActive)
            {
                audioSource?.Stop();
                damageEffect?.Stop();
                return;
            }

            delay.TimerUpdate();
            if (CurrentAmmoInClip > 0 && delay.Elapsed < 0)
                weaponState = WeaponState.ReadyToFire;

            if (CurrentAmmoInClip <= 0)
            {
                currentAmmoInClip = 0;
                weaponState = WeaponState.Empty;
            }

            if (WeaponState != WeaponState.Firing)
            {
                damageEffect?.Stop();
                fireEffect?.Stop();
                audioSource?.Stop();
                animator?.SetTrigger("Idle");
            }
            if (WeaponState == WeaponState.Empty)
            {
                animator?.SetTrigger("Reload");
            }
        }
        /// <summary>
        /// Core OnFire effects
        /// </summary>
        protected virtual void Effects()
        {
            damageEffect?.Play();
            fireEffect?.Play();
            audioSource?.Play();
            animator?.SetTrigger("Fire");
        }
    }
}

