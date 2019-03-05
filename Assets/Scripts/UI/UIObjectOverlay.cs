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
		private ABaseObject _baseObject;
		[SerializeField]
		private Canvas _frame;
		[SerializeField]
		private Canvas _healthBar;
		[SerializeField]
		private Canvas _buttonBar;
		[SerializeField]
		private Image _healthBarImage;
		private float _startHealth;
		private float _currentHealth;

		public void Init()
		{
			_baseObject = this.gameObject.GetComponent<ABaseObject>();
			_startHealth = _baseObject.Health;
			_currentHealth = _startHealth;
		}

		private void getHealth()
		{	
			if (_baseObject)
			{
				_healthBarImage.fillAmount = _currentHealth / _startHealth; 
			}
		}

		public void ShowOverlay(bool frame)
		{
			if (frame) { _frame.enabled = true; }
		}

		public void ShowOverlay(bool frame, bool health)
		{
			getHealth();
			if (frame) { _frame.enabled = true; }
			if (health) { _healthBar.enabled = true; }
		}
		public void ShowOverlay(bool frame, bool health, bool button)
		{
			getHealth();
			if (frame) { _frame.enabled = true; }
			if (health) { _healthBar.enabled = true; }
			if (button) { _buttonBar.enabled = true;	 }
		}


	}
}