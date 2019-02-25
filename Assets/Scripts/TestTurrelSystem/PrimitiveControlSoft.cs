using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public class PrimitiveControlSoft : ASoftware
    {
        public override Vector3 CalculateAim(Transform turPosition)
        {
            return base.CalculateAim(turPosition);
        }

        public override void Init(float projectileSpeed)
        {
            base.Init(projectileSpeed);
        }

        protected override IEnumerator FindClosestTarget()
        {
            return base.FindClosestTarget();
        }
    }
}
