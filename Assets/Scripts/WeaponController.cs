using Assets.Scripts.Abstract;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public class WeaponController : MonoBehaviour
    {
        public ARangedWeapon _weapon;
        public AEnergeticWeapon _Eweapon;

        private void Update()
        {
            if (Input.GetButton("Fire1"))
            {
                _Eweapon.Fire();
            }
        }



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