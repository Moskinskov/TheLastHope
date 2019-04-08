using System;
using System.Collections;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public class FlameThrower : AEnergeticWeapon
    {
        [SerializeField, Header("Necessary resources")]
        private Transform _muzzle;
        [SerializeField]
        private AudioSource _laserAudioSource;
        [SerializeField]
        private ParticleSystem _flameRenderer;
        [SerializeField]
        private ParticleSystem _laserEffect;

        [SerializeField, Header("Weapon options"), Space(10)]
        private float _damagePerSecond;
        [SerializeField]
        private float _energyCapacity;
        [SerializeField]
        private float _energyPerSecond;
        [SerializeField]
        private float _recoveryPerSecond;
        [SerializeField]
        private float _minActiveEnergy;
		[SerializeField]
		private float _maxRange;
		private float _partCountInit = 0;
		private IEnumerator coroutine;
		private bool _isPlaying;

		public override void Init()
        {
            _coreDamagePerSecond = _damagePerSecond;
            _coreEnergyCapacity = _energyCapacity;
            _coreEnergyPerSecond = _energyPerSecond;
            _coreRecoveryPerSecond = _recoveryPerSecond;
            _coreMinActiveEnergy = _minActiveEnergy;
			_coreMaxRange = _maxRange;
			_flameRenderer.emissionRate = 0;
			_flameRenderer.Play();
			//if (!_flameRenderer.isStopped) _flameRenderer.Stop();

            //if (!_laserEffect.isStopped) _laserEffect.Stop();
			if (_laserAudioSource.isPlaying)
			    _laserAudioSource.Stop();

			IsActive = true;
		}

		public override void WeaponUpdate()
        {
			_timerEndOfFire.TimerUpdate();
			CoreChecks();
			LocalChecks();
			if (IsActive == false) _laserEffect.Stop();
		}

        public override void Fire(SceneData sceneData)
        {
            _timerEndOfFire.Start(0.005f);
			if (Physics.Raycast(_muzzle.position, _muzzle.forward, out RaycastHit hit))
			{
				if (hit.distance <= _maxRange && IsActive == true)
				{
					WeaponMethod(hit);
					FireUp(240);
				}

				else FireUp(0);
			}
        }

		private void FireUp(float particleCount)
		{
			if(particleCount != _partCountInit)
			{
				_flameRenderer.Play();
				_flameRenderer.emissionRate = Mathf.Lerp(_partCountInit, particleCount, Time.deltaTime);
				_partCountInit = _flameRenderer.emissionRate;
			}

			if(particleCount == 0)
			{
				_flameRenderer.emissionRate = Mathf.Lerp(_partCountInit, particleCount, Time.deltaTime);
				_partCountInit = _flameRenderer.emissionRate;
				//if (_partCountInit == 0) _flameRenderer.Stop();
			}
		}

		protected override void WeaponMethod(RaycastHit hit)
        {
                if (hit.transform.GetComponent<AEnemy>())
                {
					if (hit.distance <= _maxRange)
					{
						HitTheEnemy(hit);
						SetLRToTarget(hit);

						_laserEffect.transform.SetPositionAndRotation(hit.point, Quaternion.Euler(hit.normal));

					coroutine = Effect(2.0f, hit);

					if (!_isPlaying)
					{
						_isPlaying = true;
						StartCoroutine(coroutine);
					}
				}
                }
        }

        protected override void SetLRToTarget(RaycastHit hit)
        {

        }

        protected void HitTheEnemy(RaycastHit hit)
        {
            hit.transform.GetComponent<AEnemy>().SetDamage(_coreDamagePerSecond * Time.deltaTime);
            _coreCurrentCharge -= _coreEnergyPerSecond * Time.deltaTime;
        }

        protected override void LocalChecks()
        {

        }

		private IEnumerator Effect(float waitTime, RaycastHit hit)
		{
			_laserEffect.transform.SetPositionAndRotation(hit.point, Quaternion.Euler(hit.normal));
			if (!_laserEffect.isPlaying)
				_laserEffect.Play();
			if (!_laserAudioSource.isPlaying)
				_laserAudioSource.Play();

			yield return new WaitForSeconds(_laserAudioSource.clip.length);

			_isPlaying = false;

		}

	}
}

#region 111

//[Header("Required components"), Tooltip("Muzzle. Laser emits from here.")]
//[SerializeField]
//private Transform _LGMuzzle;
//[Tooltip("Laser sound")]
//[SerializeField]
//private AudioSource LGAudioSource;
//[Tooltip("Laser line")]
//[SerializeField]
//private LineRenderer _LGLineRenderer;
//[Tooltip("Damage effect#1")]
//[SerializeField]
//private ParticleSystem _LGLaserEffect1;

//[Header("Laser damage"), Space(10)]
//[Tooltip("Damage set by laser")]
//[SerializeField]
//private float _LGDamagePerSecond;

//[Header("Laser energy capacity"), Space(10)]
//[Tooltip("The overall laser capacity")]
//[SerializeField]
//private float _LGEnergyCapacity;
//[Tooltip("Energy being consumed per second")]
//[SerializeField]
//private float _LGEnergyPerSecond;
//[Tooltip("Energy being recoveried every second")]
//[SerializeField]
//private float _LGRecoveryPerSecond;
//[Tooltip("Minimal amount of energy that allows to shoot")]
//[SerializeField]
//private float _LGMinActiveEnergy;


//private void Awake()
//{
//_energyCapacity = _LGEnergyCapacity;
//_coreEnergyPerSecond = _LGEnergyPerSecond;
//_coreDamagePerSecond = _LGDamagePerSecond;
//_coreRecoveryPerSecond = _LGRecoveryPerSecond;
//_muzzle = _LGMuzzle;
//_origLR = _LGLineRenderer;
//_minActiveEnergy = _LGMinActiveEnergy;
//_laserEffect1 = _LGLaserEffect1;
//}

#endregion