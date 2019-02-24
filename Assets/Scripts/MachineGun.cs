using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Weapons;
using TheLastHope.Data;

namespace TheLastHope.Weapons
{
	public sealed class MachineGun : ARangedWeapon
	{
        /// <summary>
        ///  Класс MachineGun
        ///  реализует подобие стрельбы из крупнокалиберного пулемета. Основной принцип большая
        ///  скорострельность, большой объем магазина. 
        ///  Shot - толкает все пули по направлению _muzzle.forward
        /// </summary>
        public override void Shot()
        {
            AAmmo _bullet = Instantiate(_ammo, _muzzle.position, _muzzle.rotation);
            var _bulletRigidBody = _bullet.GetComponent<Rigidbody>();
            _bulletRigidBody.AddForce(_muzzle.forward * _force);
        }

        public override void SwitchFiringMode()
		{
			throw new System.NotImplementedException();
		}
	}
}
