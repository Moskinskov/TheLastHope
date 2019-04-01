using TheLastHope.Helpers;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public abstract class AEnergeticWeapon : AWeapon
    {
        protected float _coreEnergyCapacity;                  //maximal charge
        protected float _coreEnergyPerSecond;                 //energy being consumed per second
        protected float _coreDamagePerSecond;                 //amount of damage being applied every second
        protected float _coreRecoveryPerSecond;               //time needed for the full recovery (use Timer)
        protected float _coreCurrentCharge;                   //current level of charge
        protected float _coreMinActiveEnergy;                 //min energy for using laser
        protected float _coreMaxRange;						  //maximal range of the weapon
        //----------------------------------------------------------------------------------------------------//
        //protected bool _usingLaser;
        //protected bool _isLoadEnergy = true;

        protected Timer _timerEndOfFire = new Timer();
        protected LineRenderer _origLR;
        protected float _reloadTime;

        public override void Init()
        {
            _coreCurrentCharge = _coreEnergyCapacity;
            _origLR.enabled = false;
            TypeOfAmmo = AmmoType.Energy;
            State = WeaponState.Active;
        }

        #region Abstract Methods

        protected abstract void WeaponMethod(RaycastHit hit);
        protected abstract void SetLRToTarget(RaycastHit hit);
        protected abstract void LocalChecks();

        #endregion

        /// <summary>
        /// It should be set to Update
        /// </summary>
        /// 
        protected virtual void CoreChecks()
        {
            if (_timerEndOfFire.IsEvent())
            {
                State = WeaponState.Inactive;
            }

            if (State != WeaponState.Active)
            {
                _origLR.enabled = false;
            }

            if (_coreCurrentCharge <= 0)
            {
                _coreCurrentCharge = 0;
                State = WeaponState.empty;
            }

            if (_coreCurrentCharge > _coreEnergyCapacity)
                _coreCurrentCharge = _coreEnergyCapacity;

            if (State == WeaponState.empty && _coreCurrentCharge > 0)
                State = WeaponState.Inactive;
        }
        public override void Reload(int ammoQuantity)
        {
            _coreCurrentCharge = ammoQuantity;
            State = WeaponState.Inactive;
            _timerEndOfFire.Start(_reloadTime);
        }
    }
}