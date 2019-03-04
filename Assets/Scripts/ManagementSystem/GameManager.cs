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
    /// <summary>
    /// Class that controls whole scene.
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        SceneData sceneData;
        PathLengthCounter pathCounter = new PathLengthCounter();
        [SerializeField] GeneratorManager generatorManager;
        [SerializeField] int targetEnemyCount;
        [SerializeField] int targetPropsCount;
        [SerializeField] float trainSpeed;
        [SerializeField] AWorldMover worldMover;
        [SerializeField] ADestroyer destroyer;
        [SerializeField] WeaponManager weaponController;
        [SerializeField] HippoMainPlayer hippoPlayer;
        [SerializeField] RenderManager renderManager;
        [SerializeField] SkillManager skillManager;
        [SerializeField] TriggerManager triggerManager;
        [SerializeField] float lineLength;
        [SerializeField] string currentLevel;

        // Start is called before the first frame update
        void Start()
        {
            sceneData = new SceneData();
            sceneData.TargetEnemyCount = targetEnemyCount;
            sceneData.TargetPropsCount = targetPropsCount;
            sceneData.TrainSpeed = trainSpeed;
            sceneData.LineLength = lineLength;
            sceneData.CurrentLevel = currentLevel;
            worldMover.SetupMover(sceneData);
            generatorManager.Initialize(sceneData);
            weaponController.Init();
            renderManager.Init();
            triggerManager.Init(generatorManager);
            if (skillManager != null)
            {
                skillManager.Init();
            }
            pathCounter.Init(sceneData);
			sceneData.CurrentState = GameState.Start;
			sceneData.CurrentState = GameState.Preroll;
			sceneData.CurrentState = GameState.Loop;
		}

        // Update is called once per frame
        void Update()
        {
			foreach (var enemy in sceneData.Enemies)
			{
				enemy.GetComponent<AEnemy>().EnemyUpdate(sceneData, Time.deltaTime);
			}

			destroyer.Destroy(sceneData);
			worldMover.MoveWorld(sceneData, Time.deltaTime);
			hippoPlayer.UpdatePlayer(sceneData);

			if (sceneData.CurrentState == GameState.Loop)
			{
				generatorManager.UpdateGenerators(sceneData);
				weaponController.UpdateWeapons(sceneData, Time.deltaTime);
				renderManager.UpdateRender(sceneData);
				if (skillManager != null)
				{
					skillManager.SkillUpdate(sceneData);
				}
				pathCounter.CountLenght(sceneData, Time.deltaTime);
				triggerManager.ExecuteCurrentEvents(sceneData);
			}

            else if (sceneData.CurrentState == GameState.Lose)
			{
				EndGame(false, sceneData);
				generatorManager.UpdateGenerators(sceneData);
			}
			else if (sceneData.CurrentState == GameState.Win)
			{
				EndGame(true, sceneData);
				foreach (var enemy in sceneData.Enemies)
				{
					enemy.GetComponent<AEnemy>().Die();
				}
			}
		}

        void EndGame(bool win, SceneData sceneData)
        {
            if (win)
            {
                var _oldSpeed = sceneData.TrainSpeed;
                sceneData.TrainSpeed = Mathf.Lerp(_oldSpeed, 0, Time.deltaTime);
				print("You win!");
            }
            else
            {
                var _oldSpeed = sceneData.TrainSpeed;
                sceneData.TrainSpeed = Mathf.Lerp(_oldSpeed, 0, Time.deltaTime);
				print("You lose!");
			}
        }   
    }
}

