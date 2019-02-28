using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management.AbstractLayer;

class WorldMovement : AWorldMover
{
    [SerializeField] GameObject terrain;
    Renderer[] terrainRenderers;
    [SerializeField] float speedDivider;
    float trainPrevSpeed;

    /// <summary>
    /// Initializes script's variables according to train speed and some other.
    /// </summary>
    /// <param name="sceneData"></param>
    public override void SetupMover(SceneData sceneData)
    {
        terrainRenderers = terrain.GetComponentsInChildren<Renderer>();
        trainPrevSpeed =  sceneData.trainSpeed;
    }
    /// <summary>
    /// Moves objects static on the scene. Except train.
    /// </summary>
    /// <param name="sceneData"></param>
    /// <param name="deltaTime"></param>
    public override void MoveWorld(SceneData sceneData, float deltaTime)
    {
        print("MOVERSPEED: " + sceneData.trainSpeed);
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
        
        //foreach(var terrainRenderer in terrainRenderers)
        //{
        //    if (terrainRenderer.material.mainTextureOffset.x > -1)
        //        terrainRenderer.material.mainTextureOffset -= new Vector2(sceneData.trainSpeed * deltaTime / speedDivider, 0);
        //    else
        //        terrainRenderer.material.mainTextureOffset += new Vector2(1, 0);
        //}
    }
}
