/// Limerence Games
/// The Last Hope
/// Curator: Ilya Moskinskov

using System.Collections.Generic;
using TheLastHope.Management;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Weapons
{
    /// <summary>
    /// Class that manages weapons.
    /// </summary>
	public class WeaponManager : MonoBehaviour
    {
        #region Serialized variables

        [SerializeField] private Selector selector;
        [SerializeField] private ATurret[] turretArray;

        #endregion

        #region Private variables

        private ATurret turretToReload;
        private AAmmoContainer containerToReload;
        private List<AAmmoContainer> ammoContainers = new List<AAmmoContainer>();

        #endregion
        public ATurret[] TurretList { get => turretArray; set => turretArray = value; }

        /// <summary>
        /// WeaponManager's 'Init'
        /// </summary>
        /// <param name="sceneData"></param>
        public void Init(SceneData sceneData)
        {
            turretArray = FindObjectsOfType<ATurret>();
            foreach (var turret in TurretList)
            {
                turret.Init();
            }
            ammoContainers.Add(FindObjectOfType<AAmmoContainer>());
            foreach (var container in ammoContainers)
            {
                if (container) container.Init();
            }
            print($"ContainerCount {ammoContainers.Count}");

        }
        /// <summary>
        /// WeaponManager's 'Update'
        /// </summary>
        /// <param name="sceneData"></param>
        /// <param name="deltaTime"></param>
        public void UpdateWeapons(SceneData sceneData, float deltaTime)
        {
            foreach (var turret in turretArray)
            {
                GameObject selectedObj = null;
                if (Input.GetMouseButtonDown(0))
                {
                    selectedObj = selector.GetSelectedGameObject();
                    print($"selected obj = {selectedObj.name}");
                }
                if (selectedObj && selectedObj.GetComponentInChildren<ATurret>() &&
                    selectedObj.GetComponentInChildren<ATurret>().Weapon.State == WeaponState.Empty)
                {
                    turretToReload = selectedObj.GetComponentInChildren<ATurret>();
                    print($"TURRET SELECTED {selectedObj.name}");
                }
                else if (selectedObj && selectedObj.GetComponentInChildren<AAmmoContainer>())
                {
                    containerToReload = selectedObj.GetComponentInChildren<AAmmoContainer>();
                    print("CONTAINER SELECTED");

                }

                if (containerToReload && turretToReload)
                {
                    print($"RELOADING {turret.gameObject.name}");
                    if (ammoContainers.ToArray()[0].GetAmmo(turret.Weapon.TypeOfAmmo, turret.Weapon.ClipSize))
                    {
                        turret.Weapon.Reload(turret.Weapon.ClipSize);
                        print("R1");
                    }
                    else
                    {
                        print("R2");
                        ammoContainers.ToArray()[0].Ammo.TryGetValue(turret.Weapon.TypeOfAmmo, out int ammoToReload);
                        turret.Weapon.Reload(ammoToReload);
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
                if (turret.IsActive) turret.TurUpdate(sceneData, deltaTime);
            }
        }
    }
}