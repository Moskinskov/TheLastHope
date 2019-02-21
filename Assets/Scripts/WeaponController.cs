using TheLastHope.Data;
using TheLastHope.Helpers;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public class WeaponController : MonoBehaviour
    {
        public ARangedWeapon _weapon;
        [SerializeField] GameObject manualTurret;

        public void UpdateWeapons(bool lmbIsPressed, float deltaTime)
        {
            manualTurret.GetComponent<ManualTurretPlatform>().TurnTurret(InputManager.GetMousePosIn3D(manualTurret), deltaTime);
            if (lmbIsPressed)
            {
                _weapon.Fire();				
            }

            //if (Input.GetKeyDown(KeyCode.R))
            //    _weapon.Reload();
        }
    }
}