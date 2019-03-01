using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management;
using TheLastHope.Management.Data;
using TheLastHope.Weapons;

namespace TheLastHope.Enemies
{
    public class CopterEnemy : AEnemy
    {
        [SerializeField] private float _driftingSpeedDivider;
        [SerializeField] private float _speedSmoother;
        [SerializeField] private ARangedWeapon _weapon;
        [SerializeField] private float _visionDistance;
        private RaycastHit hit;
		private Material material;

        /// <summary>
        /// Resets health.
        /// </summary>
        public override void Initialize()
        {
            base.Health = base.maxHealth;
            //base.currentSpeed = new Vector3(base.maxSpeed/2, 0f, 0f);
            //base.currentAcceleration = new Vector3(base.maxAcceleration / 2, 0f, 0f);
            base.currentDriftingPoint = new Vector3(base.targetPosition.transform.position.x,
                                                     base.targetPosition.transform.position.y,
                                                     base.targetPosition.transform.position.z);
			material = GetComponentInChildren<Renderer>().material;
        }

        /// <summary>
        /// Set target position for enemy to be there.
        /// </summary>
        /// <param name="targetPosition"></param>
        public override void SetTargetPosition(GameObject targetPosition)
        {
            base.targetPosition = targetPosition;
        }

        /// <summary>
        /// Moves this enemy according it's posibilities and targets.
        /// </summary>
        /// <param name="sceneData"></param>
        /// <param name="deltaTime"></param>
        public override void Move(SceneData sceneData, float deltaTime)
        {
            //base.currentAcceleration = GetCurrentAcceleration(base.targetPosition, base.maxAcceleration);
            //base.currentSpeed = GetCurrentSpeed(base.currentSpeed, base.currentAcceleration, deltaTime);
            if ((_speedSmoother != 0) || (_driftingSpeedDivider != 0))
            {
                Vector3 speed = GetCurrentSpeed(sceneData, base.currentSpeed, targetPosition, deltaTime);
                base.currentSpeed = Vector3.Lerp(base.currentSpeed, speed, _speedSmoother);
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
                    //Tell Singleton to destroy this enemy;
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
			print(material.color);
			material.color = Color.green;

        }

        Vector3 GetCurrentSpeed(SceneData sceneData, Vector3 currentSpeed, GameObject targetPosition, float deltaTime)
        {
            if (Mathf.Abs(targetPosition.transform.position.z - this.transform.position.z) < driftingRadius &&
                Mathf.Abs(targetPosition.transform.position.z - this.transform.position.z) < driftingRadius)
                return DriftSpeed(sceneData, deltaTime).normalized * maxSpeed / _driftingSpeedDivider;

            return new Vector3(targetPosition.transform.position.z - this.transform.position.z,
                              0,
                              targetPosition.transform.position.x - this.transform.position.x).normalized * maxSpeed;
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
                currentDriftingPoint = new Vector3(Random.Range(base.targetPosition.transform.position.x - driftingRadius,
                                                                base.targetPosition.transform.position.x + driftingRadius),
                                                   0,
                                                   Random.Range(base.targetPosition.transform.position.z - driftingRadius,
                                                                base.targetPosition.transform.position.z + driftingRadius));
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
        }
    }
}

