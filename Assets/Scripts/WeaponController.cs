using TheLastHope.Data;
using TheLastHope.Helpers;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public class WeaponController : MonoBehaviour
    {
        public ARangedWeapon _weapon;
     
        public void UpdateWeapons(bool lmbIsPressed)
        {
            if (lmbIsPressed)
            {
                _weapon.Fire();
				
            }
            //if (Input.GetKeyDown(KeyCode.R))
            //    _weapon.Reload();
        }
    }
}