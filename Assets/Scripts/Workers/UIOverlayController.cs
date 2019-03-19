﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Player;
using TheLastHope.Management.Data;
using TheLastHope.Helpers;

namespace TheLastHope.UI {

	public class UIOverlayController : MonoBehaviour
	{
		private float _health;
		private ABaseObject _baseObject;
		private float _startHealth;
		private float _healthBarValue;
		private UIObjectOverlay _overlay;
		private ObjType _currentType;
		private Vector3 _overlaySize;
		private bool _isUnderControl;
		private float _oldHealth;
		private Timer _timer;
		private IEnumerator coroutine;

		[SerializeField]
		private bool _showOnDamage;
		[SerializeField]
		private float _showTime = 1.0f;

		public void Init()
		{
			_baseObject = GetComponentInChildren<ABaseObject>();
			if (!_baseObject) GetComponent<ABaseObject>();
			_healthBarValue = _baseObject.MaxHealth;
			_overlay = this.gameObject.GetComponentInChildren<UIObjectOverlay>();
			_oldHealth = _baseObject.Health;

			if (_baseObject.GetComponentInChildren<AEnemy>()) _currentType = ObjType.Enemy;
			else if (_baseObject.GetComponentInChildren<ATurret>()) _currentType = ObjType.Turret;
			else if (_baseObject.GetComponentInChildren<MainPlayer>()) _currentType = ObjType.Loco;
			HideOverlay();
			_timer = new Timer();
		}

		public void OverlayUpdate()
		{
			if (_baseObject.Health != _oldHealth && _showOnDamage && _baseObject.IsActive && !_isUnderControl)
			{
				coroutine = DamageUI(_showTime);
				StartCoroutine(coroutine);
			}
		}


		public void CountHealth()
		{
			_healthBarValue = _baseObject.Health / _baseObject.MaxHealth;
			_overlay.CurrentHealth = _healthBarValue;
		}


		private void OnMouseOver()
		{
			//print("Mouse Over!");
			CountHealth();
			ShowOverlay();
		}

		private void OnMouseExit()
		{
			if (!_isUnderControl) HideOverlay();
			//print("Mouse Exit!");
		}

		public void ShowOverlay()
		{
			if (_currentType == ObjType.Enemy) _overlay.ShowOverlay(true, true);
			else if (_currentType == ObjType.Loco) _overlay.ShowOverlay(true, true);
			else if (_currentType == ObjType.Turret)
			{
				if (_baseObject.GetComponentInChildren<ATurret>().soft.canBeManual) _overlay.ShowOverlay(true, true, true);
				else _overlay.ShowOverlay(true, true);
			}
		}

		public void HideOverlay()
		{
			if (_overlay) _overlay.HideOverlay();
			else print("Overlay not found!");
		}

		public void ControlButton()
		{
			if (_currentType == ObjType.Turret && !_isUnderControl && _baseObject.GetComponentInChildren<ATurret>().soft.canBeManual)
			{
				_isUnderControl = true;
				_baseObject.GetComponentInChildren<ATurret>().SwitchMode();
			}

			else
			{
				_isUnderControl = false;
				_baseObject.GetComponentInChildren<ATurret>().SwitchMode();
			}
		}

		private IEnumerator DamageUI(float waitTime)
		{
			CountHealth();
			ShowOverlay();
			yield return new WaitForSeconds(waitTime);
			HideOverlay();
			_oldHealth = _baseObject.Health;
		}
	}

}
