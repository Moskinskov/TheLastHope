using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Enemies;

namespace TheLastHope.Generators
{
	class DynamicGenerator : AGenerator
	{
        private int startGenerationPosition;
        private Vector3[] positions;                 //local temporary pattern 
        private int activeColumn;

        private void Initialization()
        {
            activeColumn = 0;

            positions = new Vector3[5];                                 //5 rows z={20, 10, 0, -10, -20}; 0 - rails 
            positions[0] = new Vector3(-80, 0, 20);
            positions[1] = new Vector3(-80, 0, 10);
            positions[2] = new Vector3(-80, 0, 0);
            positions[3] = new Vector3(-80, 0, -10);
            positions[4] = new Vector3(-80, 0, -20);
        }

        public override void Generate(SceneData sceneData)          //method for static objects?
		{
			throw new NotImplementedException();
		}

        public override void Generate(List<GameObject> objList, SceneData sceneData, List<Vector3> positions)
        {
            if (sceneData.Enemies.Count < sceneData.TargetEnemyCount)
            {
                var i = 0;
                foreach(var enemy in objList)
                {
                    GameObject genEnemy = Instantiate(
                    enemy,
                    positions[objList.IndexOf(enemy)],                                //position NOT depends on obj name (or type, etc.) TEMP POSITION!
                    Quaternion.identity);
                    enemy.GetComponentInChildren<AEnemy>().Init();
                    sceneData.Enemies.Add(enemy);
                }
            }
        }

		// public override void Generate(GameObject obj, SceneData sceneData, List<Vector3> positions)
		// {
		// 	if (sceneData.Enemies.Count < sceneData.TargetEnemyCount)
		// 	{
		// 		for (int i = sceneData.TargetEnemyCount - sceneData.Enemies.Count; i < sceneData.TargetEnemyCount; i++)
		// 		{
		// 			GameObject enemy = Instantiate(
		// 			obj,
		// 			positions[0] + new Vector3((i * 8), 0, (i * 10) - 30),                                //position NOT depends on obj name (or type, etc.) TEMP POSITION!
		// 			Quaternion.identity);
		// 			enemy.GetComponentInChildren<AEnemy>().Initialize();
		// 			sceneData.Enemies.Add(enemy);
		// 		}
		// 	}
		// }
        
        /// <summary>
        /// Generates enemies by pattern.
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="sceneData"></param>
		public override void Generate(GameObject[] objects, SceneData sceneData)
		{
            if (positions == null)                                      //temporary data
            {
                Initialization();
            }

            for (int i = 0; i < 5; i++)
            {
                if (objects[i] != null)
                {
                    GameObject enemy = Instantiate(objects[i], positions[i % 5], Quaternion.identity);
                    enemy.GetComponentInChildren<AEnemy>().Init();
                    sceneData.Enemies.Add(enemy);
                }
            }
            activeColumn++;

            if (activeColumn == 10)
            {
                activeColumn = 0;
            }
        }
	}
}
