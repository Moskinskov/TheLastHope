using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualTurretPlatform : MonoBehaviour
{
    [SerializeField] float turningAngularSpeed;

    public void TurnTurret(Vector3 mousePosition, float deltaTime)
    {
        float eulerTargetRot = Quaternion.FromToRotation(transform.forward, mousePosition - transform.position).eulerAngles.y;
        //print("rot: " + eulerTargetRot );
        float turningDir = 1;
        if (Mathf.Abs(eulerTargetRot) > 180)
            turningDir *= -1; 
        gameObject.transform.rotation *= Quaternion.AngleAxis( turningAngularSpeed * turningDir * deltaTime, Vector3.up);
    }
}
