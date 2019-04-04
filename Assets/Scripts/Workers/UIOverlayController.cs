/// Limerence Games
/// The Last Hope
/// Curator: Dmitri Kuzmin
/// to be commented

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Player;
using TheLastHope.Management.Data;
using TheLastHope.Management;
using TheLastHope.Helpers;
using TheLastHope.Weapons;

namespace TheLastHope.UI {

	public class UIOverlayController : MonoBehaviour
	{
		private float _health;
		private ABaseObject _baseObject;
		private float _startHealth;
		private float _healthBarValue;
		private float _ammoBarValue;
		private UIObjectOverlay _overlay;
		private ObjType _currentType;
		private Vector3 _overlaySize;
		private bool _isUnderControl;
		private float _oldHealth;
		private Timer _timer;
		private IEnumerator coroutine;
        GameState gameState = GameState.Wait;

		[SerializeField]
		private bool _showOnDamage;
		[SerializeField]
		private float _showTime = 1.0f;

		public void Init()
		{
			_baseObject = GetComponentInChildren<ABaseObject>();
			if (!_baseObject) GetComponent<ABaseObject>();
			_healthBarValue = _baseObject.MaxHealth;
			_overlay = GetComponentInChildren<UIObjectOverlay>();
			//if (_overlay) print(gameObject.name + " has an overlay!");
			_oldHealth = _baseObject.Health;

			if (_baseObject.GetComponentInChildren<AEnemy>()) _currentType = ObjType.Enemy;
			else if (_baseObject.GetComponentInChildren<ATurret>()) _currentType = ObjType.Turret;
			else if (_baseObject.GetComponent<MainPlayer>()) _currentType = ObjType.Loco;
			//print("I am " + _baseObject.name + " and my type is " + _currentType);
			HideOverlay();
			_timer = new Timer();
		}

		public void OverlayUpdate(SceneData sceneData)
		{
			if ((_baseObject.Health != _oldHealth) && _showOnDamage && _baseObject.IsActive)
			{
				coroutine = DamageUI(_showTime);
				StartCoroutine(coroutine);
			}

			if (_currentType == ObjType.Turret)
			{
				CountAmmo();
				ShowOverlay();
			}
            gameState = sceneData.CurrentState;
		}


		public void CountHealth()
		{
			if (_baseObject)
			{
				_healthBarValue = _baseObject.Health / _baseObject.MaxHealth;
				//print("Buggy Health is " + _healthBarValue);
				_overlay.CurrentHealth = _healthBarValue;
			}
		}

		public void CountAmmo()
		{
			if (_baseObject && _currentType == ObjType.Turret)
			{
				//_ammoBarValue = _baseObject.GetComponentInChildren<ARangedWeapon>().CurrentAmmoInClip / _baseObject.GetComponentInChildren<ARangedWeapon>().ClipSize;
				//_overlay.CurrentAmmo = _ammoBarValue;
			}
		}


		private void OnMouseOver()
		{
            //print("Mouse Over!");
            if (gameState==GameState.Loop)
            {
                CountHealth();
                ShowOverlay();
            }

		}

		private void OnMouseExit()
		{
			if (!_isUnderControl && gameState == GameState.Loop) HideOverlay();
			//print("Mouse Exit!");
		}

		private void OnMouseDown()
		{
			//ControlButton();
		}

		public void ShowOverlay()
		{
            if (_currentType == ObjType.Enemy) _overlay.ShowOverlay(true, true);
            else if (_currentType == ObjType.Loco) _overlay.ShowOverlay(true, true); 
			else if (_currentType == ObjType.Turret)
			{
				_overlay.ShowOverlay(true, true, true, true);
				if (_ammoBarValue == 0) _overlay.ShowOverlay(true, true, true, true, true);
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
