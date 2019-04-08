/// Limerence Games
/// The Last Hope
/// Curator: Danny Kotov
/// Author: Sergey Aydarov
/// to be commented

using System.Collections.Generic;
using System;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Generators
{
    class NewRailGen : AGenerator
    {
        //Pattern structure 5 rows 9 columns, each cell 10x10

        private int startGenerationPosition;
        [SerializeField] private Vector3[] positions;                 //local temporary pattern 
        int currentLine = 0;
        int middleCount = 0;
        [SerializeField] float step=2.5f;

        #region Out of use
        public override void Generate(List<GameObject> gameObject, SceneData sceneData, List<Vector3> positions)
        {
        }
        #endregion

        /// <summary>
        /// Generate objects by pattern positions
        /// </summary>
        /// <param name="objects">ObjectPool with object's position pattern</param>
        /// <param name="sceneData"></param>
		public override void Generate(GameObject[] objects, SceneData sceneData)
        {
            if (sceneData.CurrentLine > currentLine)
            {
                float exactX = sceneData.Props[sceneData.Props.Count - 1].transform.position.x + 10;
                foreach(var obj in objects)
                {
                    if (obj)
                    {
                        if (obj.GetComponent<ARail>().Type == RailType.Straight)
                        {
                            middleCount = 0;
                            break;
                        }
                        else if (obj.GetComponent<ARail>().Type == RailType.UpMiddle)
                        {
                            middleCount += 1;
                            break;
                        }
                        else if (obj.GetComponent<ARail>().Type == RailType.DownMiddle)
                        {
                            middleCount -= 1;
                            break;
                        }
                        else if (obj.GetComponent<ARail>().Type == RailType.UpEnd
                            || obj.GetComponent<ARail>().Type == RailType.DownEnd)
                        {
                            middleCount = 0;
                            break;
                        }
                    }
                }
                for (var i = 0; i < 5; i++)
                {
                    positions[i] = new Vector3(exactX, positions[i].y, positions[i].z);
                }
                currentLine = sceneData.CurrentLine;
            }
            for (int i = 0; i < 5; i++)
            {
                if (objects[i] != null)
                {
                    sceneData.Rails.Add(Instantiate(objects[i], 
                                        new Vector3 (positions[sceneData.LinesCount - 1 - i].x,
                                                    positions[sceneData.LinesCount - 1 - i].y,
                                                    positions[sceneData.LinesCount - 1 - i].z + step * middleCount), 
                                        Quaternion.identity));
                }
            }
        }
    }
}
