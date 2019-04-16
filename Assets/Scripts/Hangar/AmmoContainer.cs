/// Limerence Games
/// The Last Hope
/// Curator: Danny Kotov
/// Commented

using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;

namespace TheLastHope.Hangar
{
    class AmmoContainer : AAmmoContainer
    {
        /// <summary>
        /// Max capacity of ammunition which container has
        /// </summary>
        public int maxCapacity = 1000;
        /// <summary>
        /// Remaining capacity for ammunition in container
        /// </summary>
        private int currentCapacity;

        public override void Init()
        {
            currentCapacity = maxCapacity;
            Ammo = new Dictionary<AmmoType, int>();

            AmmoSize = new Dictionary<AmmoType, int>()
            {
                [AmmoType.ADM401_84mms] = 25,
                [AmmoType.Energy] = 25,
                [AmmoType.M792HEI_T] = 1,
                [AmmoType.Shotgun] = 5
            };
            //Ammo[AmmoType.ADM401_84mms] = 30;
            //Ammo[AmmoType.Energy] = 30;
            //Ammo[AmmoType.M792HEI_T] = 700;
            //Ammo[AmmoType.Shotgun] = 200;
        }

        /// <summary>
        /// Adding certain type ammo in container
        /// </summary>
        /// <param name="type">Ammunition type</param>
        /// <param name="amount">How many add</param>
        public void AddAmmo(AmmoType type, int amount)
        {
            if (amount * AmmoSize[type] >= currentCapacity)
            {
                Ammo[type] += currentCapacity / AmmoSize[type];
                currentCapacity -= (currentCapacity / AmmoSize[type]) * AmmoSize[type];   //for example: (19 / 5) * 5 = (3) * 5 = 15
            }
            else
            {
                Ammo[type] += amount ;
                currentCapacity -= amount * AmmoSize[type];
            }
        }

        /// <summary>
        /// Taking away certain type ammo from container
        /// </summary>
        /// <param name="type">Ammunition type</param>
        /// <param name="amount">How many take</param>
        /// <returns></returns>
        public override bool GetAmmo(AmmoType type, int amount)
        {
            print($"TYPE: {type.ToString()}");
            if (Ammo[type] >= amount)
            {
                Ammo[type] -= amount;
                currentCapacity += amount * AmmoSize[type];
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
