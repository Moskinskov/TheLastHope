using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public class ATurretController : MonoBehaviour
    {
        //Оружие турели
        [SerializeField] protected ARangedWeapon weapon;
        //Установленный на турель софт
        [SerializeField] protected ASoftware soft;
        //Скорость поворота турели к цели
        [SerializeField] float turningAngularSpeed;

        //Вращает турель в сторону точки mousePosition
        public void TurnTurret(Vector3 mousePosition, float deltaTime)
        {
            float eulerTargetRot = Quaternion.FromToRotation(transform.forward,
                            mousePosition - transform.position).eulerAngles.y;
            //print("rot: " + eulerTargetRot);
            float turningDir = 1;
            if (Mathf.Abs(eulerTargetRot) > 355 || Mathf.Abs(eulerTargetRot) < 5)
            {
                weapon.Fire();
                return;
            }
            if (Mathf.Abs(eulerTargetRot) > 180)
                turningDir *= -1;
            gameObject.transform.rotation *= Quaternion.AngleAxis(turningAngularSpeed * turningDir * deltaTime,
                                                                    Vector3.up);
        }

        public virtual void Start()
        {
            soft.Init(transform.position);
        }

       
        public virtual void TurUpdate(float deltaTime)
        {
            //Каждый кадр сохраняем текущее положение цели
            TurnTurret(soft.CalculateAim(weapon._muzzle.position, weapon._force), Time.deltaTime);
            //Если навелись на цель стреляем
            if (soft.ReadyToFire)
            {
                weapon.Fire();
            }
        }
        public void Update()
        {
            TurUpdate(0);
        }
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