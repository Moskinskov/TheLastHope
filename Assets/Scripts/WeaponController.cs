using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts
{
    public class WeaponController : MonoBehaviour
    {
        public AWeapon _weapon;
       
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