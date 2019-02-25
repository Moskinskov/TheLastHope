using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public class PrimitiveControlSoft : ASoftware
    {
        public override Vector3 CalculateAim(Vector3 gunPosition, float projectileSpeed)
        {
            Debug.Log(base.CalculateAim(gunPosition, projectileSpeed));
            return base.CalculateAim(gunPosition, projectileSpeed);
        }

        public override void Init(Vector3 turPosition)
        {
            base.Init(turPosition);
        }

        protected override IEnumerator FindClosestTarget()
        {
            return base.FindClosestTarget();
        }
    }
}
