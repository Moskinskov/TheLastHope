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
	public class Rifle : ARangedWeapon
	{
		/// <summary>
		///  Класс Rifle
		///  Реализует стрельбу из крупно калиберного вооружения. Пока ничем не отличается от
		///  MachineGun, тк отличает от этого класса лишь звуком, размером обоймы, скоростью стрельбы
		///  временем перезарядки и силой толкающей пулю. Эти все параметры сменяемы в классе родителе
		/// </summary>
		public override void Shot(SceneData sceneData)
		{
			AAmmo _bullet = Instantiate(ammoPrefab, Muzzle.position, Muzzle.rotation);
			var _bulletRigidBody = _bullet.GetComponent<Rigidbody>();
			_bulletRigidBody.AddForce(Muzzle.forward * Force);
		}
	}
}
