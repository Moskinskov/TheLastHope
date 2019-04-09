
using UnityEngine;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Hardware
{
    /// <summary>
    /// Hardware for items which won't be installed on carriage. Non type.
    /// </summary>
    class NonHardware : AHardware
    {
        void Awake()
        {
            typePosition = TypePosition.Non;
        }
    }
}
