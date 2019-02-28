using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TheLastHope.Management.AbstractLayer
{
    public abstract class ABaseObject : MonoBehaviour
    {
        [SerializeField] protected float maxHealth;

        public bool IsAvtive { get; set; }
        public bool IsDestructable { get; set; }
        public float Health { get; set; }

        public abstract void Initialize();
        public abstract void SetDamage(float damage);

    }
}

