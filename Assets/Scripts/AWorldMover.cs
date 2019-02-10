using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class AWorldMover : MonoBehaviour
{
    public abstract void MoveWorld(SceneData sceneData, float deltaTime);
}
