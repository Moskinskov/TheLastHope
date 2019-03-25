using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management;
using TheLastHope.Weapons;
using TheLastHope.Management.AbstractLayer;
using System;

namespace TheLastHope.Player
{

	public class MainPlayer: ABaseObject
	{
		[SerializeField]
		private float _raycastDepth;
		[SerializeField]
		private float _health;

		[SerializeField]
		private float _maxHealth;
		

		//private void OnGUI()
		//{
		//	GUI.Label(new Rect(10, 10, 200, 20), ("Player's health: " + _health.ToString()));
		//}

		public override void SetDamage(float damage)
		{
			_health -= damage;
		}

		public void UpdatePlayer(SceneData sceneData) //Temp
		{
			base.MaxHealth = _maxHealth;
			base.Health = _health;
			if (RayCast()) sceneData.Triggers.Add(RayCast());
			if (_health < 1)
			{
				Die(sceneData);
			}

		}

		private GameObject RayCast()
		{
			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hitInfo = new RaycastHit();
			Debug.DrawLine(transform.position, transform.forward, Color.red);
			if (Physics.Raycast(ray, out hitInfo, _raycastDepth) && hitInfo.collider.isTrigger)
			{
				print("I had hit a trigger!");
				return hitInfo.collider.gameObject;
			}
			else
			{
				return null;
			}
		}

		private void Die(SceneData sceneData)
		{
			sceneData.CurrentState = GameState.Lose;
			this.GetComponent<AudioSource>().clip = null;
			int _childCount = gameObject.transform.childCount;
			for (int i = 0; i < _childCount; i++)
			{
				var _child = gameObject.transform.GetChild(i);
				if (!_child.gameObject.active) _child.gameObject.SetActive(true); 
			}

			for (int i = 0; i < _childCount; i++)
			{
				var _child = gameObject.transform.GetChild(i);
				if (!_child.gameObject.active) _child.gameObject.SetActive(true);
			}
			base.IsActive = false;
		}

		public override void Init()
		{
			base.IsActive = true;
		}

	}
}
