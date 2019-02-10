﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneData
{
    public List<GameObject> props;
    public List<GameObject> enemies;
    public List<GameObject> trainCars;
    public List<GameObject> rails;
    public float targetEnemyCount;
    public float targetPropsCount;
    public float trainSpeed;

    public SceneData()
    {
        props = new List<GameObject>();
        enemies = new List<GameObject>();
        trainCars = new List<GameObject>();
        rails = new List<GameObject>();
    }

}