using TheLastHope.Data;
using TheLastHope.Helpers;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public class WeaponController : MonoBehaviour
    {
        public ARangedWeapon _weapon;
       
        private void Update()
        {

        }

        public void UpdateWeapons(SceneData sceneData, bool lmbIsPressed)
        {
            if (lmbIsPressed)
            {
                _weapon.Fire(sceneData);
            }
            //if (Input.GetKeyDown(KeyCode.R))
            //    _weapon.Reload();
        }
    }
}