using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;

namespace TheLastHope.Management.AbstractLayer
{
    public abstract class AWeapon : MonoBehaviour
    {
        public abstract void Fire(SceneData sceneData);
		public abstract void WeaponUpdate();
		public abstract void Init();
    }
}

