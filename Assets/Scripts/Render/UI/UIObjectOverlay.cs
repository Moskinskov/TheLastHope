/// Limerence Games
/// The Last Hope
/// Curator: Dmitri Kuzmin
/// to be commented

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Render;
using UnityEngine.UI;
using TheLastHope;
using TheLastHope.Management;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.UI {

	/// <summary>
	/// A controllable group of interface elements
	/// that could be shown around an object.
	/// </summary>
	public class UIObjectOverlay : MonoBehaviour
	{
		#region Serializables
		[SerializeField]
		private Canvas frame;
		[SerializeField]
		private Canvas healthBar;
		[SerializeField]
		private Canvas ammoBar;
		[SerializeField]
		private Canvas reloadBar;
		[SerializeField]
		private Canvas buttonBar;
		[SerializeField]
		private Image healthBarImage;
		[SerializeField]
		#endregion

		#region 
		private Image ammoBarImage;
		private float currentHealth;
		private float currentAmmo = 0;
		private Vector3 overlaySize;

		/// <summary>
		/// Current health level.
		/// </summary>
		public float CurrentHealth {set => currentHealth = value; }
		public Vector3 OverlaySize { get => overlaySize; set => overlaySize = value; }
		public float CurrentAmmo { set => currentAmmo = value; }

		private void getInfo()
		{	
				healthBarImage.fillAmount = currentHealth;
				//_ammoBarImage.fillAmount = _currentAmmo;
		}

		public void ShowOverlay(bool frame)
		{
			getInfo();
			if (frame) { this.frame.enabled = true; }
		}

		public void ShowOverlay(bool frame, bool health)
		{
			getInfo();
			if (frame) { this.frame.enabled = true; }
			if (health) { healthBar.enabled = true; }
		}
		public void ShowOverlay(bool frame, bool health, bool button)
		{
			getInfo();
			if (frame) { this.frame.enabled = true; }
			if (health) { healthBar.enabled = true; }
			if (button) { buttonBar.enabled = true; }
		}

		public void ShowOverlay(bool frame, bool health, bool button, bool ammo)
		{
			getInfo();
			if (frame) { this.frame.enabled = true; }
			if (health) { healthBar.enabled = true; }
			if (button) { buttonBar.enabled = true; }
			//if (ammo) { _ammoBar.enabled = true;  }
		}

		public void ShowOverlay(bool frame, bool health, bool button, bool ammo, bool reload)
		{
			getInfo();
			if (frame) { this.frame.enabled = true; }
			if (health) { healthBar.enabled = true; }
			if (button) { buttonBar.enabled = true; }
			//if (ammo) { _ammoBar.enabled = true; }
			//if (reload) { _reloadBar.enabled = true; }
		}

		public void HideOverlay()
		{
			frame.enabled = false;
			healthBar.enabled = false;
			buttonBar.enabled = false;
			//if (_ammoBar) _ammoBar.enabled = false;
		}

        #endregion
    }
}