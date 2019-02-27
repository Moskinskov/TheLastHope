using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Data;

namespace TheLastHope.Hippo
{

	public class HippoMainPlayer: MonoBehaviour
	{
		[SerializeField]
		private float _health = 100.0f;

		public void SetDamage(float damage)
		{
			_health -= damage;
		}

		public void Update() //Temp
		{
			if (_health < 1)
			{
				Debug.Log("Player wasted!");
				//var _speed = GameManager.Instance.TrainSpeed;
				//Debug.Log("I believe, the speed is " + _speed);
				var _newSpeed = 0;
				GameManager.Instance.TrainSpeed = _newSpeed;
			}
		}

	}
}
