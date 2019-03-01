﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management;

namespace TheLastHope.Weapons.Software
{
    public class ManualControlSoft : ASoftware
    {

        public override Vector3 CalculateAim(Transform turPosition)
        {
            ReadyToFire = Input.GetButton("Fire1");
            return InputManager.GetMousePosIn3D();
        }

        public override void FindClosestTarget(SceneData sceneData)
        {           
            //Ближайшая цель, попавшая в радиус обзора
            Transform closest = null;
            //Квадрат радиуса обзора, это значение потребуется при поиске ближайшей цели
            float distance = visionRadius;
            foreach (GameObject go in sceneData.Enemies)
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
            target = closest;
        }

        public override void Init()
        {
            canBeManual = false;
            readyToFire = false;
        }
    }
}
