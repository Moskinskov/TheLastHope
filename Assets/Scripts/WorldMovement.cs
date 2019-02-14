using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Data;

class WorldMovement : AWorldMover
{
    [SerializeField] GameObject terrain1;
    [SerializeField] GameObject terrain2;

    public override void MoveWorld(SceneData sceneData, float deltaTime)
    {
        for (var i =0; i < sceneData.props.Count;i++)
        {
            sceneData.props[i].transform.position = new Vector3(sceneData.props[i].transform.position.x - sceneData.trainSpeed * deltaTime, 
                                                                sceneData.props[i].transform.position.y, 
                                                                sceneData.props[i].transform.position.z);
        }
        for (var i =0; i < sceneData.rails.Count;i++)
        {
            sceneData.rails[i].transform.position = new Vector3(sceneData.rails[i].transform.position.x - sceneData.trainSpeed * deltaTime, 
                                                                sceneData.rails[i].transform.position.y, 
                                                                sceneData.rails[i].transform.position.z);
        }
    }
}
