using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Render;
using UnityEngine.UI;
using TheLastHope;
using TheLastHope.Management;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.UI {
	public class UIObjectOverlay : MonoBehaviour
	{
		[SerializeField]
		private Canvas _frame;
		[SerializeField]
		private Canvas _healthBar;
		[SerializeField]
		private Canvas ammoPrefabBar;
		[SerializeField]
		private Canvas _reloadBar;
		[SerializeField]
		private Canvas _buttonBar;
		[SerializeField]
		private Image _healthBarImage;
		[SerializeField]
		private Image ammoPrefabBarImage;
		private float _currentHealth;
		private float _currentAmmo = 0;
		private Vector3 _overlaySize;

		public float CurrentHealth {set => _currentHealth = value; }
		public Vector3 OverlaySize { get => _overlaySize; set => _overlaySize = value; }
		public float CurrentAmmo { set => _currentAmmo = value; }

		private void getInfo()
		{	
				_healthBarImage.fillAmount = _currentHealth;
				//ammoPrefabBarImage.fillAmount = _currentAmmo;
		}

		public void ShowOverlay(bool frame)
		{
			getInfo();
			if (frame) { _frame.enabled = true; }
		}

		public void ShowOverlay(bool frame, bool health)
		{
			getInfo();
			if (frame) { _frame.enabled = true; }
			if (health) { _healthBar.enabled = true; }
		}
		public void ShowOverlay(bool frame, bool health, bool button)
		{
			getInfo();
			if (frame) { _frame.enabled = true; }
			if (health) { _healthBar.enabled = true; }
			if (button) { _buttonBar.enabled = true; }
		}

		public void ShowOverlay(bool frame, bool health, bool button, bool ammo)
		{
			getInfo();
			if (frame) { _frame.enabled = true; }
			if (health) { _healthBar.enabled = true; }
			if (button) { _buttonBar.enabled = true; }
			//if (ammo) { ammoPrefabBar.enabled = true;  }
		}

		public void ShowOverlay(bool frame, bool health, bool button, bool ammo, bool reload)
		{
			getInfo();
			if (frame) { _frame.enabled = true; }
			if (health) { _healthBar.enabled = true; }
			if (button) { _buttonBar.enabled = true; }
			//if (ammo) { ammoPrefabBar.enabled = true; }
			//if (reload) { _reloadBar.enabled = true; }
		}

		public void HideOverlay()
		{
			_frame.enabled = false;
			_healthBar.enabled = false;
			_buttonBar.enabled = false;
			//if (ammoPrefabBar) ammoPrefabBar.enabled = false;
		}


	}
}