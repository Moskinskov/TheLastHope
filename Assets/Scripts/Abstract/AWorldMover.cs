using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Data;


public abstract class AWorldMover : MonoBehaviour
{
    public abstract void MoveWorld(SceneData sceneData, float deltaTime);
}
