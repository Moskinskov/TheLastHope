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
    public class UltimateWeapon : ARangedWeapon
    {
        // Start is called before the first frame update
        /// <summary>
        ///  Класс MachineGun
        ///  реализует подобие стрельбы из крупнокалиберного пулемета. Основной принцип большая
        ///  скорострельность, большой объем магазина. 
        ///  Shot - толкает все пули по направлению _muzzle.forward
        /// </summary>
        public override void Shot(SceneData sceneData)
        {

            AAmmo _bullet = Instantiate(ammoPrefab, Muzzle.position, Muzzle.rotation);
            sceneData.Ammos.Add(_bullet.gameObject);
            _bullet.StartPoint = new Vector3(Muzzle.position.x, Muzzle.position.y, Muzzle.position.z);
            var _bulletRigidBody = _bullet.GetComponent<Rigidbody>();
            _bulletRigidBody.AddForce(Muzzle.forward * Force);

            _bullet = Instantiate(ammoPrefab, Muzzle.position, Muzzle.rotation);
            sceneData.Ammos.Add(_bullet.gameObject);
            _bullet.StartPoint = new Vector3(Muzzle.position.x, Muzzle.position.y, Muzzle.position.z);
            _bulletRigidBody = _bullet.GetComponent<Rigidbody>();
            _bulletRigidBody.AddForce(Muzzle.right * Force);

            _bullet = Instantiate(ammoPrefab, Muzzle.position, Muzzle.rotation);
            sceneData.Ammos.Add(_bullet.gameObject);
            _bullet.StartPoint = new Vector3(Muzzle.position.x, Muzzle.position.y, Muzzle.position.z);
            _bulletRigidBody = _bullet.GetComponent<Rigidbody>();
            _bulletRigidBody.AddForce(Muzzle.right * -1 * Force);

            _bullet = Instantiate(ammoPrefab, Muzzle.position, Muzzle.rotation);
            sceneData.Ammos.Add(_bullet.gameObject);
            _bullet.StartPoint = new Vector3(Muzzle.position.x, Muzzle.position.y, Muzzle.position.z);
            _bulletRigidBody = _bullet.GetComponent<Rigidbody>();
            _bulletRigidBody.AddForce(Muzzle.forward * -1 * Force);
        }

        public override void Init()
        {
            IsActive = true;
            State = WeaponState.ReadyToFire;
            typeOfAmmo = AmmoType.ADM401_84mms;
        }
    }
}