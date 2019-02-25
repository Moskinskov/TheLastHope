using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Data;

namespace TheLastHope.Weapons
{
    public class ATurretController : MonoBehaviour
    {
        //Оружие турели
        [SerializeField] protected ARangedWeapon weapon;
        //Установленный на турель софт
        [SerializeField] public ASoftware soft;
        //Скорость поворота турели к цели
        [SerializeField] float turningAngularSpeed;

        //Вращает турель в сторону точки mousePosition
        public void TurnTurret(SceneData sceneData,float deltaTime)
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

        public virtual void Start()
        {
        }

       
        public virtual void TurUpdate(SceneData sceneData, float deltaTime)
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