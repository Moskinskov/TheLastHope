using System;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Data;


class Destroyer : ADestroyer
{
    [SerializeField] float xDestroyCoord;
    public override void Destroy(SceneData sceneData)
    {
        DestroyInList(sceneData.props);
        DestroyInList(sceneData.rails);
        DestroyInList(sceneData.enemies);

    }

    void DestroyInList(List<GameObject> list)
    {
        for(var i=0;i<list.Count;i++)
        {
            if (list[i].transform.position.x < xDestroyCoord)
            {
                Destroy(list[i].gameObject);
                list.RemoveAt(i);
                i--;
            }
        }
    }
}

