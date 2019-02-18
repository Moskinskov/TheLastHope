using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Data;

public class BikeEnemy : AEnemy
{
    [SerializeField] float driftingSpeedDivider;
    /// <summary>
    /// Resets health.
    /// </summary>
    public override void Initialize()
    {
        base.health = base.maxHealth;
        //base.currentSpeed = new Vector3(base.maxSpeed/2, 0f, 0f);
        //base.currentAcceleration = new Vector3(base.maxAcceleration / 2, 0f, 0f);
        base.currentDriftingPoint = new Vector3(base.targetPosition.transform.position.x,
                                                 base.targetPosition.transform.position.y,
                                                 base.targetPosition.transform.position.z);
    }

    /// <summary>
    /// Set target position for enemy to be there.
    /// </summary>
    /// <param name="targetPosition"></param>
    public override void SetTargetPosition(GameObject targetPosition)
    {
        base.targetPosition = targetPosition;
    }

    /// <summary>
    /// Moves this enemy according it's posibilities and targets.
    /// </summary>
    /// <param name="sceneData"></param>
    /// <param name="deltaTime"></param>
    public override void Move(SceneData sceneData, float deltaTime)
    {
        //base.currentAcceleration = GetCurrentAcceleration(base.targetPosition, base.maxAcceleration);
        //base.currentSpeed = GetCurrentSpeed(base.currentSpeed, base.currentAcceleration, deltaTime);
        base.currentSpeed = GetCurrentSpeed(sceneData, base.currentSpeed, targetPosition, deltaTime);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + currentSpeed.x * deltaTime,
                                                    gameObject.transform.position.y + currentSpeed.y * deltaTime,
                                                    gameObject.transform.position.z + currentSpeed.z * deltaTime);
    }

    /// <summary>
    /// For not it do nothing.
    /// </summary>
    /// <param name="damage"></param>
    public override void SetDamage(float damage)
    {
        health -= damage;
    }

    Vector3 GetCurrentSpeed(SceneData sceneData,Vector3 currentSpeed, GameObject targetPosition, float deltaTime)
    {
        if (Mathf.Abs(targetPosition.transform.position.x - this.transform.position.x) < driftingRadius &&
            Mathf.Abs(targetPosition.transform.position.x - this.transform.position.x) < driftingRadius)
            return DriftSpeed(sceneData, deltaTime).normalized * maxSpeed/ driftingSpeedDivider;

        return new Vector3(targetPosition.transform.position.x - this.transform.position.x,
                          0,
                          targetPosition.transform.position.z - this.transform.position.z).normalized * maxSpeed ;
    }


    //Vector3 GetCurrentSpeed(Vector3 currentSpeed, Vector3 currentAcceleration, float deltaTime)
    //{

    //    return new Vector3(currentSpeed.x + currentAcceleration.x*deltaTime,
    //                        currentSpeed.y + currentAcceleration.y*deltaTime,
    //                        currentSpeed.z + currentAcceleration.z*deltaTime);
    //}

    //Vector3 GetCurrentAcceleration(GameObject targetPosition, float maxAcceleration)
    //{
    //   return new Vector3(targetPosition.transform.position.x - this.transform.position.x,
    //                      0,
    //                      targetPosition.transform.position.z - this.transform.position.z).normalized * maxAcceleration;
    //}

    Vector3 DriftSpeed(SceneData sceneData, float deltaTime)
    {
        if (Mathf.Abs(currentDriftingPoint.x - transform.position.x) < driftingRadius / 10 &&
            Mathf.Abs(currentDriftingPoint.z - transform.position.z) < driftingRadius / 10)
            currentDriftingPoint = new Vector3(Random.Range(base.targetPosition.transform.position.x - driftingRadius, 
                                                            base.targetPosition.transform.position.x + driftingRadius),
                                               0,
                                               Random.Range(base.targetPosition.transform.position.z - driftingRadius, 
                                                            base.targetPosition.transform.position.z + driftingRadius));
        return new Vector3(currentDriftingPoint.x - transform.position.x, 0, currentDriftingPoint.z - transform.position.z);
    }
}
