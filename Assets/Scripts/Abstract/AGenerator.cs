using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Data;

namespace TheLastHope.Generators
{
    public abstract class AGenerator : MonoBehaviour
    {

        public abstract void Generate(SceneData sceneData);
        
    }
}
