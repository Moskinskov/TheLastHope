/// Limerence Games
/// The Last Hope
/// Curator: Danny Kotov
/// to be commented

using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Hardware
{
    class SquareHardware : AHardware
    {
        //Information of contained weapon/shields stuffs will be here
        public void Init()
        {
            typePosition = TypePosition.Square;
        }
    }
}
