/// Limerence Games
/// The Last Hope
/// Curator: Danny Kotov
/// Commented

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Management.AbstractLayer
{
    /// <summary>
    /// Abstract hardware class
    /// </summary>
    public class AHardware : MonoBehaviour
    {
        /// <summary>
        /// Required slot type on carriage
        /// </summary>
        public TypePosition typePosition;
        /// <summary>
        /// Icon of item in inventory
        /// </summary>
        public Sprite sprite;
        /// <summary>
        /// Contained objects
        /// </summary>
        public List<GameObject> objects;
    }

    public enum TypePosition
    {
        Square
    }
}