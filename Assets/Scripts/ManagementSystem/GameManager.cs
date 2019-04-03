/// Limerence Games
/// The Last Hope
/// Curator: Sergey Aydarov
/// to be commented

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
using UnityEngine.SceneManagement;

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
        GameObject playerTrain;
        [SerializeField] int credits;
        Player player;
        [SerializeField] HangarData hangar;
        [SerializeField] GameObject panelHangar;

        // Start is called before the first frame update
        void Start()
        {
            SaveLoadManager.objectsDictionary = FindObjectOfType<ObjectDictionary>();
            SaveLoadManager.Load(out playerTrain, out player);
            sceneData = new SceneData();
            mainPlayer = playerTrain.GetComponentInChildren<MainPlayer>();
            trainManager = playerTrain.GetComponentInChildren<TrainManager>();
            trainManager.Init(sceneData);
            mainPlayer.Init();
            sceneData.TargetEnemyCount = targetEnemyCount;
            sceneData.TargetPropsCount = targetPropsCount;
            sceneData.TrainSpeed = trainSpeed;
            sceneData.LineLength = lineLength;
            sceneData.CurrentLevel = currentLevel;
            sceneData.LinesCount = linesCount;
            sceneData.Player = player;
            worldMover.SetupMover(sceneData);
            generatorManager.Init(sceneData);
            renderManager.Init();
            triggerManager.Init(generatorManager);
            hangar.Init(sceneData);
            

            trainStuffAdd();
            if (skillManager != null)
            {
                skillManager.Init();
            }
            pathCounter.Init(sceneData);
			sceneData.LinesOverall = generatorManager.LevelLenght;
            GenerateFirstArea();
			//sceneData.CurrentState = GameState.Start;
			sceneData.CurrentState = GameState.Preroll;
            sceneData.CurrentState = GameState.Wait;
			//sceneData.CurrentState = GameState.Loop;
            //SaveLoadManager.SavePlayer(playerTrain, sceneData.Player); TEMPORARY OFF.
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
                foreach(var trigger in sceneData.Triggers)
                {
                    print($"T {trigger.name}");
                }
				triggerManager.ExecuteCurrentEvents(sceneData);
				//******************************************//
				//Temp Progress Bar
				//******************************************//
                trainManager.UpdateTrain(sceneData);
                foreach (var enemy in sceneData.Enemies)
                {
                    enemy.GetComponent<AEnemy>().EnemyUpdate(sceneData, Time.deltaTime);
                }

                destroyer.Destroy(sceneData);
                worldMover.MoveWorld(sceneData, Time.deltaTime);
                mainPlayer.UpdatePlayer(sceneData);
            }

			else if (sceneData.CurrentState == GameState.Lose)
			{
				EndGame(false, sceneData);
				generatorManager.UpdateGenerators(sceneData);
                foreach (var enemy in sceneData.Enemies)
                {
                    enemy.GetComponent<AEnemy>().EnemyUpdate(sceneData, Time.deltaTime);
                }

                destroyer.Destroy(sceneData);
                worldMover.MoveWorld(sceneData, Time.deltaTime);
                mainPlayer.UpdatePlayer(sceneData);
            }
			else if (sceneData.CurrentState == GameState.Win)
			{
				EndGame(true, sceneData);
                foreach (var enemy in sceneData.Enemies)
                {
                    enemy.GetComponent<AEnemy>().EnemyUpdate(sceneData, Time.deltaTime);
                }

                destroyer.Destroy(sceneData);
                worldMover.MoveWorld(sceneData, Time.deltaTime);
                mainPlayer.UpdatePlayer(sceneData);
            }
            else if (sceneData.CurrentState == GameState.Start)
            {
                panelHangar.SetActive(false);
                weaponController.Init(sceneData);
                uiManager.Init(sceneData);
                sceneData.CurrentState = GameState.Loop;
            }
            else if (sceneData.CurrentState == GameState.Wait)
            {
                
                panelHangar.SetActive(true);
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
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
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
            sceneData.CurrentState = GameState.Start;
        }
    }
}

