using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using TheLastHope.Enemies;
using TheLastHope.Helpers;

namespace TheLastHope.Management
{
	public class GeneratorManager : Singleton<GeneratorManager>
	{
		[SerializeField] AGenerator enemyGen;
		[SerializeField] AGenerator staticGen;
		[SerializeField] AGenerator railsGen;
		[SerializeField] ObjectDictionary objDictionary;
		[SerializeField] List<GameObject> enemies;
		[SerializeField] List<GameObject> enemyPatterns;
		[Header("Quantity of patterns being generated per tick"), Tooltip("Pattern quantity.")]
		[SerializeField] int linesCount;
		//[SerializeField] int layersCount;
		List<Vector3> patternPositions;
		GameObject[] enemiesArray;
		GameObject[] groundArray;
		GameObject[] staticArray;

		int currentLine = -1;
		LevelReader levelReader;
		[SerializeField] char divider = ',';


		public void Initialize(SceneData sceneData)
		{
			enemiesArray = new GameObject[linesCount];
			groundArray = new GameObject[linesCount];
			staticArray = new GameObject[linesCount];
			levelReader = new LevelReader(Application.dataPath + "/Maps/" + sceneData.CurrentLevel + ".txt", divider);
			foreach (var enemy in enemies)
			{
				sceneData.Enemies.Add(enemy);
				enemy.GetComponent<CopterEnemy>().Initialize();

			}
			patternPositions = new List<Vector3>();
			foreach (var pattern in enemyPatterns)
			{
				sceneData.EnemiesPatterns.Add(pattern);
				patternPositions.Add(pattern.transform.position);
			}
		}

		public void UpdateGenerators(SceneData sceneData)
		{
			railsGen.Generate(sceneData);
			if (currentLine < sceneData.CurrentLine)
			{
				GetLineArrays(sceneData);
				staticGen.Generate(groundArray, sceneData);
				staticGen.Generate(staticArray, sceneData);
				if (enemies.Count > 0)
				{
					enemyGen.Generate(enemiesArray, sceneData);
				}
				currentLine = sceneData.CurrentLine;
			}
		}

		void GetLineArrays(SceneData sceneData)
		{
			string[] line = levelReader.GetLine(sceneData.CurrentLine);
			if (line[0] != null)
			{
				var j = 0;
				for (var i = 0; i < linesCount; i++)
				{
					//print(line[i]);
					objDictionary.ObjectsDictionary.TryGetValue(line[i], out groundArray[j]);
					j++;
				}
				j = 0;
				for (var i = linesCount; i < linesCount * 2; i++)
				{
					//print(line[i]);
					objDictionary.ObjectsDictionary.TryGetValue(line[i], out staticArray[j]);
					j++;
				}
				j = 0;
				for (var i = linesCount * 2; i < linesCount * 3; i++)
				{
					//print(line[i]);
					objDictionary.ObjectsDictionary.TryGetValue(line[i], out enemiesArray[j]);
					j++;
				}
			}
			else
			{
				print("Looks like file hadn't been found. Path " + Application.dataPath + "/Maps/" + sceneData.CurrentLevel + ".txt");
			}

		}

	}
}

