﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Weapons.Software;

namespace TheLastHope.Management.AbstractLayer
{
    public abstract class ATurret : MonoBehaviour
    {
        //Оружие турели
        [SerializeField] internal AWeapon weapon;
        [SerializeField] internal ASoftware soft;
        [SerializeField] internal bool manualMode = false;
        //Скорость поворота турели к цели
        [SerializeField] internal float turningAngularSpeed;

        public abstract void TurnTurret(float deltaTime);

        public abstract void TurUpdate(SceneData sceneData, float deltaTime);

        public abstract void Init();

        //меняем режим стрельбы
        public void SwitchMode()
        {
            manualMode = !manualMode;
        }
    }
}

