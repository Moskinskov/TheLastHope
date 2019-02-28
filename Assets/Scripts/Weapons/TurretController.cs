using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Weapons.Software;

namespace TheLastHope.Weapons
{
    public class TurretController : ATurret
    {
        //Установленный на турель софт
        [SerializeField] ASoftware soft;

        public ASoftware Soft { get { return soft; } set { soft = value; } }

        //Вращает турель в сторону точки mousePosition
        public override void TurnTurret(SceneData sceneData,float deltaTime)
        {
            Vector3 aimingPoint = soft.CalculateAim(sceneData,transform);
            float eulerTargetRot = Quaternion.FromToRotation(transform.forward,
                            aimingPoint - transform.position).eulerAngles.y;
            //print("rot: " + eulerTargetRot);
            float turningDir = 1;
            if (Mathf.Abs(eulerTargetRot) > 180)
                turningDir *= -1;
            if (Mathf.Abs(eulerTargetRot) < turningAngularSpeed* deltaTime)
            {
                gameObject.transform.rotation *= Quaternion.AngleAxis(eulerTargetRot * deltaTime, Vector3.up);           
            }
            else
            {
                gameObject.transform.rotation *= Quaternion.AngleAxis( turningAngularSpeed * turningDir * deltaTime,
                                                                     Vector3.up);       
            }
        }
       
        public override void TurUpdate(SceneData sceneData, float deltaTime)
        {
            soft.FindClosestTarget(sceneData);
            //Каждый кадр сохраняем текущее положение цели
            TurnTurret(sceneData, Time.deltaTime);
            //Если навелись на цель стреляем
            if (soft.ReadyToFire)
            {
                weapon.Fire(sceneData);
            }
        }
        // public void Update()
        // {
        //     TurUpdate(0);
        // }
        /*
        //Для удобства - в окне редактора покажем радиус поражения турели и некоторые дополнительные данные
        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, visionRadius);
            if (state == State.Attack)
            {
                //Текущее направление пушки
                Debug.DrawRay(turretGun.position, turretGun.forward * visionRadius, Color.blue);
                //Направление от центра вращения пушки к цели, которое в итоге должна принять пушка турели
                Debug.DrawRay(turretGun.position, (targetingPosition - turretGun.position), Color.yellow);
                //Направление, в которое "смотрит" турель
                Debug.DrawRay(turretHead.position, turretHead.forward * visionRadius, Color.red);
            }
        }
        */
    }
}