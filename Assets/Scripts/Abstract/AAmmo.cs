using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Weapons
{
    public abstract class AAmmo : MonoBehaviour, IPoolable
    {
        [SerializeField] internal float damage;
        [SerializeField] internal float speed;
        public bool IsFinifhed { get; set; }
        public float Range { get; set; }

        public Vector3 Direction { get; set; }
        public float Speed { get { return speed; } }

        public virtual void OnPopulate()
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnDepopulate()
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnTriggerEnter(Collider collision)
        {
            try { collision.gameObject.GetComponent<AEnemy>().SetDamage(damage); IsFinifhed = true;  }
            catch { };
            
        }

        public abstract void Move(float deltaTime);

    }
}