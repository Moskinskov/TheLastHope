﻿using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Data;
using TheLastHope.Weapons;

class Destroyer : ADestroyer
{
    [SerializeField] private float _xDestroyCoord;
    public override void Destroy(SceneData sceneData)
    {
        DestroyInList(sceneData.props);
        DestroyInList(sceneData.rails);
        DestroyDeadEnemiesInList(sceneData.enemies);
        //DestroyFinishedAmmoInList(sceneData.ammos);
    }

    void DestroyInList(List<GameObject> list)
    {
        for(var i=0;i<list.Count;i++)
        {
            if (list[i].transform.position.x < _xDestroyCoord)
            {
                Destroy(list[i].gameObject);
                list.RemoveAt(i);
                i--;
            }
        }
    }

    void DestroyDeadEnemiesInList(List<GameObject> list)
    {
        for (var i = 0; i < list.Count; i++)
        {
            if (list[i].GetComponent<AEnemy>().health<=0)
            {
				list[i].GetComponent<AEnemy>().Die();
				Destroy(list[i].gameObject, 2.0f);
                list.RemoveAt(i);
                i--;
            }
        }
    }

    void DestroyFinishedAmmoInList(List<GameObject> list)
    {
        for (var i = 0; i < list.Count; i++)
        {
            if ((list[i].GetComponent<AAmmo>().transform.position - list[i].GetComponent<AAmmo>().startPoint).magnitude 
                > list[i].GetComponent<AAmmo>().maxMagnitude)
            {
                Destroy(list[i].gameObject);
                list.RemoveAt(i);
                i--;
            }
        }
    }
}

