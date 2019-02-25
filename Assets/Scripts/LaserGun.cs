using UnityEngine;

namespace TheLastHope.Weapons
{
    public class LaserGun : AEnergeticWeapon
    {
        [Header("Required components"), Tooltip("Muzzle. Laser emits from here.")]
        public Transform _LGMuzzle;
        [Tooltip("Laser sound")]
        public AudioSource LGAudioSource;
        [Tooltip("Laser line")]
        public LineRenderer _LGLineRenderer;
        [Tooltip("Damage effect")]
        public ParticleSystem _LGLaserEffect;

        [Header("Laser damage"), Space(10)]
        [Tooltip("Damage set by laser")]
        public float _LGDamagePerSecond;

        [Header("Laser energy capacity"), Space(10)]
        [Tooltip("The overall laser capacity")]
        public float _LGEnergyCapacity;
        [Tooltip("Energy being consumed per second")]
        public float _LGEnergyPerSecond;
        [Tooltip("Energy being recoveried every second")]
        public float _LGRecoveryPerSecond;
        [Tooltip("Minimal amount of energy that allows to shoot")]
        public float _LGMinActiveEnergy;


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