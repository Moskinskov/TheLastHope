using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Data;

class WorldMovement : AWorldMover
{
    [SerializeField] GameObject terrain;
    Material terrainMaterial;
    float trainPrevSpeed;

    /// <summary>
    /// Initializes script's variables according to train speed and some other.
    /// </summary>
    /// <param name="sceneData"></param>
    public override void SetupMover(SceneData sceneData)
    {
        terrainMaterial = terrain.GetComponent<Renderer>().material;
        trainPrevSpeed =  sceneData.trainSpeed;

    }
    /// <summary>
    /// Moves objects static on the scene. Except train.
    /// </summary>
    /// <param name="sceneData"></param>
    /// <param name="deltaTime"></param>
    public override void MoveWorld(SceneData sceneData, float deltaTime)
    {
        for (var i = 0; i < sceneData.props.Count; i++)
        {
            sceneData.props[i].transform.position = new Vector3(sceneData.props[i].transform.position.x - sceneData.trainSpeed * deltaTime,
                                                                sceneData.props[i].transform.position.y,
                                                                sceneData.props[i].transform.position.z);
        }
        for (var i = 0; i < sceneData.rails.Count; i++)
        {
            sceneData.rails[i].transform.position = new Vector3(sceneData.rails[i].transform.position.x - sceneData.trainSpeed * deltaTime,
                                                                sceneData.rails[i].transform.position.y,
                                                                sceneData.rails[i].transform.position.z);
        }

        if (trainPrevSpeed != sceneData.trainSpeed)
        {
            for (var i=0; i< sceneData.enemiesPatterns.Count;i++)
            {
                sceneData.enemiesPatterns[i].transform.position = new Vector3(sceneData.enemiesPatterns[i].transform.position.x + (sceneData.trainSpeed - trainPrevSpeed) * deltaTime,
                                                    sceneData.enemiesPatterns[i].transform.position.y,                 
                                                    sceneData.enemiesPatterns[i].transform.position.z);
            }
            trainPrevSpeed = sceneData.trainSpeed;
        }

        //if (terrainMaterial.mainTextureOffset.x < 1)
        //    terrainMaterial.mainTextureOffset = terrainMaterial.mainTextureOffset + new Vector2(sceneData.trainSpeed * deltaTime, 0);
        //else
        //    terrainMaterial.mainTextureOffset = new Vector2(0, 0);
        //terrain.GetComponent<Renderer>().material = terrainMaterial;
    }
}
