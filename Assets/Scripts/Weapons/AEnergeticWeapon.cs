using TheLastHope.Helpers;
using TheLastHope.Management.AbstractLayer;
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
        protected bool _usingLaser;
        protected bool _isLoadEnergy = true;

        protected Timer _timerEndOfFire = new Timer();
        protected LineRenderer _origLR;

        private void Start()
        {
            _coreCurrentCharge = _coreEnergyCapacity;
            _origLR.enabled = false;
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
                _usingLaser = false;
            }

            if (!_usingLaser)
            {
                _coreCurrentCharge += _coreEnergyPerSecond * Time.deltaTime;
                _origLR.enabled = false;
            }

            if (_coreCurrentCharge < 0)
            {
                _coreCurrentCharge = 0;
                _usingLaser = false;
                _isLoadEnergy = false;
            }

            if (_coreCurrentCharge > _coreEnergyCapacity)
                _coreCurrentCharge = _coreEnergyCapacity;

            if (!_isLoadEnergy && _coreCurrentCharge >= _coreMinActiveEnergy)
                _isLoadEnergy = true;
        }
    }
}