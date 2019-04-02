﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Management.AbstractLayer
{
	/// <summary>
	/// Abstract carriage class.
	/// </summary>
	/// <remarks>
	/// Use this class to create new versions of 
	/// train cars.
	/// </remarks>
	public abstract class ACarriage : MonoBehaviour
    {
		#region Abstract methods
		/// <summary>
		/// Carriage Update.
		/// Being called from a manager.
		/// </summary>
		public abstract void UpdateCarriage();
		#endregion
	}
}

