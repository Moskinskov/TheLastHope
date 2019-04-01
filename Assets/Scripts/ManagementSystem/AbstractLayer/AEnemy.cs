using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;

namespace TheLastHope.Management.AbstractLayer
{
    public abstract class AEnemy : ABaseObject
    {
        [SerializeField] internal float maxSpeed;
        internal Vector3 currentSpeed;
        [SerializeField] internal float maxAcceleration;
        internal Vector3 currentAcceleration;
        [SerializeField] internal GameObject target;        //Object to which enemy must shoot
        [SerializeField] internal GameObject targetPosition; //Place where enemy must be.
        [SerializeField] internal float driftingRadius;
		[SerializeField] internal float maxHealth;
		internal Vector3 currentDriftingPoint;

        public abstract void SetTargetPosition(GameObject targetPosition);
        public abstract void EnemyUpdate(SceneData sceneData, float deltaTime);
        public abstract void Die();
    }
}

