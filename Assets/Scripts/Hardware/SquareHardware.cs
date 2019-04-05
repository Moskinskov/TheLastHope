﻿using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Hardware
{
    class SquareHardware : AHardware
    {
        void Awake()
        {
            typePosition = TypePosition.square;
        }
    }
}
