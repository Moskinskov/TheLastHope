using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.AbstractLayer;
using TheLastHope.Management.Data;

public class TriggerEnemyGen : ATrigger
{
    [SerializeField] List<Transform> patternPositions;
    [SerializeField] List<string> enemyNames;
    // Start is called before the first frame update
    void Start()
    {
        base.Type = TriggerType.GenerateEnemy;
        base.Pattern = patternPositions;
        base.EnemiesNames = enemyNames;
    }

}
