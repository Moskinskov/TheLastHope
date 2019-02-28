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
        public override void Generate(SceneData sceneData)          //method for static objects?
        {
            throw new NotImplementedException();
        }
        public override void Generate(GameObject obj, SceneData sceneData, List<Vector3> positions)
        {
            if(sceneData.Enemies.Count < sceneData.TargetEnemyCount)
            {
                for (int i = sceneData.TargetEnemyCount - sceneData.Enemies.Count; i < sceneData.TargetEnemyCount; i++)
                {
                    GameObject enemy = Instantiate(
                    obj,
                    positions[0] + new Vector3((i*6), 0, (i*7)-40),                                //position NOT depends on obj name (or type, etc.) TEMP POSITION!
                    Quaternion.identity);
                    enemy.GetComponentInChildren<CopterEnemy>().Initialize();
                    sceneData.Enemies.Add(enemy);
                }
            }
        }

        public override void Generate(GameObject[] objects, SceneData sceneData)
        {
            throw new NotImplementedException();
        }
    }
}
