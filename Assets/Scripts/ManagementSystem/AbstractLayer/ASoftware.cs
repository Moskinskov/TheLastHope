﻿/// Limerence Games
/// The Last Hope
/// Curator: Ilya Moskinskov
/// Author: Nikolay Pankrakhin
/// to be commented

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;

namespace TheLastHope.Weapons.Software
{
    public abstract class ASoftware : MonoBehaviour
    {
        #region Parametrs
        //Тег, который присвоен объектам, являющимися потенциальными целям для турели
        [SerializeField] internal string enemyTag = "Enemy";
        //Радиус обзора турели, объект попавший в этот радиус будет атакован
        [SerializeField] internal float visionRadius = 10;
        //Турель будет самостоятельно выбирать ближайшую цель для атаки,
        //поиск цели будет осуществлен с помощью корутина 1 раз в searchTimeDelay секунд
        [SerializeField] internal float searchTimeDelay = 1f;
		[SerializeField] internal bool canBeManual = false;
		//Текущая цель турели
		internal Transform target;
        //Предыдущее положение цели
        private Vector3 previousTargetPosition;
        //Скорость цели
        internal Vector3 targetSpeed;
        //возможные состояния турели
        internal bool readyToFire;
        //Точка, по которой будет стрелять турель в случае обнаружения цели, по умолчанию это сама цель
        internal Vector3 targetingPosition = new Vector3(0, 0, 0);
        internal Transform turrelPosition;
        private float projectileSpeed;

        public bool ReadyToFire { get => readyToFire; set => readyToFire = value; }
        public Transform Target { get => target; set => target = value; }
        #endregion


        public virtual void SwitchMode()
        {
            canBeManual = !canBeManual;
        }
        public abstract void Init();
        //Считает точку, на которую должна навестись турель
        public abstract Vector3 CalculateAim(Transform turPosition);

        public abstract void FindClosestTarget(SceneData sceneData);
    }
}
