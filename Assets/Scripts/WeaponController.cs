using UnityEngine;
using TheLastHope.Data;
using System.Collections.Generic;
namespace TheLastHope.Weapons
{
	public class WeaponController : MonoBehaviour
	{
		[SerializeField] Selector selector;
		[SerializeField] List<ATurretController> listTurretControllers = new List<ATurretController>();
		GameObject selectedTurret = null;
		GameObject currentManualTurret = null;

		public void Init()
		{
			foreach (var turret in listTurretControllers)
			{
				turret.gameObject.AddComponent<AutoAndManualSoft>();
				turret.soft = turret.gameObject.GetComponent<AutoAndManualSoft>();
				turret.soft.Init(0f); //MAGIC NUMBER
			}
		}

		/// <summary>
		/// Turns and fires turrets. Also switches manual turret.
		/// </summary>
		/// <param name="sceneData"></param>
		/// <param name="deltaTime"></param>
		public void UpdateWeapons(SceneData sceneData, float deltaTime)
		{

			selectedTurret = selector.GetSelectedGameObject();
			if (Input.GetButtonDown("Fire2"))
			{
				currentManualTurret.GetComponentInChildren<ATurretController>().soft.SwitchMode();
				currentManualTurret = null;
			}
			if (Input.GetButtonDown("Fire1") &&
				selectedTurret.GetComponentInChildren<ATurretController>() != null &&
				selectedTurret != currentManualTurret)
			{
				if (currentManualTurret != null)
				{
					currentManualTurret.GetComponentInChildren<ATurretController>().soft.SwitchMode();
				}
				else
				{
					currentManualTurret = selectedTurret;
				}
				selectedTurret.GetComponentInChildren<ATurretController>().soft.SwitchMode();

			}
			foreach (var turret in listTurretControllers)
			{
				turret.TurUpdate(sceneData, deltaTime);
			}
		}
	}
}