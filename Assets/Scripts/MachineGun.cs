using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Weapons;
using TheLastHope.Data;

namespace TheLastHope.Weapons
{
	public sealed class MachineGun : ARangedWeapon
	{
        [SerializeField] GameObject barrel;
		public override void Fire(SceneData scene)
		{
            AAmmo ammo =  Instantiate(_ammo,barrel.transform.position, barrel.transform.rotation);
            ammo.Direction = barrel.transform.forward;
            ammo.Range = _shootingRange;
            scene.ammos.Add(ammo.gameObject);
            
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
