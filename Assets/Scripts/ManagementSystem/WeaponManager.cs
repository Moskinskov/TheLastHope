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
		[SerializeField] ATurret[] turretArray;
        ATurret turretToReload;
        AAmmoContainer containerToReload;
        List<AAmmoContainer> ammoContainers = new List<AAmmoContainer>();
        

		public ATurret[] TurretList { get => turretArray; set => turretArray = value; }

		public void Init(SceneData sceneData)
		{
            turretArray = FindObjectsOfType<ATurret>();
			foreach (var turret in TurretList)
			{
                turret.Init();
			}
            ammoContainers.Add(FindObjectOfType<AAmmoContainer>());
            foreach(var container in ammoContainers)
            {
                container.Init();
            }
            print($"ContainerCount {ammoContainers.Count}");
            
		}

		/// <summary>
		/// Turns and fires turrets. Also switches manual turret.
		/// </summary>
		/// <param name="sceneData"></param>
		/// <param name="deltaTime"></param>
		public void UpdateWeapons(SceneData sceneData, float deltaTime)
		{
            
            foreach(var turret in turretArray)
            {
                GameObject selectedObj = null;
                if (Input.GetMouseButtonDown(0))
                {
                    selectedObj = selector.GetSelectedGameObject();
                }
                if (selectedObj && selectedObj.GetComponentInChildren<ATurret>() &&
                    selectedObj.GetComponentInChildren<ATurret>().weapon.State ==  WeaponState.empty)
                {
                    turretToReload = selectedObj.GetComponentInChildren<ATurret>();
                    print("TURRET SELECTED");
                }
                else if (selectedObj && selectedObj.GetComponentInChildren<AAmmoContainer>())
                {
                    containerToReload = selectedObj.GetComponentInChildren<AAmmoContainer>();
                    print("CONTAINER SELECTED");

                }

                if (containerToReload && turretToReload)
                {
                    print($"RELOADING {turret.gameObject.name}");
                    if (ammoContainers.ToArray()[0].GetAmmo(turret.weapon.TypeOfAmmo, turret.weapon.ClipSize))
                    {
                        turret.weapon.Reload(turret.weapon.ClipSize);
                        print("R1");
                        turret.weapon.State = WeaponState.Active;
                    }
                    else
                    {
                        print("R2");
                        int ammoToReload = 0;
                        ammoContainers.ToArray()[0].ammo.TryGetValue(turret.weapon.TypeOfAmmo, out ammoToReload);
                        turret.weapon.Reload(ammoToReload);
                        turret.weapon.State = WeaponState.Active;
                    }
                    containerToReload = null;
                    turretToReload = null;
                }
            }

            //Fire2 - пкм. При нажатии пкм на турель, мы меняем режим ее стрельбы, если это нам позволяет софт

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