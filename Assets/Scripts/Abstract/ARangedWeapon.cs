using System.Collections;
using System.Collections.Generic;
using TheLastHope.Helpers;
using TheLastHope.Data;
using UnityEngine;

namespace TheLastHope.Weapons
{
	public abstract class ARangedWeapon : MonoBehaviour //change to BaseObject when it is ready
	{
		#region Serializables
		[SerializeField] protected float _rateOfFire = 1.0f; //firing speed
		[SerializeField] protected float _shootingRange = 30.0f; //firing range
		[SerializeField] protected float _reloadTime = 1.0f; //reload time
		[SerializeField] protected float _force = 30.0f; //power of fire
        [SerializeField] protected int _clipSize = 5; //clip size
        [SerializeField] protected bool _canChangeFiringMode = false; //ability to change firing mode
		[SerializeField] protected FiringMode _currentFiringMode; //current active firing mode
		[SerializeField] protected int[] _firingModesAvailable; //available firing modes
		[SerializeField] protected AAmmo _ammo; //ammunition to fire
		[SerializeField] protected Transform _muzzle;
		#endregion

		#region Protected Variables
		protected Timer _delay = new Timer();                       //Timer
		protected bool _readyToFire = true;                         //Ready to shoot flag
		protected int _ammoInClip;									//Current ammo in clip
        protected int _currentAmmoInClip;
        protected AudioSource _audioPlayer;
        #endregion

        #region Abstract Functions
        public abstract void Shot(); //Логика выстрела             
		public abstract void SwitchFiringMode();
        #endregion
        public void Fire() 
        //Функция вызывает комманду выстрела проверяя наличие 
        //патронов и учитывая скорость стрельбы
        {
            Debug.Log("Fire!");
            if (_delay._elapsed == -1)
            {
                _readyToFire = true;
            }
            if (_currentAmmoInClip == 0)
            {
                _readyToFire = false;
                _delay.Start(_reloadTime);
                _currentAmmoInClip = _clipSize;
            }
            if (_readyToFire && _ammo)
            {
                Shot();
                var snd = this.GetComponent<AudioSource>();
                snd.Play();
                _delay.Start(_rateOfFire);
                _readyToFire = false;
                _currentAmmoInClip -= 1;
            }
                

        }
        public AudioSource AudioPlayer
		{
			get { return _audioPlayer; }
			set { _audioPlayer = value; }
		}
        protected virtual void Start()
        {
            _currentAmmoInClip =  _clipSize;
            
        }
		protected virtual void Update()
		{
			_delay.Update();
            
        }
	}
}
