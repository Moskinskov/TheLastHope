﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Weapons
{
	public sealed class GatlingGun : ARangedWeapon
	{
		/// <summary>
		///  Класс MachineGun
		///  реализует подобие стрельбы из крупнокалиберного пулемета. Основной принцип большая
		///  скорострельность, большой объем магазина. 
		///  Shot - толкает все пули по направлению _muzzle.forward
		/// </summary>
        public override void Shot(SceneData sceneData)
        {
			
            AAmmo _bullet = Instantiate(_ammo, Muzzle.position, Muzzle.rotation);
			sceneData.Ammos.Add(_bullet.gameObject);
			_bullet.StartPoint = new Vector3(Muzzle.position.x, Muzzle.position.y, Muzzle.position.z);
            var _bulletRigidBody = _bullet.GetComponent<Rigidbody>();
            _bulletRigidBody.AddForce(Muzzle.forward * Force);
			if (_barrel) _barrel.transform.Rotate(0,0,-30);
		}

        public override void SwitchFiringMode()
		{
			throw new System.NotImplementedException();
		}


	}
}
