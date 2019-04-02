using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;

namespace TheLastHope.Management.AbstractLayer
{
    public abstract class AAmmoContainer : MonoBehaviour
    {
        public Dictionary<AmmoType, int> ammo;
        protected Dictionary<AmmoType, int> ammoSize;

        public virtual void Init()
        {

        }

        public virtual bool GetAmmo(AmmoType type, int amount)
        {
            if(ammo[type] >= amount)
            {
                ammo[type] -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

