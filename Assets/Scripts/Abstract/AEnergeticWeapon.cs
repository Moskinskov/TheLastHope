using TheLastHope.Helpers;
using UnityEngine;

namespace Assets.Scripts.Abstract
{
    public abstract class AEnergeticWeapon : MonoBehaviour
    {
        protected float _energyCapacity;                  //maximum charge
        protected float _energyPerSecond;                 //energy being consumed per second
        protected float _damagePerSecond;                 //amount of damage being applied every second
        protected float _recoveryPerSecond;               //time needed for the full recovery (use Timer)
        protected float _currentCharge;                   //current level of charge
        protected bool _impulse = true;                   //if true, firing is available only when _currentCharge is equal to _energyCapacity
        protected Transform _muzzle;                      //child transform that casts the fire
        protected float _minActiveEnergy;
        protected GameObject _laserEffect;

        protected AudioSource AudioSource { get; set; }
        protected Timer _timerEndOfFire = new Timer();
        protected LineRenderer _origLR;

        private bool _usingLaser;
        private bool _isLoadEnergy = true;

        private void Start()
        {
            _currentCharge = _energyCapacity;
        }

        private void Update()
        {
            _timerEndOfFire.Update();
            AllChecks();
            Laser();
        }

        #region Weapon Methods

        public void Fire()
        {
            if (!_isLoadEnergy) return;
            _usingLaser = true;
            _timerEndOfFire.Start(0.05f);
        }
        protected void Reload()
        {

        }

        #endregion

        #region SomeFunctions

        /// <summary>
        /// Постоянно отрисовывающийся лазер
        /// </summary>
        private void Laser()
        {
            if (!_usingLaser)
            {
                SetLRToTarget(_muzzle.position);
            }
            else
            {
                if (Physics.Raycast(_muzzle.position, _muzzle.forward, out RaycastHit hit))
                {
                    SetLRToTarget(hit.point);

                    if (hit.transform.GetComponent<AEnemy>())
                    {

                        GameObject tempObject = Instantiate(_laserEffect, hit.point, Quaternion.Euler(hit.normal));
                        Destroy(tempObject, 1);

                        hit.transform.GetComponent<AEnemy>().SetDamage(_damagePerSecond * Time.deltaTime);
                        _currentCharge -= _energyPerSecond * Time.deltaTime;
                    }
                }
            }
        }

        /// <summary>
        /// Направляем луч(LR) в нужное место
        /// </summary>
        /// <param name="target">цель луча</param>
        private void SetLRToTarget(Vector3 target)
        {
            _origLR.SetPosition(0, _muzzle.position);
            _origLR.SetPosition(1, target);
        }
        /// <summary>
        /// Необходимые проверки
        /// </summary>
        private void AllChecks()
        {
            if (_timerEndOfFire.IsEvent())
            {
                _usingLaser = false;
            }
            if (!_usingLaser)
            {
                _currentCharge += _energyPerSecond * Time.deltaTime;
            }

            if (_currentCharge < 0)
            {
                _currentCharge = 0;
                _usingLaser = false;
                _isLoadEnergy = false;
            }

            if (_currentCharge > _energyCapacity)
                _currentCharge = _energyCapacity;

            if (!_isLoadEnergy && _currentCharge >= _minActiveEnergy)
                _isLoadEnergy = true;
        }

        #endregion

    }
}