/// Limerence Games
/// The Last Hope
/// Curator: Ilya Moskinskov
/// Author: Nikolay Pankrakhin

using TheLastHope.Management;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Weapons
{
    /// <summary>
    /// Basic Turret - class
    /// </summary>
    public class BasicTurret : ATurret
    {
        #region Serialazed variables

        [SerializeField] private float maxHealth;

        #endregion

        #region Private variables

        private Vector3 aimingPoint;
        private ParticleSystem effect;

        #endregion

        #region Override methods

        /// <summary>
        /// BasicTurret 'Init'
        /// </summary>
        /// <param name="sceneData"></param>
        public override void Init(SceneData sceneData)
        {

        }
        /// <summary>
        /// BasicTurret 'Init'
        /// </summary>
        public override void Init()
        {
            IsActive = true;
            Weapon.Init();
            soft.Init();
            MaxHealth = maxHealth;
            Health = MaxHealth;
            effect = GetComponent<ParticleSystem>();
            if (effect) effect.Stop();
        }
        /// <summary>
        /// Turret is turning
        /// </summary>
        /// <param name="deltaTime"></param>
        public override void TurnTurret(float deltaTime)
        {
            float eulerTargetRot = Quaternion.FromToRotation(transform.forward,
                            aimingPoint - transform.position).eulerAngles.y;
            //print("rot: " + eulerTargetRot);
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
        /// <summary>
        /// BasicTurret 'Update'
        /// </summary>
        /// <param name="sceneData"></param>
        /// <param name="deltaTime"></param>
        public override void TurUpdate(SceneData sceneData, float deltaTime)
        {
            Weapon.WeaponUpdate();
            //Проверяем включен ли ручной режим на турели и возможен ли он при установленном софте
            if (manualMode && soft.canBeManual)
            {
                //Смотрим куда показывает мышка
                aimingPoint = InputManager.GetMousePosIn3D();
                if (Input.GetButton("Fire1"))
                {
                    Weapon.Fire(sceneData);
                }
            }
            else
            {
                //Рассчитываем точку для стрельбы
                soft.FindClosestTarget(sceneData);
                aimingPoint = soft.CalculateAim(transform);
                //Если навелись на цель стреляем
                if (soft.ReadyToFire)
                {
                    Weapon.Fire(sceneData);
                }
            }
            TurnTurret(Time.deltaTime);

            if (Health <= 0) Die();
        }
        /// <summary>
        /// BasicTurret 'SetDamage'
        /// </summary>
        /// <param name="damage"></param>
        public override void SetDamage(float damage)
        {
            Health -= damage;
        }

        #endregion

        /// <summary>
        /// BasicTurret 'Die' - method
        /// </summary>
        public void Die()
        {
            if (effect) effect.Play();
            IsActive = false;
            Weapon.IsActive = false;

            gameObject.tag = "Untagged";
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