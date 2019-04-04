/// Limerence Games
/// The Last Hope
/// Curator: Ilya Mosckinskov
/// Author: Danny Kotov

/// DO WE STILL NEED THIS?

using UnityEngine;

public class ManualTurretPlatform : MonoBehaviour
{
    #region Serialized variables

    [SerializeField] private float turningAngularSpeed;
    [SerializeField] private float accuracy;

    #endregion
    /// <summary>
    /// Turret is turning
    /// </summary>
    /// <param name="mousePosition"></param>
    /// <param name="deltaTime"></param>
    public void TurnTurret(Vector3 mousePosition, float deltaTime)
    {
        float eulerTargetRot = Quaternion.FromToRotation(transform.forward, mousePosition - transform.position).eulerAngles.y;
        //print("rot: " + eulerTargetRot );
        float turningDir = 1;
        if (Mathf.Abs(eulerTargetRot) > 180)
            turningDir *= -1;
        if (Mathf.Abs(eulerTargetRot) < turningAngularSpeed * deltaTime)
        {
            gameObject.transform.rotation *= Quaternion.AngleAxis(eulerTargetRot * deltaTime, Vector3.up);
        }
        else
        {
            gameObject.transform.rotation *= Quaternion.AngleAxis(turningAngularSpeed * turningDir * deltaTime, Vector3.up);
        }
    }
}
