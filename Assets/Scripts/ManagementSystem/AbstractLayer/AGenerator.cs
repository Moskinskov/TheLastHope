using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;

namespace TheLastHope.Management.AbstractLayer
{
    /// <summary>
	/// Abstract generator class.
	/// </summary>
	public abstract class AGenerator : MonoBehaviour
	{
        #region Abstract methods
        /// <summary>
        /// Generate dynamic objects from list objList (Object Pool) by patterns from list positions
        /// </summary>
        /// <param name="objList">Object Pool</param>
        /// <param name="sceneData"></param>
        /// <param name="positions">Positions pattern</param>
        public abstract void Generate(List<GameObject> objList, SceneData sceneData, List<Vector3> positions);
        /// <summary>
        /// Generate static objects from array by local pattern
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="sceneData"></param>
		public abstract void Generate(GameObject[] objects, SceneData sceneData);
        #endregion

    }
}
