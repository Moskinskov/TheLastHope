using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneData
{
    List<GameObject> props;
    List<GameObject> enemies;
    List<GameObject> trainCars;
    List<GameObject> rails;

    public SceneData()
    {
        props = new List<GameObject>();
        enemies = new List<GameObject>();
        trainCars = new List<GameObject>();
        rails = new List<GameObject>();
    }

}