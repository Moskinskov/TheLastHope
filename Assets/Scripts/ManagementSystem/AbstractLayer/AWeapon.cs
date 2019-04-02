using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Management.AbstractLayer
{
    public abstract class AWeapon : MonoBehaviour
    {
        [SerializeField] AmmoType typeOfAmmo;
        [SerializeField] internal int currentAmmoInClip;
        [SerializeField] internal int clipSize;
        public AmmoType TypeOfAmmo { get { return typeOfAmmo; } set { typeOfAmmo = value; } }
        public WeaponState State { get; set; }
		public bool IsActive { get; set; }
        public int ClipSize { get { return clipSize; } set { clipSize = value; } }
        public int CurrentAmmoInClip { get { return currentAmmoInClip; } set { currentAmmoInClip = value; } }

        public abstract void Fire(SceneData sceneData);
        public abstract void Reload(int ammoQuantity);
        public abstract void WeaponUpdate();
        public abstract void Init();
    }
}

