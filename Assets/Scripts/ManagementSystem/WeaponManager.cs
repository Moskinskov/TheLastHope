using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management;
using System.Collections.Generic;
using TheLastHope.Weapons.Software;
namespace TheLastHope.Weapons
{
	public class WeaponManager : MonoBehaviour
	{
		[SerializeField] Selector selector;
		[SerializeField] List<TurretController> listTurretControllers = new List<TurretController>();
        public void Init()
		{
			foreach (var turret in listTurretControllers)
			{
                turret.Init();
			}
		}

		/// <summary>
		/// Turns and fires turrets. Also switches manual turret.
		/// </summary>
		/// <param name="sceneData"></param>
		/// <param name="deltaTime"></param>
		public void UpdateWeapons(SceneData sceneData, float deltaTime)
		{
            //Fire2 - пкм. При нажатии пкм на турель, мы меняем режим ее стрельбы, если это нам позволяет софт
            GameObject selectedTurret = selector.GetSelectedGameObject();
            if (Input.GetButtonDown("Fire2") &&
                selectedTurret.GetComponentInChildren<TurretController>() != null)
            {
                selectedTurret.GetComponentInChildren<TurretController>().SwitchMode();
            }
            foreach (var turret in listTurretControllers)
			{
				turret.TurUpdate(sceneData, deltaTime);
			}
        }
	}
}