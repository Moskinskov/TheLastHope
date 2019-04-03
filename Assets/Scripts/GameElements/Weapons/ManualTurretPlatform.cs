/// Limerence Games
/// The Last Hope
/// Curator: Ilya Mosckinskov
/// Author: Danny Kotov
/// to be commented

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualTurretPlatform : MonoBehaviour
{
    [SerializeField] float turningAngularSpeed;
    [SerializeField] float accuracy;

    public void TurnTurret(Vector3 mousePosition, float deltaTime)
    {
        float eulerTargetRot = Quaternion.FromToRotation(transform.forward, mousePosition - transform.position).eulerAngles.y;
        //print("rot: " + eulerTargetRot );
        float turningDir = 1;
        if (Mathf.Abs(eulerTargetRot) > 180)
            turningDir *= -1; 
        if (Mathf.Abs(eulerTargetRot) < turningAngularSpeed* deltaTime)
        {
            gameObject.transform.rotation *= Quaternion.AngleAxis(eulerTargetRot * deltaTime, Vector3.up);           
        }
        else
        {
            gameObject.transform.rotation *= Quaternion.AngleAxis( turningAngularSpeed * turningDir * deltaTime, Vector3.up);       
        }
    }
}
