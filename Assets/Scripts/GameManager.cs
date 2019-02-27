using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Generators;
using TheLastHope.Data;
using TheLastHope.Weapons;
using TheLastHope.Hippo;
using TheLastHope.Helpers;

public class GameManager : Singleton<GameManager>
{
	SceneData sceneData;
    [SerializeField] GeneratorManager generatorManager;
	[SerializeField] int targetEnemyCount;
	[SerializeField] int targetPropsCount;
	[SerializeField] float trainSpeed;
	[SerializeField] AWorldMover worldMover;
	[SerializeField] ADestroyer destroyer;
	[SerializeField] WeaponController weaponController;
	[SerializeField] HippoMainPlayer mainPlayer;

	// Start is called before the first frame update
	void Start()
	{
		sceneData = new SceneData();
		sceneData.targetEnemyCount = targetEnemyCount;
		sceneData.targetPropsCount = targetPropsCount;
		sceneData.trainSpeed = trainSpeed;
		worldMover.SetupMover(sceneData);
        generatorManager.Initialize(sceneData);
		weaponController.Init();
	}

	// Update is called once per frame
	void Update()
	{
        generatorManager.UpdateGenerators(sceneData);
		sceneData.trainSpeed = trainSpeed;
		foreach (var enemy in sceneData.enemies)
		{
			enemy.GetComponent<AEnemy>().Move(sceneData, Time.deltaTime);
		}
		destroyer.Destroy(sceneData);	
		worldMover.MoveWorld(sceneData, Time.deltaTime);
		weaponController.UpdateWeapons(sceneData,Time.deltaTime);
		mainPlayer.UpdatePlayer(sceneData);
		

		print(sceneData.trainSpeed);
	}
}
