	using System;
using System.Collections.Generic;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;
using UnityEngine;
namespace TheLastHope.Generators
{
	class RailGenerator : AGenerator
	{
		[SerializeField] private GameObject _rails;
		[SerializeField] private float railLength;
		[SerializeField] private float startTerrainCoordinate;    //if Terrain is located in x[-15; 15] startTerrainCoordinate will be -15
		[SerializeField] private int railsCount = 3;
		#region Debugging; start without GameManager
		//void Start()
		//{
		//    SceneData sceneData = new SceneData();
		//    Generate(sceneData);
		//}
		#endregion

		


		public override void Generate(List<GameObject> obj, SceneData sceneData, List<Vector3> positions)
		{
			//throw new NotImplementedException();
		}

		public override void Generate(GameObject[] objects, SceneData sceneData)
		{
			//throw new NotImplementedException();
		}
	}
}
