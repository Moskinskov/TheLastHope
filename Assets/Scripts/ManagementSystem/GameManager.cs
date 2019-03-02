using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Weapons;
using TheLastHope.Hippo;
using TheLastHope.Helpers;
using TheLastHope.Management.AbstractLayer;

namespace TheLastHope.Management
{
    public class GameManager : Singleton<GameManager>
    {
        SceneData sceneData;
        [SerializeField] GeneratorManager generatorManager;
        [SerializeField] int targetEnemyCount;
        [SerializeField] int targetPropsCount;
        [SerializeField] float trainSpeed;
        [SerializeField] AWorldMover worldMover;
        [SerializeField] ADestroyer destroyer;
        [SerializeField] WeaponManager weaponController;
        [SerializeField] HippoMainPlayer mainPlayer;
		[SerializeField] RenderManager renderManager;
        [SerializeField] SkillManager skillManager;
        // Start is called before the first frame update
        void Start()
        {
            sceneData = new SceneData();
            sceneData.TargetEnemyCount = targetEnemyCount;
            sceneData.TargetPropsCount = targetPropsCount;
            sceneData.TrainSpeed = trainSpeed;
            worldMover.SetupMover(sceneData);
            generatorManager.Initialize(sceneData);
            weaponController.Init();
			renderManager.Init();
            if (skillManager != null)
            {
                skillManager.Init();
            }
        }

        // Update is called once per frame
        void Update()
        {
            generatorManager.UpdateGenerators(sceneData);
            foreach (var enemy in sceneData.Enemies)
            {
                enemy.GetComponent<AEnemy>().Move(sceneData, Time.deltaTime);
            }
            destroyer.Destroy(sceneData);
            worldMover.MoveWorld(sceneData, Time.deltaTime);
            weaponController.UpdateWeapons(sceneData, Time.deltaTime);
            mainPlayer.UpdatePlayer(sceneData);
			renderManager.UpdateRender(sceneData);
            if (skillManager != null)
            {
                skillManager.SkillUpdate(sceneData);
            }
        }

	
	}
}

