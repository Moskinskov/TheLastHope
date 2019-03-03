﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Management.Data
{
	public class SceneData
	{
		List<GameObject> props = new List<GameObject>();
        List<GameObject> enemies = new List<GameObject>();
        List<GameObject> trainCars = new List<GameObject>();
        List<GameObject> rails = new List<GameObject>();
        List<GameObject> enemiesPatterns = new List<GameObject>();
        List<GameObject> ammos = new List<GameObject>();

        /// <summary>
        /// List of active props on scene.
        /// </summary>
        public List<GameObject> Props { get { return props; } set { props = value; }}
        /// <summary>
        /// List of active enemies on scene.
        /// </summary>
        public List<GameObject> Enemies { get { return enemies; } set { enemies = value; } }
        /// <summary>
        /// List of tran cars on scene.
        /// </summary>
        public List<GameObject> TrainCars { get { return trainCars; } set { trainCars = value; } }
        /// <summary>
        /// List of  active rails on scene.
        /// </summary>
        public List<GameObject> Rails { get { return rails; } set { rails = value; } }
        /// <summary>
        /// List of enemies' spawn points.
        /// </summary>
        public List<GameObject> EnemiesPatterns { get { return enemiesPatterns; } set { enemiesPatterns = value; } }
        /// <summary>
        /// List of shot ammos.
        /// </summary>
        public List<GameObject> Ammos { get { return ammos; } set { ammos = value; } }
        /// <summary>
        /// Count of expected enemies on scene right now.
        /// </summary>
        public int TargetEnemyCount { get; set; }
        /// <summary>
        /// Count of expected props on scene right now.
        /// </summary>
        public int TargetPropsCount { get; set; }
        /// <summary>
        /// Current train speed.
        /// </summary>
        public float TrainSpeed { get; set; }
        /// <summary>
        /// Length of vertical (from camera) line. Used for counting driven path.
        /// </summary>
        public float LineLength { get; set; }
        /// <summary>
        /// Store number of driven lines;
        /// </summary>
        public int CurrentLine { get; set; }
        /// <summary>
        /// Name of current level.
        /// </summary>
        public string CurrentLevel { get; set; }

		public SceneData()
		{
		}

	}

}