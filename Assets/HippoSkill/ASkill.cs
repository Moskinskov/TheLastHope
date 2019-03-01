using System.Collections;
using System.Collections.Generic;
using TheLastHope.Helpers;
using UnityEngine;

public abstract class ASkill : MonoBehaviour
{
    //Время восстановления способности
    [SerializeField] public float cooldown = 1;
    //Список объектов, с которыми взаимодействует скилл (Может быть пустым)
    [SerializeField] public List<GameObject> listObject = new List<GameObject>();
    protected Timer _delay = new Timer();
    // Start is called before the first frame update
    public abstract void Ability();
    public virtual void Activate()
    {
        if (_delay.Elapsed == -1)
        {
            Ability();
            _delay.Start(cooldown);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _delay.Update();
    }
}
