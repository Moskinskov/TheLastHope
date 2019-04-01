using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Weapons
{ 
    public class Rifle : ARangedWeapon
    {
        public override void Shot(SceneData sceneData)
        {
            /// <summary>
            ///  Класс Rifle
            ///  Реализует стрельбу из крупно калиберного вооружения. Пока ничем не отличается от
            ///  MachineGun, тк отличает от этого класса лишь звуком, размером обоймы, скоростью стрельбы
            ///  временем перезарядки и силой толкающей пулю. Эти все параметры сменяемы в классе родителе
            /// </summary>
            AAmmo _bullet = Instantiate(_ammo, Muzzle.position, Muzzle.rotation);
            var _bulletRigidBody = _bullet.GetComponent<Rigidbody>();
            _bulletRigidBody.AddForce(Muzzle.forward * Force);
        }
        public override void SwitchFiringMode()
        {
            throw new System.NotImplementedException();
        }

        public override void Init()
        {
            _currentAmmoInClip = _clipSize;
            TypeOfAmmo = AmmoType.ADM401_84mms;
            State = WeaponState.Inactive;
        }
    }
}
