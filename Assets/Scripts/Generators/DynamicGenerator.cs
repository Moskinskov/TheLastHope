using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Data;

namespace TheLastHope.Generators
{
    class DynamicGenerator : AGenerator
    {
        public override void Generate(SceneData sceneData)          //method for static objects?
        {
            throw new NotImplementedException();
        }
        public override void Generate(GameObject obj, SceneData sceneData, List<Vector3> positions)
        {
            if(sceneData.enemies.Count < sceneData.targetEnemyCount)
            {
                for (int i = sceneData.targetEnemyCount - sceneData.enemies.Count; i < sceneData.targetEnemyCount; i++)
                {
                    GameObject enemy = Instantiate(
                    obj,
                    positions[0],                                //position NOT depends on obj name (or type, etc.)
                    Quaternion.identity);
                    enemy.GetComponent<CopterEnemy>().Initialize();
                    sceneData.enemies.Add(enemy);
                }
            }
        }
    }
}
