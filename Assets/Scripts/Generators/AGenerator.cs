using System;
using System.Collections.Generic;
using UnityEngine;

namespace Generators
{
    public abstract class AGenerator : MonoBehaviour
    {

        public abstract void Generate(SceneData sceneData);
        
    }
}
