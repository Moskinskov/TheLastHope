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
		[SerializeField] protected float _clipSize = 5.0f; //clip size
		[SerializeField] protected float _force = 30.0f; //power of fire
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
		protected AudioSource _audioPlayer;
		#endregion

		#region Abstract Functions
		public abstract void Fire(SceneData sceneData);                 //Firing (mandatory to implement in inheriting classes)
		public abstract void Reload(int bulletsInClip);        //Recharging (mandatory to implement in inheriting classes)
		public abstract void SwitchFiringMode();
		#endregion

		public AudioSource AudioPlayer
		{
			get { return _audioPlayer; }
			set { _audioPlayer = value; }
		}

		protected virtual void Update()
		{
				_delay.Update();
				if (_delay.IsEvent())

				{
					_readyToFire = true;
				}
		}
	}
}
