/// Limerence Games
/// The Last Hope
/// Curator: Sergey Aydarov
/// to be commented

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;

namespace TheLastHope.Management.AbstractLayer
{
    public abstract class ATrigger : MonoBehaviour
    {
        public TriggerType Type { get; set; }
        public List<Transform> Pattern { get; set; }
        public List<string> EnemiesNames { get; set; }
    }
}

