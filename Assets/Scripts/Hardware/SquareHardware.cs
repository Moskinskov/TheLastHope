/// Limerence Games
/// The Last Hope
/// Curator: Danny Kotov
/// to be commented

using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Hardware
{
    /// <summary>
    /// Hardware for items which will be installed on carriage. Square type.
    /// </summary>
    class SquareHardware : AHardware
    {
        void Awake()
        {
            typePosition = TypePosition.Square;
        }
    }
}
