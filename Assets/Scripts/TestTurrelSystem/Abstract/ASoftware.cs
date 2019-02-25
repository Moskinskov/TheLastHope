using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public abstract class ASoftware : MonoBehaviour
    {
        #region Parametrs
        //Тег, который присвоен объектам, являющимися потенциальными целям для турели
        [SerializeField] protected string enemyTag = "Enemy";
        //Радиус обзора турели, объект попавший в этот радиус будет атакован
        [SerializeField] protected float visionRadius = 10;
        //Турель будет самостоятельно выбирать ближайшую цель для атаки,
        //поиск цели будет осуществлен с помощью корутина 1 раз в searchTimeDelay секунд
        [SerializeField] protected float searchTimeDelay = 1f;
        //Текущая цель турели
        private Transform target;
        //Предыдущее положение цели
        private Vector3 previousTargetPosition;
        //Скорость цели
        protected Vector3 targetSpeed;
        //возможные состояния турели
        private bool readyToFire;
        //Точка, по которой будет стрелять турель в случае обнаружения цели, по умолчанию это сама цель
        private Vector3 targetingPosition;
        private float sqrVisionRadius;
        private Transform turrelPosition;
        private float projectileSpeed;

        public bool ReadyToFire { get => readyToFire; set => readyToFire = value; }
        public Transform Target { get => target; set => target = value; }
        #endregion

        public virtual void Init(float projectileSpeed)
        {
            //радиус обзора в квадрате, используется в FindClosestTarget;
            this.projectileSpeed = projectileSpeed;
            sqrVisionRadius = visionRadius * visionRadius;
            
            //запускаем поиск цели
            StartCoroutine(FindClosestTarget());
        }
        //Считает точку, на которую должна навестись турель
        public virtual Vector3 CalculateAim(Transform turPosition)
        {
            //По умолчанию турель стреляет прямо по цели, но, если цель движется, то нужно высчитать точку,
            //которая находится перед движущейся целью и по которой будет стрелять турель.
            //То есть турель должна стрелять на опережение
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
            return targetingPosition;
        }
        //Ищет цель с тэгом enemyTag с переодичностью searchTimeDelay
        protected virtual IEnumerator FindClosestTarget()
        {
            while (true)
            {
                //Ближайшая цель, попавшая в радиус обзора
                Transform closest = null;
                GameObject[] targets = GameObject.FindGameObjectsWithTag(enemyTag);
                //Квадрат радиуса обзора, это значение потребуется при поиске ближайшей цели
                float distance = sqrVisionRadius;
                foreach (GameObject go in targets)
                {
                    //Находим расстояние между турелью и предполагаемой целью
                    Vector3 diff = go.transform.position - transform.position;
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
                //Следующий поиск цели будет осуществлен через searchTimeDelay секунд
                yield return new WaitForSeconds(searchTimeDelay);
            }
        }
    }
}
