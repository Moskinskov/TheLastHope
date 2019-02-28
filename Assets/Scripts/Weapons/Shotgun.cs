using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Weapons
{
    public class Shotgun : ARangedWeapon
    {
        /// <summary>
        ///  Shotgun class
        ///  Реализует подобие стрельбы из дробовика выпуская одновременно _ammoPerShot патронов
        ///  Shot толкает следующий патрон на i * dirDeviation правее и левее
        ///  В свою очередь dirDeviation считается как перепендикулярный вектор к _muzzle.forward
        /// </summary>
        [SerializeField] protected int _ammoPerShot = 5;//Количество дроби
        [SerializeField] protected float _angleScatter;
        public override void Shot(SceneData sceneData)
        {
			//Направление перпендикулярное
            Vector3 dirDeviation = new Vector3(_muzzle.forward.z,
                                   0,
                                   _muzzle.forward.x);
            dirDeviation /= 10;
            for (int i = 0; i < _ammoPerShot; i++)
            {
                AAmmo _bullet = Instantiate(_ammo, _muzzle.position, _muzzle.rotation);
                var _bulletRigidBody = _bullet.GetComponent<Rigidbody>();
                _bulletRigidBody.AddForce((_muzzle.forward + i * dirDeviation) * _force);
                _bullet = Instantiate(_ammo, _muzzle.position, _muzzle.rotation);
                _bulletRigidBody = _bullet.GetComponent<Rigidbody>();
                _bulletRigidBody.AddForce((_muzzle.forward - i * dirDeviation) * _force);
            }
            
        }

        public override void SwitchFiringMode()
        {
            throw new System.NotImplementedException();
        }
    }
}
