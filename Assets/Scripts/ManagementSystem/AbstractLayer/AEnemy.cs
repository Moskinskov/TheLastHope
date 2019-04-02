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
		internal Transform target;        //Object to which enemy must shoot
		internal Transform targetPosition; //Place where enemy must be.
        [SerializeField] internal float driftingRadius;
		[SerializeField] internal float maxHealth;
		internal Vector3 currentDriftingPoint;

        public abstract void EnemyUpdate(SceneData sceneData, float deltaTime);
        public abstract void Die();
    }
}

