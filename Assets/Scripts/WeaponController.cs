using UnityEngine;
using TheLastHope.Data;

namespace TheLastHope.Weapons
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] ARangedWeapon _weapon;
        [SerializeField] AEnergeticWeapon _Eweapon;
        [SerializeField] GameObject manualTurret;

        public void UpdateWeapons(SceneData sceneData,float deltaTime)
        {
            manualTurret.GetComponent<ManualTurretPlatform>().TurnTurret(
                            InputManager.GetMousePosIn3D(manualTurret),deltaTime);
            if (Input.GetButton("Fire1"))
            {
                try
                {
                    _Eweapon.Fire();
                }
                catch { }
                try
                {
                    _weapon.Fire(sceneData);
                }
                catch { }
            }
            //if (Input.GetKeyDown(KeyCode.R))
            //    _weapon.Reload();
        }
    }
}