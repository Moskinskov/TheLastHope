using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Data;

public abstract class AEnemy : BaseObject
{
    [SerializeField] internal float maxSpeed;
    internal Vector3 currentSpeed;
    [SerializeField] internal float maxAcceleration;
    internal Vector3 currentAcceleration;
    [SerializeField] internal GameObject target;        //Object to which enemy must shoot
    [SerializeField] internal GameObject targetPosition; //Place where enemy must be.
    [SerializeField] internal float driftingRadius;
    internal Vector3 currentDriftingPoint;

    public abstract void SetTargetPosition(GameObject targetPosition);
    public abstract void Move(SceneData sceneData, float deltaTime);
	public abstract void Die();
}
