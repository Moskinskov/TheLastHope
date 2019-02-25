using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Data;

namespace TheLastHope.Weapons
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
        //Текущая цель турели
        public Transform target;
        //Предыдущее положение цели
        private Vector3 previousTargetPosition;
        //Скорость цели
        internal Vector3 targetSpeed;
        //возможные состояния турели
        internal bool readyToFire;
        //Точка, по которой будет стрелять турель в случае обнаружения цели, по умолчанию это сама цель
        internal Vector3 targetingPosition = new Vector3(0, 0, 0);
        internal float sqrVisionRadius;
        internal Transform turrelPosition;
        private float projectileSpeed;
        internal bool manualMode = false;

        public bool ReadyToFire { get => readyToFire; set => readyToFire = value; }
        public Transform Target { get => target; set => target = value; }
        #endregion


        public virtual void SwitchMode()
        {
            manualMode = !manualMode;
        }
        public virtual void Init(float projectileSpeed)
        {
            //радиус обзора в квадрате, используется в FindClosestTarget;
            this.projectileSpeed = projectileSpeed;
            sqrVisionRadius = visionRadius * visionRadius;
            // //запускаем поиск цели
            // StartCoroutine(FindClosestTarget());
        }
        //Считает точку, на которую должна навестись турель
        public abstract Vector3 CalculateAim(SceneData sceneData, Transform turPosition);

        public abstract void FindClosestTarget(SceneData sceneData);


        //Ищет цель с тэгом enemyTag с переодичностью searchTimeDelay
        // protected virtual IEnumerator FindClosestTarget()
        // {
        //     while (true) //DEATHCODE
        //     {
        //         //Ближайшая цель, попавшая в радиус обзора
        //         Transform closest = null;
        //         GameObject[] targets = GameObject.FindGameObjectsWithTag(enemyTag);
        //         //Квадрат радиуса обзора, это значение потребуется при поиске ближайшей цели
        //         float distance = sqrVisionRadius;
        //         foreach (GameObject go in targets)
        //         {
        //             //Находим расстояние между турелью и предполагаемой целью
        //             Vector3 diff = go.transform.position - transform.position;
        //             //С точки зрения производительности быстрее сравнить квадраты расстояний,
        //             //чем делать лишнюю операцию извлечения квадратного корня
        //             float curDistance = diff.sqrMagnitude;
        //             //если найдена цель в радиусе поражения, то запоминаем её
        //             if (curDistance < distance)
        //             {
        //                 closest = go.transform;
        //                 distance = curDistance;
        //             }
        //         }
        //         target = closest;
        //         //Следующий поиск цели будет осуществлен через searchTimeDelay секунд
        //         yield return new WaitForSeconds(searchTimeDelay);
        //     }
        //}
    }
}
