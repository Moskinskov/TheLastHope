﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Weapons.Software;
using TheLastHope.Management;

namespace TheLastHope.Weapons
{
    public class BasicTurret : ATurret
    {
		[SerializeField] private float _maxHealth;
        private Vector3 aimingPoint;
		private ParticleSystem _effect;

		public override void Init(SceneData sceneData)
		{
			throw new System.NotImplementedException();
		}

		public override void Init()
		{
			base.IsActive = true;
			soft.Init();
			base.MaxHealth = _maxHealth;
			base.Health = base.MaxHealth;
			_effect = GetComponent<ParticleSystem>();
			if (_effect) _effect.Stop();
			weapon.Init();
		}


		//Вращает турель в сторону точки ффz
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
                gameObject.transform.rotation *= Quaternion.AngleAxis( turningAngularSpeed * turningDir * deltaTime, Vector3.up);       
            }
        }
       
        public override void TurUpdate(SceneData sceneData, float deltaTime)
        {
			weapon.WeaponUpdate();
            //Проверяем включен ли ручной режим на турели и возможен ли он при установленном софте
            if (manualMode && soft.canBeManual)
            {
                //Смотрим куда показывает мышка
                aimingPoint = InputManager.GetMousePosIn3D();
                if (Input.GetButton("Fire1"))
                {
                    weapon.Fire(sceneData);
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
                    weapon.Fire(sceneData);
                }
            }
            TurnTurret(Time.deltaTime);

			if (Health <= 0) Die();
		}

		public override void SetDamage(float damage)
		{
			base.Health -= damage;
			
		}

		public void Die()
		{
			if (_effect) _effect.Play();
			base.IsActive = false;
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