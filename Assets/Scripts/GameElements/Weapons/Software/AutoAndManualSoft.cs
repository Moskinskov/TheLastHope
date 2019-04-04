/// Limerence Games
/// The Last Hope
/// Curator: Ilya Moskinskov
/// Author: Nikolay Pankrakhin
/// to be commented

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management;

namespace TheLastHope.Weapons.Software
{
    public class AutoAndManualSoft : ASoftware
    {
        public override Vector3 CalculateAim(Transform turPosition)
        {
            //По умолчанию турель стреляет прямо по цели, но, если цель движется, то нужно высчитать точку,
            //которая находится перед движущейся целью и по которой будет стрелять турель.
            //То есть турель должна стрелять на опережение
            if (target != null)
            {
                targetingPosition = target.position;
                turrelPosition = turPosition;
                float eulerTargetRot = Quaternion.FromToRotation(turrelPosition.forward,
                                target.position - turrelPosition.position).eulerAngles.y;
                //Проверяем навелись мы или нет
                if (Mathf.Abs(eulerTargetRot) > 355 || Mathf.Abs(eulerTargetRot) < 5)
                {
                    readyToFire = true;
                }
                else
                {
                    readyToFire = false;
                }
            }
            else
            {
                readyToFire = false;
            }

            return targetingPosition;
        }


        public override void FindClosestTarget(SceneData sceneData)
        {           
            //Ближайшая цель, попавшая в радиус обзора
            Transform closest = null;
            //Квадрат радиуса обзора, это значение потребуется при поиске ближайшей цели
            float distance = visionRadius * visionRadius;
            foreach (GameObject go in sceneData.Enemies)
            {
				if (go != null)
				{
					//Находим расстояние между турелью и предполагаемой целью
					Vector3 diff = go.transform.position - this.gameObject.transform.position;
					//С точки зрения производительности быстрее сравнить квадраты расстояний,
					//чем делать лишнюю операцию извлечения квадратного корня
					float curDistance = diff.sqrMagnitude;
					//если найдена цель в радиусе поражения, то запоминаем её
					if (curDistance < distance)
					{
						closest = go.transform;
						distance = curDistance;
					}
				}
            }
            target = closest;
        }

        public override void Init()
        {
            //canBeManual = true;
            readyToFire = false;
        }
    }
}

