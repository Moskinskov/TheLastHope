﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;
using TheLastHope.Enemies;
using TheLastHope.Helpers;

namespace TheLastHope.Management
{
    /// <summary>
    /// Class that manages generators.
    /// </summary>
    public class GeneratorManager : Singleton<GeneratorManager>
    {
        [SerializeField] AGenerator enemyGen;
        [SerializeField] AGenerator staticGen;
        [SerializeField] AGenerator railsGen;
        [SerializeField] ObjectDictionary objDictionary;
        [SerializeField] List<GameObject> enemies;
        [SerializeField] List<GameObject> enemyPatterns;
        [SerializeField] int lineWidth;
        //[SerializeField] int layersCount;
        List<Vector3> patternPositions;
        GameObject[] enemiesArray;
        GameObject[] groundArray;
        GameObject[] staticArray;
        GameObject[] triggerArray;
        int currentLine = -1;
        LevelReader levelReader;
        [SerializeField] char devider = ',';

        /// <summary>
        /// Initializes generator manager.
        /// </summary>
        /// <param name="sceneData"></param>
        public void Initialize(SceneData sceneData)
        {
            enemiesArray = new GameObject[lineWidth];
            groundArray = new GameObject[lineWidth];
            staticArray = new GameObject[lineWidth];
            triggerArray = new GameObject[lineWidth];
            levelReader = new LevelReader(Application.dataPath + "/Maps/" + sceneData.CurrentLevel +".txt", devider);
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

        /// <summary>
        /// Call generators and generate objects according to map-file.
        /// </summary>
        /// <param name="sceneData"></param>
        public void UpdateGenerators(SceneData sceneData)
        {
            //TODO rewrite this method. Map-file must have influence on it.
            railsGen.Generate(sceneData);
            if (currentLine < sceneData.CurrentLine)
            {
                GetLineArrays(sceneData);
                staticGen.Generate(groundArray,sceneData);
                staticGen.Generate(staticArray,sceneData);
                staticGen.Generate(triggerArray, sceneData);
                if (enemies.Count > 0)
                {
                    enemyGen.Generate(enemiesArray, sceneData);
                }
                currentLine = sceneData.CurrentLine;
            }
        }

        public void GenerateDynamicFromTrigger(List<string> enemiesNames, SceneData sceneData, List<Transform> patterns )
        {
            List<GameObject> enemies = new List<GameObject>();
            for (var i = 0; i < lineWidth; i++)
            {
                GameObject enemy = new GameObject();
                objDictionary.ObjectsDictionary.TryGetValue(enemiesNames[i], out enemy);
                enemies.Add(enemy);
            }
            List<Vector3> positions = new List<Vector3>();
            foreach(var pattern in patterns)
            {
                positions.Add(pattern.position);
            }
            enemyGen.Generate(enemies,sceneData,positions);
        }

        void GetLineArrays(SceneData sceneData)
        {
            string[] line = levelReader.GetLine(sceneData.CurrentLine);
            if (line[0] != null)
            {
                var j = 0;
                for (var i = 0; i < lineWidth; i++)
                {
                    //print(line[i]);
                    objDictionary.ObjectsDictionary.TryGetValue(line[i], out groundArray[j]);
                    j++;
                }
                j = 0;
                for (var i = lineWidth; i < lineWidth * 2; i++)
                {
                    //print(line[i]);
                    objDictionary.ObjectsDictionary.TryGetValue(line[i], out staticArray[j]);
                    j++;
                }
                j = 0;
                for (var i = lineWidth * 2; i < lineWidth * 3; i++)
                {
                    //print(line[i]);
                    objDictionary.ObjectsDictionary.TryGetValue(line[i], out enemiesArray[j]);
                    j++;
                }
                j = 0;
                for (var i = lineWidth * 3; i < lineWidth * 4; i++)
                {
                    //print(line[i]);
                    objDictionary.ObjectsDictionary.TryGetValue(line[i], out triggerArray[j]);
                    j++;
                }
            }
            else
            {
                print("Looks like map-file have not founded. Path " + Application.dataPath + "/Maps/" + sceneData.CurrentLevel + ".txt");
            }

        }

    }
}

