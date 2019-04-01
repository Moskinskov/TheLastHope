using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Management.Data;
using TheLastHope.Weapons;
using TheLastHope.Management.AbstractLayer;

class Destroyer : ADestroyer
{
    [SerializeField] private float _xDestroyCoord;
    /// <summary>
    /// Destroy objects according to conditions.
    /// </summary>
    /// <param name="sceneData"></param>
    public override void Destroy(SceneData sceneData)
    {
        DestroyInList(sceneData.Props);
        DestroyInList(sceneData.Rails);
        DestroyDeadEnemiesInList(sceneData.Enemies);
        //DestroyFinishedAmmoInList(sceneData.Ammos);
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
            if (list[i].GetComponent<AEnemy>().IsActive == false)
            {
                list.RemoveAt(i);
                i--;
            }
        }
    }

    void DestroyFinishedAmmoInList(List<GameObject> list)
    {
        for (var i = 0; i < list.Count; i++)
        {
            if ((list[i].GetComponent<AAmmo>().transform.position - list[i].GetComponent<AAmmo>().StartPoint).magnitude 
                > list[i].GetComponent<AAmmo>().MaxMagnitude)
            {
                Destroy(list[i].gameObject);
                list.RemoveAt(i);
                i--;
            }
        }
    }
}

