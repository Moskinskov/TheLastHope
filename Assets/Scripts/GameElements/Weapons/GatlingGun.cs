using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
/// Limerence Games
/// The Last Hope
/// Curator: Ilya Moskinskov
using UnityEngine;

namespace TheLastHope.Weapons
{
    /// <summary>
    /// 'ARangedWeapon' - class. For gatling gun
    /// </summary>
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

            AAmmo _bullet = Instantiate(ammoPrefab, Muzzle.position, Muzzle.rotation);
            sceneData.Ammos.Add(_bullet.gameObject);
            _bullet.StartPoint = new Vector3(Muzzle.position.x, Muzzle.position.y, Muzzle.position.z);
            var _bulletRigidBody = _bullet.GetComponent<Rigidbody>();
            _bulletRigidBody.AddForce(Muzzle.forward * Force);
            if (barrel) barrel.transform.Rotate(0, 0, -30);
        }

        public override void Init()
        {
            IsActive = true;
            State = WeaponState.ReadyToFire;
            TypeOfAmmo = AmmoType.M792HEI_T;
        }
    }
}
