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
		private Canvas _buttonBar;
		[SerializeField]
		private Image _healthBarImage;
		private float _currentHealth;
		private Vector3 _overlaySize;

		public float CurrentHealth {set => _currentHealth = value; }
		public Vector3 OverlaySize { get => _overlaySize; set => _overlaySize = value; }

		private void getInfo()
		{	
				_healthBarImage.fillAmount = _currentHealth;
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

		public void HideOverlay()
		{
			_frame.enabled = false;
			_healthBar.enabled = false;
			_buttonBar.enabled = false; 
		}


	}
}