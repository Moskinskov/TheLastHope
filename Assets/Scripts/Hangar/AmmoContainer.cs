using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;

namespace TheLastHope.Hangar
{
    class AmmoContainer : AAmmoContainer
    {
        public int maxCapacity = 1000;
        int currentCapacity;

        public override void Init()
        {
            currentCapacity = maxCapacity;
            ammo = new Dictionary<AmmoType, int>();

            ammoSize = new Dictionary<AmmoType, int>()
            {
                [AmmoType.ADM401_84mms] = 25,
                [AmmoType.Energy] = 25,
                [AmmoType.M792HEI_T] = 1,
                [AmmoType.Shotgun] = 5
            };
            ammo[AmmoType.ADM401_84mms] = 30;
            ammo[AmmoType.Energy] = 30;
            ammo[AmmoType.M792HEI_T] = 700;
            ammo[AmmoType.Shotgun] = 200;
        }

        /// <summary>
        /// Adding certain type ammo in container
        /// </summary>
        /// <param name="type"></param>
        /// <param name="amount">how many add</param>
        public void AddAmmo(AmmoType type, int amount)
        {
            if (amount * ammoSize[type] >= currentCapacity)
            {
                ammo[type] += currentCapacity / ammoSize[type];
                currentCapacity -= (currentCapacity / ammoSize[type]) * ammoSize[type];   //for example: (19 / 5) * 5 = (3) * 5 = 15
            }
            else
            {
                ammo[type] += amount ;
                currentCapacity -= amount * ammoSize[type];
            }
        }

        /// <summary>
        /// Taking away certain type ammo from container
        /// </summary>
        /// <param name="type"></param>
        /// <param name="amount">how many take</param>
        /// <returns></returns>
        public override bool GetAmmo(AmmoType type, int amount)
        {
            print($"TYPE: {type.ToString()}");
            if (ammo[type] >= amount)
            {
                ammo[type] -= amount;
                currentCapacity += amount * ammoSize[type];
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
