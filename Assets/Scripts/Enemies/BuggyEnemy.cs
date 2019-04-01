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
    public class BuggyEnemy : AEnemy
    {
        [SerializeField] private float _driftingSpeedDivider;
        [SerializeField] private float _speedSmoother;
        [SerializeField] private ATurret _turret;
        [SerializeField] private float _visionDistance;
		[SerializeField] private Texture _damageTex;
		[SerializeField] private List<GameObject> _frontWheels;
		[SerializeField] private List<GameObject> _rearWheels;
		[SerializeField] private float _visionSpread;
		[SerializeField] private float _turnSpeed;
		[SerializeField] private float _turnRatio;

		private RaycastHit hit;
		private Renderer[] renderers;
		private Color _initColor;
		private Texture _initTexture;
		private Timer timer;
		private List<GameObject> _wheels;
		private List<Texture> _textures;
		private Vector3 _leftEyeVector;
		private Vector3 _rightEyeVector;
		private bool _isAlive;



		/// <summary>
		/// Resets health.
		/// </summary>
		public override void Init()
		{
			_textures = new List<Texture>();
			timer = new Timer();
			renderers = GetComponentsInChildren<Renderer>();
			foreach (Renderer rend in renderers)
			{
				if (rend) _textures.Add(rend.material.mainTexture);
			}
		}

		public override void Init(SceneData sceneData)
        {
			_turret.Init();
			_isAlive = true;
			IsActive = true;
			base.MaxHealth = maxHealth;
            base.Health = base.MaxHealth;
			int _rndCar = Random.Range(1, sceneData.TrainCars.Count);
			target = sceneData.TrainCars[_rndCar].gameObject.transform;
			this.targetPosition = target;
			base.currentDriftingPoint = new Vector3(this.targetPosition.position.x,
                                                     this.targetPosition.position.y,
                                                     this.targetPosition.position.z);
			_wheels = new List<GameObject>();
			InitVision();
			Init();
		}

		/// <summary>
		/// Moves this enemy according it's posibilities and targets.
		/// </summary>
		/// <param name="sceneData"></param>
		/// <param name="deltaTime"></param>
		private void InitVision()
		{
			Quaternion _leftSpreadAngle = Quaternion.AngleAxis((_visionSpread/2), new Vector3(0, 1, 0));
			Quaternion _rightSpreadAngle = Quaternion.AngleAxis((-_visionSpread/2), new Vector3(0, 1, 0));
			Vector3 _leftEyeVector = _leftSpreadAngle * transform.forward;
			Vector3 _rightEyeVector = _rightSpreadAngle * transform.forward;
			Ray _leftEye = new Ray(transform.position, _leftEyeVector);
			Ray _rightEye = new Ray(transform.position, _rightEyeVector);
		}

		public override void EnemyUpdate(SceneData sceneData, float deltaTime)
		{
			if (timer.IsEvent()) ChangeTex(false);
			if (Health < 0 && _isAlive)
			{
				Die();
				sceneData.Props.Insert((sceneData.Props.Count-2), this.gameObject);
			}
			if (_isAlive)
			{
				RotateWheels(sceneData);
				_turret.TurUpdate(sceneData, deltaTime);
				timer.TimerUpdate();
				Move(sceneData, deltaTime);
				if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity))
				{
					if ((hit.transform.tag == "Player") && (hit.distance < _visionDistance))
					{
						_turret.TurnTurret(2);
						Turn();
					}

					else if ((hit.distance < _visionDistance) && (hit.transform.gameObject.tag == "Finish"))
					{
						this.gameObject.GetComponent<AudioSource>().clip = null; // KILL ME FOR THIS!
						Health = 0;
						//Tell Destroyer to destroy this enemy;
					}

					else if ((hit.transform.tag == "Prop" || hit.transform.tag == "Enemy") && (hit.distance < _visionDistance))
					{
						print("I SEE A PROP!");
						Turn();
					}
				}

			}

			else
			{

			}


			Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * _visionDistance, Color.red);
			Debug.DrawRay(transform.position, transform.TransformDirection(_leftEyeVector) * _visionDistance, Color.red);
			Debug.DrawRay(transform.position, transform.TransformDirection(_rightEyeVector) * _visionDistance, Color.red);
		}

		private void Turn()
		{
			if(IsActive && _isAlive) transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(transform.position.z, transform.position.z + _turnRatio, Time.deltaTime/_turnSpeed));
		}

		

		private void Move(SceneData sceneData, float deltaTime)
		{
			if ((_speedSmoother != 0) || (_driftingSpeedDivider != 0))
			{
				Vector3 speed = GetCurrentSpeed(sceneData, base.currentSpeed, targetPosition, deltaTime);
				base.currentSpeed = Vector3.Lerp(base.currentSpeed, speed, _speedSmoother);
				gameObject.transform.position = new Vector3(gameObject.transform.position.x + currentSpeed.x * deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
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
            if (Mathf.Abs(targetPosition.transform.position.x - this.transform.position.x) < driftingRadius &&
                Mathf.Abs(targetPosition.transform.position.x - this.transform.position.x) < driftingRadius)
                return DriftSpeed(sceneData, deltaTime).normalized * maxSpeed / _driftingSpeedDivider;

            return new Vector3(targetPosition.transform.position.x - this.transform.position.x,
                              0,
                              targetPosition.transform.position.z - this.transform.position.z).normalized * maxSpeed;
        }

		private void ChangeTex(bool isDamage)
		{
			if (isDamage && IsActive)
			{

				foreach (Renderer rend in renderers)
				{
					rend.material.mainTexture = _damageTex;
				}
				timer.Start(0.1f);
			}
			else
			{
				for (int i = 0; i < renderers.Length; i++)
				{
					renderers[i].material.mainTexture = _textures[i];
				}
			}
		}

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
            var _buggy = this.gameObject;
			var _snd = this.gameObject.GetComponent<AudioSource>();
			Rigidbody[] _rigidBodies = this.gameObject.GetComponentsInChildren<Rigidbody>();
			//_buggy.gameObject.SetActive(false);
			IsActive = false;
			_isAlive = false;
			foreach (Rigidbody rb in _rigidBodies)
			{
				rb.isKinematic = false;
				rb.AddRelativeForce(transform.forward * 70, ForceMode.Impulse);
				rb.AddExplosionForce(50, transform.position, 100);
			}
            _explosion.gameObject.SetActive(true);
            if (_snd) _snd.Play();
			this.gameObject.GetComponent<Collider>().enabled = false;
			ChangeTex(false);
        }

		public void RotateWheels(SceneData sceneData)
		{
			if (_wheels != null)
			{
				foreach (GameObject wheel in _frontWheels)
				{
					try { wheel.transform.Rotate(sceneData.TrainSpeed * 3, 0, 0); }
					catch { print("No Wheels!"); }
				}

				foreach (GameObject wheel in _rearWheels)
				{
					try { wheel.transform.Rotate(sceneData.TrainSpeed * 3, 0, 0); }
					catch { print("No Wheels!"); }
				}
			}
		}

    }
}

