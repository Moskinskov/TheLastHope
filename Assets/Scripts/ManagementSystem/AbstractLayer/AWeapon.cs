using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Management.AbstractLayer
{
    public abstract class AWeapon : MonoBehaviour
    {
        public AmmoType TypeOfAmmo { get; set; }
        public WeaponState State { get; set; }

        public abstract void Fire(SceneData sceneData);
        public abstract void Reload(int ammoQuantity);
        public abstract void WeaponUpdate();
        public abstract void Init();
    }
}

