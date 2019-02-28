using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;

namespace TheLastHope.Management.AbstractLayer
{
    public abstract class ATurret : MonoBehaviour
    {
        //Оружие турели
        [SerializeField] internal AWeapon weapon;
        //Скорость поворота турели к цели
        [SerializeField] internal float turningAngularSpeed;

        public abstract void TurnTurret(SceneData sceneData, float deltaTime);

        public abstract void TurUpdate(SceneData sceneData, float deltaTime);

    }
}

