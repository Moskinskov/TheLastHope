/// Limerence Games
/// The Last Hope
/// Curator: Ilya Moskinskov

using TheLastHope.Helpers;
using TheLastHope.Management.AbstractLayer;
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
        [SerializeField] protected float reloadTime = 1.0f;
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
        protected AudioSource audioPlayer;
        #endregion

        #region Abstract Functions
        /// <summary>
        /// Shooting logic
        /// </summary>
        /// <param name="sceneData"></param>
        public abstract void Shot(SceneData sceneData);
        #endregion

        /// <summary>
        /// ARanged 'Fire'
        /// </summary>
        /// <param name="sceneData"></param>
        public override void Fire(SceneData sceneData)
        {
            if (delay.Elapsed == -1)
            {
                State = WeaponState.Active;
            }
            if (currentAmmoInClip <= 0)
            {
                State = WeaponState.empty;
            }
            if (State == WeaponState.Active && ammoPrefab)
            {
                if (muzzleFlash) muzzleFlash.SetActive(true);
                Shot(sceneData);
                var snd = GetComponent<AudioSource>();
                if (snd) snd.Play();
                delay.Start(rateOfFire);
                State = WeaponState.Inactive;
                currentAmmoInClip--;
            }
        }
        /// <summary>
        /// ARanged 'Reload'
        /// </summary>
        /// <param name="ammoQuantity"></param>
        public override void Reload(int ammoQuantity)
        {
            currentAmmoInClip = ammoQuantity;
            State = WeaponState.Inactive;
            delay.Start(reloadTime);
        }

        /// <summary>
        /// Weapon sound
        /// </summary>
        public AudioSource AudioPlayer
        {
            get { return audioPlayer; }
            set { audioPlayer = value; }
        }
        /// <summary>
        /// ARanged 'Start'
        /// </summary>
        public override void Init()
        {
            currentAmmoInClip = clipSize;
            State = WeaponState.Active;
        }
        /// <summary>
        /// ARanged 'Update'
        /// </summary>
        public override void WeaponUpdate()
        {
            if (muzzleFlash) muzzleFlash.SetActive(false);
            delay.TimerUpdate();
        }
    }
}
