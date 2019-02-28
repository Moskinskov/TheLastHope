using UnityEngine;

namespace TheLastHope.Weapons
{
    public class LaserGun : AEnergeticWeapon
    {
        [Header("Required components"), Tooltip("Muzzle. Laser emits from here.")]
		[SerializeField]
		private Transform _LGMuzzle;
        [Tooltip("Laser sound")]
		[SerializeField]
		private AudioSource LGAudioSource;
        [Tooltip("Laser line")]
		[SerializeField]
		private LineRenderer _LGLineRenderer;
        [Tooltip("Damage effect")]
		[SerializeField]
		private ParticleSystem _LGLaserEffect;

        [Header("Laser damage"), Space(10)]
        [Tooltip("Damage set by laser")]
		[SerializeField]
		private float _LGDamagePerSecond;

        [Header("Laser energy capacity"), Space(10)]
        [Tooltip("The overall laser capacity")]
		[SerializeField]
		private float _LGEnergyCapacity;
        [Tooltip("Energy being consumed per second")]
		[SerializeField]
		private float _LGEnergyPerSecond;
        [Tooltip("Energy being recoveried every second")]
		[SerializeField]
		private float _LGRecoveryPerSecond;
        [Tooltip("Minimal amount of energy that allows to shoot")]
		[SerializeField]
		private float _LGMinActiveEnergy;


        private void Awake()
        {
            _energyCapacity = _LGEnergyCapacity;
            _energyPerSecond = _LGEnergyPerSecond;
            _damagePerSecond = _LGDamagePerSecond;
            _recoveryPerSecond = _LGRecoveryPerSecond;
            _muzzle = _LGMuzzle;
            _origLR = _LGLineRenderer;
            _minActiveEnergy = _LGMinActiveEnergy;
            _laserEffect = _LGLaserEffect;
        }
    }
}