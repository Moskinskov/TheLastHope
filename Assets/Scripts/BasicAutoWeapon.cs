using TheLastHope.Helpers;
using UnityEngine;

public class BasicAutoWeapon : MonoBehaviour
{
    [Header("Необходимые префабы")]
    [Tooltip("'Дуло', мушка")]
    public GameObject _Barrel;
    [Tooltip("Осколки при попадании")]
    public GameObject _Effect;
    [Tooltip("Игровая камера, чтобы видеть, куда стрелять")]
    public Camera _camera;

    [Header("Параметры стрельбы")]
    [Range(0.1f, 1f), Tooltip("Скорость стрельбы")]
    public float _rateOfFire;
    [Range(1f, 5f), Tooltip("Скорость перезарядки")]
    public float _reloadTime;
    [Range(10, 20), Tooltip("Кол-во патронов в обоиме")]
    public int _clipSize;

    [SerializeField] float damage;

    #region Properties

    /// <summary>
    /// Сколько патронов в обоиме прям сейчас
    /// </summary>
    public int CurrentClipSize { get; set; }
    /// <summary>
    /// Готово ли оружие для действий?
    /// </summary>
    public bool _isActive { get; private set; }
    /// <summary>
    /// Таймер для стрельбы
    /// </summary>
    private Timer _timerOfFire;
    /// <summary>
    /// Таймер для перезарядки
    /// </summary>
    private Timer _timerOfReload;

    #endregion


    private void Awake()
    {
        SetActive(true);
        _timerOfFire = new Timer();
        _timerOfReload = new Timer();
        CurrentClipSize = _clipSize;
    }
    private void Update()
    {
        _timerOfFire.Update();
        _timerOfReload.Update();

        CheckForRateOfFire();
        CheckForReload();
    }

    #region Weapon Functions
    /// <summary>
    /// Метод "Стрелять"
    /// </summary>
    public void Fire()
    {
        if (!_isActive) return;
        if (CurrentClipSize <= 0) return;

        Ray tempRay = _camera.ScreenPointToRay(Input.mousePosition);    //Пока что стрельба из камеры, потом можно прикрутить стрельбу из "Дула"
        if (Physics.Raycast(tempRay, out RaycastHit tempHit))
        {
            Destroy(Instantiate(_Effect, tempHit.point, Quaternion.identity), 2f);
        }
        tempHit.collider.gameObject.GetComponent<AEnemy>().SetDamage(damage);

        CurrentClipSize--;
        SetActive(false);
        _timerOfFire.Start(_rateOfFire);

        Debug.Log("Выстрел");
    }
    /// <summary>
    /// Метод "Перезарядка"
    /// </summary>
    public void Reload()
    {
        if (!_isActive) return;

        CurrentClipSize = _clipSize;
        _timerOfReload.Start(_reloadTime);
        SetActive(false);

        Debug.Log("Перезарядка");
    }
    /// <summary>
    /// Сделать оружие активным\неактивным
    /// </summary>
    /// <param name="value"></param>
    public void SetActive(bool value)
    {
        if (_isActive != value)
            _isActive = value;
    }

    #endregion

    #region Any Checks
    /// <summary>
    /// Проверка интервала между выстрелами
    /// </summary>
    private void CheckForRateOfFire()
    {
        if (!_timerOfFire.IsEvent()) return;

        if (_timerOfFire.IsEvent())
            SetActive(true);
    }
    /// <summary>
    /// Проверка для перезарядки
    /// </summary>
    private void CheckForReload()
    {
        if (!_timerOfReload.IsEvent())
            return;
        if (_timerOfReload.IsEvent())
            SetActive(true);
    }

    #endregion
}
