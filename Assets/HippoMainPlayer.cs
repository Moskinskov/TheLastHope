using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management;
using TheLastHope.Weapons;
using System;

namespace TheLastHope.Hippo
{

	public class HippoMainPlayer: MonoBehaviour
	{
		[SerializeField]
		private float _health = 100.0f;

		private void OnGUI()
		{
			GUI.Label(new Rect(10, 10, 200, 20), ("Player's health: " + _health.ToString()));
		}

		public void SetDamage(float damage)
		{
			_health -= damage;
		}

		public void UpdatePlayer(SceneData sceneData) //Temp
		{

			if (_health < 1)
			{
				Die(sceneData);
			}

		}

		private void Die(SceneData sceneData)
		{
			Debug.Log("Player wasted!");
			var _oldSpeed = sceneData.TrainSpeed;
			sceneData.TrainSpeed = Mathf.Lerp(_oldSpeed, 0, Time.deltaTime);
			int _childCount = gameObject.transform.childCount;
			for (int i = 0; i < _childCount; i++)
			{
				var _child = gameObject.transform.GetChild(i);
				if (!_child.gameObject.active) _child.gameObject.SetActive(true); 
			}
			//List<WeaponController> _weaponControllers;
			//WeaponController weaponController = GameManager.Instance.

			for (int i = 0; i < _childCount; i++)
			{
				var _child = gameObject.transform.GetChild(i);
				if (!_child.gameObject.active) _child.gameObject.SetActive(true);
			}
		}
	}
}
