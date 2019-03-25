using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Management.AbstractLayer
{
    public class AHardware : MonoBehaviour
    {
        public TypePosition typePosition;
        public Sprite sprite;
    }

    public enum TypePosition
    {
        square
    }
}