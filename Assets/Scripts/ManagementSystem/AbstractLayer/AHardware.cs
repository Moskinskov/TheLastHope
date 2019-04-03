/// Limerence Games
/// The Last Hope
/// Curator: Danny Kotov
/// to be commented

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Management.AbstractLayer
{
	/// <summary>
	/// Abstract generator class.
	/// Use this class to create new object generators.
	/// </summary>
	public class AHardware : MonoBehaviour
    {
		//TODO: Comment this
        public TypePosition typePosition;
        public Sprite sprite;
    }

    public enum TypePosition
    {
        Square
    }
}