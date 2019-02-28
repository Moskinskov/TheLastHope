using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TheLastHope.Management.AbstractLayer
{
    public abstract class ABaseObject : MonoBehaviour
    {
        public bool isActive;
        public bool isDestructable;
        public float health;
        [SerializeField] internal float maxHealth;

        public abstract void Initialize();
        public abstract void SetDamage(float damage);

    }
}

