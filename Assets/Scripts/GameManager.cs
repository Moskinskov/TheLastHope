using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Generators;
using TheLastHope.Data;
using TheLastHope.Weapons;

public class GameManager : MonoBehaviour
{

	SceneData sceneData;
	[SerializeField] AGenerator railGen;
	[SerializeField] AGenerator propsGen;
	[SerializeField] AGenerator enemyGen;
	[SerializeField] int targetEnemyCount;
	[SerializeField] int targetPropsCount;
	[SerializeField] float trainSpeed;
	[SerializeField] AWorldMover worldMover;
	[SerializeField] ADestroyer destroyer;
	[SerializeField] GameObject debugEnemy;
	[SerializeField] GameObject debugEnemyPattern;
	//[SerializeField] GameObject debugProps;
	List<Vector3> patternPositions;
	[SerializeField] GameObject manualTurret;
	[SerializeField] Camera camera;
	[SerializeField] WeaponController weaponController;


	// Start is called before the first frame update
	void Start()
	{
		sceneData = new SceneData();
		sceneData.targetEnemyCount = targetEnemyCount;
		sceneData.targetPropsCount = targetPropsCount;
		sceneData.trainSpeed = trainSpeed;
		sceneData.enemies.Add(debugEnemy);
		sceneData.enemiesPatterns.Add(debugEnemyPattern);
		debugEnemy.GetComponent<BikeEnemy>().Initialize();
		patternPositions = new List<Vector3>();
		patternPositions.Add(debugEnemyPattern.transform.position);
		worldMover.SetupMover(sceneData);
	}

	// Update is called once per frame
	void Update()
	{
		manualTurret.GetComponent<ManualTurretPlatform>().TurnTurret(GetMousePosIn3D(manualTurret), Time.deltaTime);
		sceneData.trainSpeed = trainSpeed;
		foreach (var enemy in sceneData.enemies)
		{
			enemy.GetComponent<BikeEnemy>().Move(sceneData, Time.deltaTime);
		}
		//if (sceneData.ammos.Count > 0)
		//{
		//	foreach (var ammo in sceneData.ammos)
		//	{
		//		if (ammo)
		//		{
		//			ammo.GetComponent<AAmmo>().Move(Time.deltaTime);
		//		}
		//	}
		//}
		destroyer.Destroy(sceneData);
		railGen.Generate(sceneData);
		propsGen.Generate(sceneData);
		enemyGen.Generate(debugEnemy, sceneData, patternPositions);
		worldMover.MoveWorld(sceneData, Time.deltaTime);
		weaponController.UpdateWeapons(Input.GetMouseButton(0));
	}

	Vector3 GetMousePosIn3D(GameObject gameObject)
	{
		//Plane objPlane = new Plane(Vector3.up, gameObject.transform.position);
		Ray ray = camera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane));
		RaycastHit raycastHit;
		Physics.Raycast(ray, out raycastHit);
		return raycastHit.point;
	}

}
