using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Player;
using TheLastHope.Management.Data;

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

		[SerializeField]
		private bool _showOnDamage;

		public void Init()
		{
			_baseObject = GetComponentInChildren<ABaseObject>();
			_healthBarValue = _baseObject.MaxHealth;
			_overlay = this.gameObject.GetComponentInChildren<UIObjectOverlay>();

			if (_baseObject.GetComponentInChildren<AEnemy>()) _currentType = ObjType.Enemy;
			else if (_baseObject.GetComponentInChildren<ATurret>()) _currentType = ObjType.Turret;
			else if (_baseObject.GetComponentInChildren<MainPlayer>()) _currentType = ObjType.Loco;
			HideOverlay();
		}

		public void CountHealth()
		{
			print(_baseObject.Health);
			_healthBarValue = _baseObject.Health / _baseObject.MaxHealth;
			_overlay.CurrentHealth = _healthBarValue;
			print("Current Health: " + _healthBarValue + " as a result of " + _baseObject.Health + " / " + _baseObject.MaxHealth);
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
	}

}
