using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Weapons
{
    class SquareHardware : AHardware
    {
        //Information of contained weapon/shields stuffs will be here
        void Awake()
        {
            typePosition = TypePosition.square;
        }
    }
}
