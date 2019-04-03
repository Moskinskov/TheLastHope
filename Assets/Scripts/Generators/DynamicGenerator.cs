using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Enemies;

namespace TheLastHope.Generators
{
    /// <summary>
    /// Generator of dynamic objects
    /// </summary>
	class DynamicGenerator : AGenerator
	{
        public override void Generate(List<GameObject> objList, SceneData sceneData, List<Vector3> positions)
        {
            if (sceneData.Enemies.Count < sceneData.TargetEnemyCount)
            {
                foreach(var enemy in objList)
                {
                    GameObject genEnemy = Instantiate(enemy, positions[objList.IndexOf(enemy)], Quaternion.identity);
                    enemy.GetComponentInChildren<AEnemy>().Init(sceneData);
                    sceneData.Enemies.Add(enemy);
                }
            }
        }

        #region Out of use
        public override void Generate(GameObject[] objects, SceneData sceneData)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
