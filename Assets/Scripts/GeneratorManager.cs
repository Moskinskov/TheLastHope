using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Generators;
using TheLastHope.Data;
using Assets.Scripts.Helpers;


public class GeneratorManager : Singleton<GeneratorManager>
{
    [SerializeField] AGenerator enemyGen;
    [SerializeField] AGenerator staticGen;
    [SerializeField] AGenerator railsGen;
    [SerializeField] List<GameObject> enemies;
    [SerializeField] List<GameObject> enemyPatterns;
    List<Vector3> patternPositions;

    public void Initialize(SceneData sceneData)
    {
        foreach(var enemy in enemies)
        {
            sceneData.enemies.Add(enemy);
            enemy.GetComponent<BikeEnemy>().Initialize();

        }
        patternPositions = new List<Vector3>();
        foreach (var pattern in enemyPatterns)
        {
            sceneData.enemiesPatterns.Add(pattern);
            patternPositions.Add(pattern.transform.position);

        }
    }

    public void UpdateGenerators(SceneData sceneData)
    {
        railsGen.Generate(sceneData);
        staticGen.Generate(sceneData);
        if(enemies.Count > 0)
        {
            enemyGen.Generate(enemies[0], sceneData, patternPositions);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
