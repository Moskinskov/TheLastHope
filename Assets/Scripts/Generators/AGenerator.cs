using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Data;

namespace Generators
{
    public abstract class AGenerator : MonoBehaviour
    {

        public abstract void Generate(SceneData sceneData);
        
    }
}
