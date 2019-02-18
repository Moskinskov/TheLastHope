using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Generators;
using TheLastHope.Data;

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
        sceneData.trainSpeed = trainSpeed;
        foreach(var enemy in sceneData.enemies)
        {
            enemy.GetComponent<BikeEnemy>().Move(sceneData, Time.deltaTime);
        }
        destroyer.Destroy(sceneData);
        railGen.Generate(sceneData);
        propsGen.Generate(sceneData);
        enemyGen.Generate(debugEnemy,sceneData,patternPositions);
        worldMover.MoveWorld(sceneData, Time.deltaTime);
    }
}
