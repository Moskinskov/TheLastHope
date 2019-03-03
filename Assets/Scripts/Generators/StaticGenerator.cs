﻿using System.Collections.Generic;
using System;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Generators
{
	class StaticGenerator : AGenerator
    {
        //Pattern structure 5 rows 9 columns, each cell 10x10

        private int startGenerationPosition;
        private Vector3[] positions;                 //local temporary pattern 
        private int activeColumn;

		#region Debugging
		//[SerializeField] private GameObject[] prop;
		//[SerializeField] private int step = 1;
		//[SerializeField] private Vector2 activeTerrainGeometry = new Vector2(15, 5); //Active spawn zone x[-15;15] z[-5;5]
                                                                                     //void Start()
                                                                                     //{
                                                                                     //    SceneData sceneData = new SceneData();
                                                                                     //    sceneData.rails.Add(new GameObject());
                                                                                     //    sceneData.rails[0].transform.position = Vector3.zero;
                                                                                     //    Generate(sceneData);
                                                                                     //}
        #endregion

        #region Out of use
        /// <summary>
        /// Obsolete method
        /// </summary>
        /// <param name="sceneData"></param>
        public override void Generate(SceneData sceneData)
		{
            /*
			if (sceneData.TargetPropsCount > sceneData.Props.Count)
			{
				startGenerationPosition = (int)activeTerrainGeometry.x;

				var stepPos = startGenerationPosition;
				for (var i = sceneData.Props.Count; i < sceneData.TargetPropsCount; i++)
				{
					float zCoordinate = UnityEngine.Random.Range(-activeTerrainGeometry.y, activeTerrainGeometry.y);

					//if props cross the rails; rails' width is [-5;5]
					if (zCoordinate < sceneData.Rails[sceneData.Rails.Count - 1].transform.position.z + 5 &&
						zCoordinate > sceneData.Rails[sceneData.Rails.Count - 1].transform.position.z)
					{
						zCoordinate += 5;
					}
					else if (zCoordinate > sceneData.Rails[sceneData.Rails.Count - 1].transform.position.z - 5 &&
							 zCoordinate < sceneData.Rails[sceneData.Rails.Count - 1].transform.position.z)
					{
						zCoordinate -= 5;
					}

					sceneData.Props.Add(Instantiate(
						prop[UnityEngine.Random.Range(0, prop.Length)],
						new Vector3(stepPos, 0, zCoordinate),
						Quaternion.identity));
					stepPos += step;
				}
			}
            */
		}

		public override void Generate(GameObject gameObject, SceneData sceneData, List<Vector3> positions)
		{
			throw new System.NotImplementedException();
		}
        #endregion

        private void Initialization()
        {
            activeColumn = 0;

            positions = new Vector3[5];                                 //5 rows z={20, 10, 0, -10, -20}; 0 - rails 
            positions[0] = new Vector3(40, 0, 20);
            positions[1] = new Vector3(40, 0, 10);
            positions[2] = new Vector3(40, 0, 0);
            positions[3] = new Vector3(40, 0, -10);
            positions[4] = new Vector3(40, 0, -20);
        }

        /// <summary>
        /// Generate objects by pattern positions
        /// </summary>
        /// <param name="objects">ObjectPool with object's position pattern</param>
        /// <param name="sceneData"></param>
		public override void Generate(GameObject[] objects, SceneData sceneData)
        {
            if (positions == null)                                      //temporary data
            {
                Initialization();
            }

            if (sceneData.Props[sceneData.Props.Count - 1].transform.position.x < positions[0].x - 10)
            {
                for (int i = 0 + (5 * activeColumn); i < 5 + (5 * activeColumn); i++)
                {
                    if (objects[i] != null)
                    {
                        sceneData.Props.Add(Instantiate(objects[i], positions[i % 5], Quaternion.identity));
                    }
                }
                activeColumn++;
            }
            if (activeColumn == 10)
            {
                activeColumn = 0;
            }
		}
	}
}
