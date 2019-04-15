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
        [SerializeField] private List<ATurret> turretArray;
        [SerializeField] private GameObject selectedObj;

        #endregion

        #region Private variables

        private ATurret turretToReload;
        private AAmmoContainer containerToReload;
        private List<AAmmoContainer> ammoContainers = new List<AAmmoContainer>();

        #endregion
        public List<ATurret> TurretList { get => turretArray; set => turretArray = value; }

        /// <summary>
        /// WeaponManager's 'Init'
        /// </summary>
        /// <param name="sceneData"></param>
        public void Init(SceneData sceneData)
        {
            ammoContainers.Add(FindObjectOfType<AAmmoContainer>());
            var tempArray = FindObjectsOfType<ATurret>();

            foreach (var turret in tempArray)
            {
                if (!turret.GetComponentInParent<AEnemy>())
                {
                    turret.Init();
                    turretArray.Add(turret);
                }
            }
            foreach (var container in ammoContainers)
            {
                if (container)
                container.Init();
            }

            print($"ContainerCount {ammoContainers.Count}");
            selectedObj = null;
        }



        /// <summary>
        /// WeaponManager's 'Update'
        /// </summary>
        /// <param name="sceneData"></param>
        /// <param name="deltaTime"></param>
        public void UpdateWeapons(SceneData sceneData, float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                selectedObj = selector.GetSelectedGameObject();
                print($"selected obj = {selectedObj.name}");
            }
            if ((selectedObj && selectedObj.GetComponentInChildren<ATurret>() &&
                 selectedObj.GetComponentInChildren<ATurret>().Weapon.State == WeaponState.Empty) ||
                (selectedObj && selectedObj.GetComponentInParent<ATurret>() &&
                 selectedObj.GetComponentInParent<ATurret>().Weapon.State == WeaponState.Empty))
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
                print($"RELOADING {turretToReload.gameObject.name}");

                if (containerToReload.GetAmmo(turretToReload.Weapon.Ammo, turretToReload.Weapon.ClipSize))
                {
                    turretToReload.Weapon.Reload(turretToReload.Weapon.ClipSize);
                    print("R1");
                }
                else
                {
                    print("R2");
                    containerToReload.Ammo.TryGetValue(turretToReload.Weapon.Ammo, out int ammoToReload);
                    turretToReload.Weapon.Reload(ammoToReload);
                }

                selectedObj = null;
                containerToReload = null;
                turretToReload = null;
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