using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Data;

namespace TheLastHope.Generators
{
    public abstract class AGenerator : MonoBehaviour
    {

        public abstract void Generate(SceneData sceneData);
        public abstract void Generate(GameObject gameObject, SceneData sceneData, List<Vector3> positions);
        
    }
}
