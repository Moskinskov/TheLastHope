using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Weapons;
using TheLastHope.Player;
//using TheLastHope.Hippo; // Temporary. For the Hippo needs.
using TheLastHope.Helpers;
using TheLastHope.Management.AbstractLayer;
using System;
using TheLastHope.Hangar;

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
        [SerializeField] MainPlayer mainPlayer;
        [SerializeField] RenderManager renderManager;
        [SerializeField] SkillManager skillManager;
        [SerializeField] TriggerManager triggerManager;
        [SerializeField] TrainManager trainManager;
		[SerializeField] UIManager uiManager;
        [SerializeField] float lineLength;
        [SerializeField] int linesCount;
        [SerializeField] string currentLevel;
		[SerializeField] Canvas winCanvas;
		[SerializeField] Canvas looseCanvas;
        [Tooltip("Number of lines to pregenerate scene")] 
        [SerializeField] int firstFrameLengthInLines = 14;
        [SerializeField] HangarData hangar;

        [SerializeField] int PlayerStartCredit;

        // Start is called before the first frame update
        void Start()
        {
            sceneData = new SceneData();
            sceneData.TargetEnemyCount = targetEnemyCount;
            sceneData.TargetPropsCount = targetPropsCount;
            sceneData.TrainSpeed = trainSpeed;
            sceneData.LineLength = lineLength;
            sceneData.CurrentLevel = currentLevel;
            sceneData.LinesCount = linesCount;
            sceneData.Player = new Player();
            sceneData.Player.Credit = PlayerStartCredit;
            worldMover.SetupMover(sceneData);
            generatorManager.Init(sceneData);
            renderManager.Init();
            trainManager.Init(sceneData);
            triggerManager.Init(generatorManager);

			mainPlayer.Init();

            if (skillManager != null)
            {
                skillManager.Init();
            }
            pathCounter.Init(sceneData);
			sceneData.LinesOverall = generatorManager.LevelLenght;
            GenerateFirstArea();
			//sceneData.CurrentState = GameState.Start;
			sceneData.CurrentState = GameState.Preroll;
			//ceneData.CurrentState = GameState.Loop;
		}

		private void trainStuffAdd()
		{
			foreach (ABaseObject tur in weaponController.TurretList)
			{
				sceneData.TrainStuff.Add(tur);
				print(sceneData.TrainStuff);
			}

			sceneData.TrainStuff.Add(mainPlayer);
		}

		// Update is called once per frame
		void Update()
        {


			if (sceneData.CurrentState == GameState.Loop)
			{
				generatorManager.UpdateGenerators(sceneData);
				weaponController.UpdateWeapons(sceneData, Time.deltaTime);
				renderManager.UpdateRender(sceneData);
				uiManager.UIUpdate(sceneData, pathCounter);
				if (skillManager != null)
				{
					skillManager.SkillUpdate(sceneData);
				}
				pathCounter.CountLenght(sceneData, Time.deltaTime);
				triggerManager.ExecuteCurrentEvents(sceneData);
				//******************************************//
				//Temp Progress Bar
				//******************************************//
                trainManager.UpdateTrain(sceneData);
                destroyer.Destroy(sceneData);
                worldMover.MoveWorld(sceneData, Time.deltaTime);
                mainPlayer.UpdatePlayer(sceneData);
                foreach (var enemy in sceneData.Enemies)
                {
                    enemy.GetComponent<AEnemy>().EnemyUpdate(sceneData, Time.deltaTime);
                }
            }

			else if (sceneData.CurrentState == GameState.Lose)
			{
				EndGame(false, sceneData);
				generatorManager.UpdateGenerators(sceneData);
                destroyer.Destroy(sceneData);
                worldMover.MoveWorld(sceneData, Time.deltaTime);
                mainPlayer.UpdatePlayer(sceneData);
                foreach (var enemy in sceneData.Enemies)
                {
                    enemy.GetComponent<AEnemy>().EnemyUpdate(sceneData, Time.deltaTime);
                }
            }
			else if (sceneData.CurrentState == GameState.Win)
			{
				EndGame(true, sceneData);
                destroyer.Destroy(sceneData);
                worldMover.MoveWorld(sceneData, Time.deltaTime);
                mainPlayer.UpdatePlayer(sceneData);
                foreach (var enemy in sceneData.Enemies)
                {
                    enemy.GetComponent<AEnemy>().EnemyUpdate(sceneData, Time.deltaTime);
                }
            }
            else if (sceneData.CurrentState == GameState.Preroll)
            {

            }
            else if (sceneData.CurrentState == GameState.Start)
            {
                hangar.SetInactive();               
                weaponController.Init();
                uiManager.Init(sceneData);
                trainStuffAdd();
                sceneData.CurrentState = GameState.Loop;
            }




        }

        void EndGame(bool win, SceneData sceneData)
        {
            if (win)
            {
                var _oldSpeed = sceneData.TrainSpeed;
                sceneData.TrainSpeed = Mathf.Lerp(_oldSpeed, 0, Time.deltaTime);
				print("You win!");
				winCanvas.gameObject.active = true;
				foreach (GameObject enemy in sceneData.Enemies)
				{
					var enemyController = enemy.GetComponent<AEnemy>();
					enemyController.Health = 0;
				}
			}
            else
            {
                var _oldSpeed = sceneData.TrainSpeed;
                sceneData.TrainSpeed = Mathf.Lerp(_oldSpeed, 0, Time.deltaTime);
				print("You lose!");
				looseCanvas.gameObject.active = true;
			}
        }
		
		public void StartOver()
		{
			Application.LoadLevel("MainScene");
		}

		public void Exit()
		{
			Application.Quit();
		}

        void GenerateFirstArea()
        {
            for(var i=0; i<firstFrameLengthInLines;i++)
            {
                generatorManager.UpdateGenerators(sceneData);
                worldMover.MoveWorld(sceneData, sceneData.LineLength / sceneData.TrainSpeed);
                pathCounter.CountLenght(sceneData, sceneData.LineLength / sceneData.TrainSpeed);
            }
        }

        public void RunScene()
        {
            hangar.SetInactive();
            sceneData.CurrentState = GameState.Start;
        }
    }
}

