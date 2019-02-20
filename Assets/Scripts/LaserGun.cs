using Assets.Scripts.Abstract;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public class LaserGun : AEnergeticWeapon
    {
        [Header("Необходимые компоненты"), Tooltip("Мушка, отсюда вылетает лазер")]
        public Transform _LGMuzzle;
        [Tooltip("Ну это понятно")]
        public AudioSource LGAudioSource;
        [Tooltip("Сам лазер")]
        public LineRenderer _LGLineRenderer;
        [Tooltip("Эффект от попадания")]
        public GameObject _LGLaserEffect;

        [Header("Урон лазера"), Space(10)]
        [Tooltip("Ну это понятно")]
        public float _LGDamagePerSecond;

        [Header("Енергия лазера"), Space(10)]
        [Tooltip("Общий объем энергии")]
        public float _LGEnergyCapacity;
        [Tooltip("Ну это понятно")]
        public float _LGEnergyPerSecond;
        [Tooltip("Ну это понятно")]
        public float _LGRecoveryPerSecond;
        [Tooltip("Минимальное кол-во энергии для начала стрельбы")]
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