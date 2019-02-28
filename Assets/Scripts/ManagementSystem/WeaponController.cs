using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Management;
using System.Collections.Generic;
using TheLastHope.Weapons.Software;
namespace TheLastHope.Weapons
{
	public class WeaponController : MonoBehaviour
	{
		[SerializeField] Selector selector;
		[SerializeField] List<TurretController> listTurretControllers = new List<TurretController>();
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
				currentManualTurret.GetComponentInChildren<TurretController>().soft.SwitchMode();
				currentManualTurret = null;
			}
			if (Input.GetButtonDown("Fire1") &&
				selectedTurret.GetComponentInChildren<TurretController>() != null &&
				selectedTurret != currentManualTurret)
			{
				if (currentManualTurret != null)
				{
					currentManualTurret.GetComponentInChildren<TurretController>().soft.SwitchMode();
				}
				else
				{
					currentManualTurret = selectedTurret;
				}
				selectedTurret.GetComponentInChildren<TurretController>().soft.SwitchMode();

			}
			foreach (var turret in listTurretControllers)
			{
				turret.TurUpdate(sceneData, deltaTime);
			}
		}
	}
}