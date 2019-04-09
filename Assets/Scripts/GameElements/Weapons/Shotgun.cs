/// Limerence Games
/// The Last Hope
/// Curator: Ilya Moskinskov

using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Weapons
{
    /// <summary>
    /// 'ARangedWeapon' - class.
    /// </summary>
    public class Shotgun : ARangedWeapon
    {
        #region Serialized variables

        [SerializeField] protected int ammoPerShot = 5;//Количество дроби
        [SerializeField] protected float angleScatter;

        #endregion

        /// <summary>
        ///  Shotgun class
        ///  Реализует подобие стрельбы из дробовика выпуская одновременно _ammoPerShot патронов
        ///  Shot толкает следующий патрон на i * dirDeviation правее и левее
        ///  В свою очередь dirDeviation считается как перепендикулярный вектор к _muzzle.forward
        /// </summary>
        public override void Shot(SceneData sceneData)
        {
            //Направление перпендикулярное
            Vector3 dirDeviation = new Vector3(Muzzle.forward.z,
                0,
                Muzzle.forward.x);
            dirDeviation /= (10 * angleScatter);
            for (int i = 0; i < ammoPerShot; i++)
            {
                AAmmo _bullet = Instantiate(ammoPrefab, Muzzle.position, Muzzle.rotation);
                var _bulletRigidBody = _bullet.GetComponent<Rigidbody>();
                _bulletRigidBody.AddForce((Muzzle.forward + i * dirDeviation) * Force);
                _bullet = Instantiate(ammoPrefab, Muzzle.position, Muzzle.rotation);
                _bulletRigidBody = _bullet.GetComponent<Rigidbody>();
                _bulletRigidBody.AddForce((Muzzle.forward - i * dirDeviation) * Force);
            }
        }
        public override void Init()
        {
            IsActive = true;
            State = WeaponState.ReadyToFire;
            typeOfAmmo = AmmoType.Shotgun;
        }
    }
}
