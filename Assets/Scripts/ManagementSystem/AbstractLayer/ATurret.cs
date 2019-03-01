using System.Collections;
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
<<<<<<< HEAD
		[SerializeField] internal bool manualMode = false;
		//Скорость поворота турели к цели
		[SerializeField] internal float turningAngularSpeed;
=======
        [SerializeField] internal bool manualMode = false;
        //Скорость поворота турели к цели
        [SerializeField] internal float turningAngularSpeed;
>>>>>>> fab68fbe74401601a774c71ab207ca4929fa3d92

        public abstract void TurnTurret(float deltaTime);

        public abstract void TurUpdate(SceneData sceneData, float deltaTime);

        public abstract void Init();

    }
}

