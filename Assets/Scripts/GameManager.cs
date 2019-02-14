using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generators;

public class GameManager : MonoBehaviour
{

    SceneData sceneData;
    [SerializeField] AGenerator railGen;
    [SerializeField] AGenerator propsGen;
    [SerializeField] AGenerator enemyGen;
    [SerializeField] float targetEnemyCount;
    [SerializeField] float targetPropsCount;
    [SerializeField] float trainSpeed;
    [SerializeField] AWorldMover worldMover;
    [SerializeField] ADestroyer destroyer;
    [SerializeField] GameObject debugEnemy;
    [SerializeField] GameObject debugEnemyPattern;


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
    }

    // Update is called once per frame
    void Update()
    {
        sceneData.enemies[0].GetComponent<BikeEnemy>().Move(sceneData,Time.deltaTime);
        destroyer.Destroy(sceneData);
        railGen.Generate(sceneData);
        propsGen.Generate(sceneData);
        enemyGen.Generate(sceneData);
        worldMover.MoveWorld(sceneData, Time.deltaTime);
    }
}
