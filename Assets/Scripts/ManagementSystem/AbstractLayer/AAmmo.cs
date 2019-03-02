using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Interfaces;

namespace TheLastHope.Management.AbstractLayer
{
	[RequireComponent(typeof(Collider))]
	public abstract class AAmmo : MonoBehaviour, IPoolable
    {
        [SerializeField] protected float damage;
        [SerializeField] float speed;
        [SerializeField] float maxMagnitude;

        public float Speed { get { return speed; } set { speed = value; } }
        public float MaxMagnitude { get { return maxMagnitude; } set { maxMagnitude = value; } }
        public Vector3 StartPoint { get; set; }
        

        public abstract void OnDepopulate();
		public abstract void OnPopulate();
		public abstract void OnTriggerEnter(Collider collision);
	}
}