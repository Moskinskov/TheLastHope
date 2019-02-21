using Assets.Scripts.Abstract;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] ARangedWeapon _weapon;
        [SerializeField] AEnergeticWeapon _Eweapon;
        [SerializeField] GameObject manualTurret;

        public void UpdateWeapons(float deltaTime)
        {
            manualTurret.GetComponent<ManualTurretPlatform>().TurnTurret(InputManager.GetMousePosIn3D(manualTurret),deltaTime);
            if (Input.GetButton("Fire1"))
            {
                try
                {
                    _Eweapon.Fire();
                }
                catch { }
                try
                {
                    _weapon.Fire();
                }
                catch { }
                
            }
            //if (Input.GetKeyDown(KeyCode.R))
            //    _weapon.Reload();
        }
    }
}