using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public class ManualControlSoft : ASoftware
    {

        public override Vector3 CalculateAim(Vector3 gunPosition, float projectileSpeed)
        {
            if (Input.GetButton("Fire1"))
            {
                ReadyToFire = true;
            }
            else
            {
                ReadyToFire = false;
            }
            return InputManager.GetMousePosIn3D(new GameObject());
        }

        public override void Init(Vector3 turPosition)
        {
            
        }
    }
}
