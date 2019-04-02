using TheLastHope.Helpers;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Weapons
{
    /// <summary>
    ///  ARangedWeapon class
    ///  realizes basic bahaviour of the ranged weapon
    ///  Fire() calls Shot() method that is to be overriden in every 
    /// </summary>
    public abstract class ARangedWeapon : AWeapon //change to BaseObject when it is ready
    {
        #region Serializables
        [SerializeField] protected float _rateOfFire = 1.0f; //firing speed
        [SerializeField] protected float _shootingRange = 30.0f; //firing range
        [SerializeField] protected float _reloadTime = 1.0f; //reload time
        [SerializeField] private float _force = 30.0f; //power of fire
        [SerializeField] protected bool _canChangeFiringMode = false; //ability to change firing mode
        [SerializeField] protected FiringMode _currentFiringMode; //current active firing mode
        [SerializeField] protected int[] _firingModesAvailable; //available firing modes
        [SerializeField] protected AAmmo _ammo; //ammunition to fire
        [SerializeField] private Transform _muzzle; // Система координат для вылета пули
        [SerializeField] protected GameObject _barrel;
        [SerializeField] protected GameObject _muzzleFlash;
        #endregion

        public float Force { get { return _force; } set { _force = value; } }
        public Transform Muzzle { get { return _muzzle; } set { _muzzle = value; } }

        #region Protected Variables
        protected Timer _delay = new Timer();                       //Timer
                                                                    //protected bool _readyToFire = true;                         //Ready to shoot flag
        protected int _ammoInClip;                                  //Current ammo in clip
        //protected int _currentAmmoInClip; //Текущие количество патронов в магазине
        protected AudioSource _audioPlayer; //Типа должно быть использовано для звуков выстрела
        #endregion

        #region Abstract Functions
        public abstract void Shot(SceneData sceneData); //Логика выстрела             
        public abstract void SwitchFiringMode(); // Возможная механика смены режима стрельбы
        #endregion

        public override void Fire(SceneData sceneData)
        //Функция вызывает комманду выстрела проверяя наличие 
        //патронов и учитывая скорость стрельбы
        {
            //_delay.Start(time) отсчитывает время time 
            //после отчета разрешает снова стрелять.
            if (_delay.Elapsed == -1)
            {

                State = WeaponState.Active;
            }
            if (currentAmmoInClip <= 0)
            {
                State = WeaponState.empty;
            }
            if (State == WeaponState.Active && _ammo)
            {
                if (_muzzleFlash) _muzzleFlash.SetActive(true);
                Shot(sceneData);
                var snd = GetComponent<AudioSource>();
                if (snd) snd.Play();
                _delay.Start(_rateOfFire);
                State = WeaponState.Inactive;
                currentAmmoInClip--;
            }


        }
        public AudioSource AudioPlayer
        {
            get { return _audioPlayer; }
            set { _audioPlayer = value; }
        }
        public override void Init()
        {
            currentAmmoInClip = clipSize;
            State = WeaponState.Active;

        }
        public override void WeaponUpdate()
        {
            if (_muzzleFlash) _muzzleFlash.SetActive(false);
            _delay.TimerUpdate();
        }
        public override void Reload(int ammoQuantity)
        {
            currentAmmoInClip = ammoQuantity;
            State = WeaponState.Inactive;
            _delay.Start(_reloadTime);
        }

    }
}
