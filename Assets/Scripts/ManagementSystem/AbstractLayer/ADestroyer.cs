using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;

namespace TheLastHope.Management.AbstractLayer
{
    public abstract class ADestroyer : MonoBehaviour
    {
        public abstract void Destroy(SceneData sceneData);
    }
}

