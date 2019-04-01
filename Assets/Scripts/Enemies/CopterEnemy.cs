using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management;
using TheLastHope.Management.Data;
using TheLastHope.Weapons;
using TheLastHope.Helpers;

namespace TheLastHope.Enemies
{
    public class CopterEnemy : AEnemy
    {
        [SerializeField] private float _driftingSpeedDivider;
        [SerializeField] private float _speedSmoother;
        [SerializeField] private ARangedWeapon _weapon;
        [SerializeField] private float _visionDistance;
		[SerializeField] private Texture _damageTex;
        private RaycastHit hit;
		private Renderer[] renderers;
		private Color _initColor;
		private Texture _initTexture;
		private Timer timer;

		/// <summary>
		/// Resets health.
		/// </summary>
		public override void Init(SceneData sceneData)
        {
			target = sceneData.TrainCars[0].gameObject.transform;
			this.targetPosition = sceneData.TrainCars[0].gameObject.transform;
			currentDriftingPoint = new Vector3(this.targetPosition.position.x,
										 this.targetPosition.position.y,
										 this.targetPosition.position.z);
			_weapon.Init();
			IsActive = true;
			MaxHealth = maxHealth;
			Health = MaxHealth;
			renderers = GetComponentsInChildren<Renderer>();
			_initTexture = renderers[2].material.mainTexture;
			Init();
		}

		public override void Init()
		{
			timer = new Timer();
		}

        /// <summary>
        /// Moves this enemy according it's posibilities and targets.
        /// </summary>
        /// <param name="sceneData"></param>
        /// <param name="deltaTime"></param>
        public override void EnemyUpdate(SceneData sceneData, float deltaTime)
        {
			if (Health < 0 && IsActive)
			{
				Die();
				sceneData.Props.Insert((sceneData.Props.Count - 2), this.gameObject);
			}
			_weapon.WeaponUpdate();
			timer.TimerUpdate();
			if (timer.IsEvent()) ChangeTex(false);
			if ((_speedSmoother != 0) || (_driftingSpeedDivider != 0))
            {
				//targetPosition = this.targetPosition;
				print(targetPosition.name);
                Vector3 speed = GetCurrentSpeed(sceneData, currentSpeed, targetPosition, deltaTime);
                currentSpeed = Vector3.Lerp(currentSpeed, speed, _speedSmoother);
                gameObject.transform.rotation *= Quaternion.FromToRotation(gameObject.transform.forward, currentSpeed);
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + currentSpeed.x * deltaTime,
                                                        gameObject.transform.position.y + currentSpeed.y * deltaTime,
                                                        gameObject.transform.position.z + currentSpeed.z * deltaTime);
            }

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * _visionDistance, Color.red);

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if ((hit.transform.tag == "Player") && (hit.distance < _visionDistance)) _weapon.Fire(sceneData);
                if ((hit.distance < _visionDistance) && (hit.transform.gameObject.tag == "Finish"))
                {
                    this.gameObject.GetComponent<AudioSource>().clip = null; // KILL ME FOR THIS!
                    Health = 0;
                    //Tell Destroyer to destroy this enemy;
                }
            }
        }

        /// <summary>
        /// For not it do nothing.
        /// </summary>
        /// <param name="damage"></param>
        public override void SetDamage(float damage)
        {
            Health -= damage;
			ChangeTex(true);
		}

        Vector3 GetCurrentSpeed(SceneData sceneData, Vector3 currentSpeed, Transform targetPosition, float deltaTime)
        {
            if (Mathf.Abs(targetPosition.position.z - transform.position.z) < driftingRadius &&
                Mathf.Abs(targetPosition.position.z - transform.position.z) < driftingRadius)
                return DriftSpeed(sceneData, deltaTime).normalized * maxSpeed / _driftingSpeedDivider;

            return new Vector3(targetPosition.position.z - transform.position.z,
                              0,
                              targetPosition.position.x - transform.position.x).normalized * maxSpeed;
        }

		private void ChangeTex(bool isDamage)
		{
			if (isDamage)
			{

				foreach (Renderer rend in renderers)
				{
					rend.material.mainTexture = _damageTex;
				}
				timer.Start(0.1f);
			}
			else
			{
				foreach (Renderer rend in renderers)
				{
					rend.material.mainTexture = _initTexture;
				}
			}
		}

        //Vector3 GetCurrentSpeed(Vector3 currentSpeed, Vector3 currentAcceleration, float deltaTime)
        //{

        //    return new Vector3(currentSpeed.x + currentAcceleration.x*deltaTime,
        //                        currentSpeed.y + currentAcceleration.y*deltaTime,
        //                        currentSpeed.z + currentAcceleration.z*deltaTime);
        //}

        //Vector3 GetCurrentAcceleration(GameObject targetPosition, float maxAcceleration)
        //{
        //   return new Vector3(targetPosition.transform.position.x - this.transform.position.x,
        //                      0,
        //                      targetPosition.transform.position.z - this.transform.position.z).normalized * maxAcceleration;
        //}

        Vector3 DriftSpeed(SceneData sceneData, float deltaTime)
        {
            if (Mathf.Abs(currentDriftingPoint.x - transform.position.x) < driftingRadius / 10 &&
                Mathf.Abs(currentDriftingPoint.z - transform.position.z) < driftingRadius / 10)
                currentDriftingPoint = new Vector3(Random.Range(targetPosition.position.x - driftingRadius,
                                                                targetPosition.position.x + driftingRadius),
                                                   0,
                                                   Random.Range(targetPosition.position.z - driftingRadius,
                                                                targetPosition.position.z + driftingRadius));
            return new Vector3(currentDriftingPoint.x - transform.position.x, 0, currentDriftingPoint.z - transform.position.z);
        }

        public override void Die()
        {
            var _explosion = this.gameObject.transform.GetChild(1);
            var _copter = this.gameObject.transform.GetChild(0);
            _copter.gameObject.SetActive(false);
            _explosion.gameObject.SetActive(true);
            this.gameObject.GetComponent<AudioSource>().Play();
			this.gameObject.GetComponent<Collider>().enabled = false;

			IsActive = false;
        }
    }
}

