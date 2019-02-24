using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public class Shotgun : ARangedWeapon
    {
        [SerializeField] protected int _ammoPerShot = 5;
        [SerializeField] protected float _angleScatter;
        public override void Shot()
        {
            //Направление перпендикулярное
            Vector3 dirDeviasion = new Vector3(_muzzle.forward.z,
                                   0,
                                   _muzzle.forward.x);
            dirDeviasion /= 10;
            for (int i = 0; i < _ammoPerShot; i++)
            {
                AAmmo _bullet = Instantiate(_ammo, _muzzle.position, _muzzle.rotation);
                var _bulletRigidBody = _bullet.GetComponent<Rigidbody>();
                _bulletRigidBody.AddForce((_muzzle.forward + i * dirDeviasion) * _force);
                _bullet = Instantiate(_ammo, _muzzle.position, _muzzle.rotation);
                _bulletRigidBody = _bullet.GetComponent<Rigidbody>();
                _bulletRigidBody.AddForce((_muzzle.forward - i * dirDeviasion) * _force);
            }
            
        }

        public override void SwitchFiringMode()
        {
            throw new System.NotImplementedException();
        }
    }
}
