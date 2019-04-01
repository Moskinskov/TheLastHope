using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management;
using System.Collections.Generic;
using TheLastHope.Weapons.Software;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Weapons
{
    /// <summary>
    /// Class that manages weapons.
    /// </summary>
	public class WeaponManager : MonoBehaviour
	{
		[SerializeField] Selector selector;
		[SerializeField] ATurret[] turretList;

		public ATurret[] TurretList { get => turretList; set => turretList = value; }

		public void Init()
		{
            turretList = FindObjectsOfType<ATurret>();
			foreach (var turret in TurretList)
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
            //if (Input.GetButtonDown("Fire2") &&
            //    selectedTurret.GetComponentInChildren<ATurret>() != null)
            //{
            //    selectedTurret.GetComponentInChildren<ATurret>().SwitchMode();
            //}
            foreach (var turret in TurretList)
			{
				if(turret.IsActive) turret.TurUpdate(sceneData, deltaTime);
			}
        }
	}
}