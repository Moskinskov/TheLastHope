using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;

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

		public void UpdatePlayer(SceneData sceneData) //Temp
		{
			if (_health < 1)
			{
				Debug.Log("Player wasted!");
				var _oldSpeed = sceneData.TrainSpeed;
				sceneData.TrainSpeed = 0;//Mathf.Lerp(_oldSpeed, 0, Time.deltaTime);
			}
		}

	}
}
