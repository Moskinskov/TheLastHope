/// Limerence Games
/// The Last Hope
/// Curator: Danny Kotov
/// Commented

using System.Collections.Generic;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using UnityEngine;

namespace TheLastHope.Hangar
{
    internal class AmmoContainer : AAmmoContainer
    {
        [SerializeField] float heitAmount = 0f;
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
            Ammo = new Dictionary<AmmoType, int>()
            {
                [AmmoType.ADM401_84mms] = 30,
                [AmmoType.Energy] = 30,
                [AmmoType.M792HEI_T] = 3000,
                [AmmoType.Shotgun] = 20
            };

            AmmoSize = new Dictionary<AmmoType, int>()
            {
                [AmmoType.ADM401_84mms] = 25,
                [AmmoType.Energy] = 25,
                [AmmoType.M792HEI_T] = 1,
                [AmmoType.Shotgun] = 5
            };

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
                Ammo[type] += amount;
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
            heitAmount = Ammo[AmmoType.M792HEI_T];
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
