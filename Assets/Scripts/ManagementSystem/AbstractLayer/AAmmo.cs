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
        [SerializeField] public float speed;
        [SerializeField] public float maxMagnitude;
        public Vector3 startPoint;

		public abstract void OnDepopulate();
		public abstract void OnPopulate();
		public abstract void OnTriggerEnter(Collider collision);

	}
}