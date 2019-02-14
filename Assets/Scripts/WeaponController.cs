using TheLastHope.Helpers;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public class WeaponController : MonoBehaviour
    {
        public BasicAutoWeapon _weapon;
       
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _weapon._isActive)
            {
                _weapon.Fire();
            }
            if (Input.GetKeyDown(KeyCode.R))
                _weapon.Reload();
        }
    }
}