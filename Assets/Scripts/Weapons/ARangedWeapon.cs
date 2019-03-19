using System.Collections;
using System.Collections.Generic;
using TheLastHope.Helpers;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;
using UnityEngine;

namespace TheLastHope.Weapons
{
	/// <summary>
	///  ARangedWeapon class
	///  realizes basic bahaviour of the ranged weapon
	///  Fire() calls Shot() method that is to be overriden in every 
	/// </summary>
	public abstract class ARangedWeapon : AWeapon //change to BaseObject when it is ready
	{
		#region Serializables
		[SerializeField] protected float _rateOfFire = 1.0f; //firing speed
		[SerializeField] protected float _shootingRange = 30.0f; //firing range
		[SerializeField] protected float _reloadTime = 1.0f; //reload time
		[SerializeField] float _force = 30.0f; //power of fire
		[SerializeField] protected int _clipSize = 5; //clip size
		[SerializeField] protected bool _canChangeFiringMode = false; //ability to change firing mode
		[SerializeField] protected FiringMode _currentFiringMode; //current active firing mode
		[SerializeField] protected int[] _firingModesAvailable; //available firing modes
		[SerializeField] protected AAmmo _ammo; //ammunition to fire
		[SerializeField] Transform _muzzle; // Система координат для вылета пули
		#endregion

        public float Force { get { return _force; } set { _force = value; } }
        public Transform Muzzle { get { return _muzzle; }  set { _muzzle = value; } }

		#region Protected Variables
		protected Timer _delay = new Timer();                       //Timer
		protected bool _readyToFire = true;                         //Ready to shoot flag
		protected int _ammoInClip;                                  //Current ammo in clip
		protected int _currentAmmoInClip; //Текущие количество патронов в магазине
		protected AudioSource _audioPlayer; //Типа должно быть использовано для звуков выстрела
		#endregion

		#region Abstract Functions
		public abstract void Shot(SceneData sceneData); //Логика выстрела             
		public abstract void SwitchFiringMode(); // Возможная механика смены режима стрельбы
		#endregion
		public override void Fire(SceneData sceneData)
		//Функция вызывает комманду выстрела проверяя наличие 
		//патронов и учитывая скорость стрельбы
		{
			//_delay.Start(time) отсчитывает время time 
			//после отчета разрешает снова стрелять.
			if (_delay.Elapsed == -1)
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
				Shot(sceneData);
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
		public override void Init()
		{
			_currentAmmoInClip = _clipSize;

		}
		public override void WeaponUpdate()
		{
			_delay.Update();
		}
	}
}
