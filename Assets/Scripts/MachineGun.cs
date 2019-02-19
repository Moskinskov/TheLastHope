using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Weapons;
using TheLastHope.Data;

namespace TheLastHope.Weapons
{
	public sealed class MachineGun : ARangedWeapon
	{

		private Light _flash;

		private void Start()
		{
			_flash = _muzzle.GetComponent<Light>();
		}


		public override void Fire()
		{
			if (_delay._elapsed == -1)
			{
				_readyToFire = true;
			}
			if (_readyToFire)
			{
				Shot(_ammo);
				_flash.enabled = true;
			}
			else _flash.enabled = false;
		}

		private void Shot(AAmmo ammo)
		{
			if (ammo)
			{
				AAmmo _bullet = Instantiate(ammo, _muzzle.position, _muzzle.rotation);
				var _bulletRigidBody = _bullet.GetComponent<Rigidbody>();
				_bulletRigidBody.AddForce(_muzzle.forward * _force);
				_delay.Start(_rateOfFire);
				_readyToFire = false;
				var snd = this.GetComponent<AudioSource>();
				snd.Play();
			}
		}


		public override void Reload(int bulletsInClip)
		{
			throw new System.NotImplementedException();
		}

		public override void SwitchFiringMode()
		{
			throw new System.NotImplementedException();
		}
	}
}
