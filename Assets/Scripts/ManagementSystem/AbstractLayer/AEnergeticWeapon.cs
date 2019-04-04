/// Limerence Games
/// The Last Hope
/// Curator: Ilya Moskinskov
/// to be commented

using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Management.AbstractLayer
{
    public abstract class AEnergeticWeapon : AWeapon
    {

        [SerializeField]
        protected float maxRange;
        [SerializeField]
        protected float damagePerSecond;
        [SerializeField]
        protected float energyPerSecond;
        [SerializeField, Header("Necessary objects")]
        protected Transform muzzle;
        [SerializeField]
        protected AudioSource audioSource; //to do
        [SerializeField]
        protected ParticleSystem effect;

        public override void Init()
        {
            if (!effect.isStopped)
                effect.Stop();

            TypeOfAmmo = AmmoType.Energy;
            State = WeaponState.Active;
        }

        #region Abstract Methods

        protected abstract void WeaponMethod(RaycastHit hit);
        protected abstract void LocalChecks();

        #endregion

        /// <summary>
        /// It should be set to Update
        /// </summary>
        /// 
        protected virtual void CoreChecks()
        {
            if (delay.Finished())
            {
                State = WeaponState.Active;
            }

            if (CurrentAmmoInClip <= 0)
            {
                CurrentAmmoInClip = 0;
                State = WeaponState.empty;
            }

            if (State == WeaponState.empty && CurrentAmmoInClip > 0)
                State = WeaponState.Inactive;
        }
        /// <summary>
        /// AEnergetic 'Reload'
        /// </summary>
        /// <param name="ammoQuantity">ammo value</param>
        public override void Reload(int ammoQuantity)
        {
            CurrentAmmoInClip = ammoQuantity;
            State = WeaponState.Inactive;
            delay.Start(reloadTime);
        }
    }
}