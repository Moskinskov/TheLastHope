using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;

namespace TheLastHope.Management.AbstractLayer
{
	public abstract class AGenerator : MonoBehaviour
	{

		public abstract void Generate(SceneData sceneData);
		public abstract void Generate(GameObject gameObject, SceneData sceneData, List<Vector3> positions);
		public abstract void Generate(GameObject[] objects, SceneData sceneData);

	}
}
