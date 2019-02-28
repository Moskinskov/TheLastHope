using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Management.Data
{
	public class SceneData
	{
		public List<GameObject> props;
		public List<GameObject> enemies;
		public List<GameObject> trainCars;
		public List<GameObject> rails;
		public List<GameObject> enemiesPatterns;
        public List<GameObject> ammos = new List<GameObject>();
        public int targetEnemyCount;
		public int targetPropsCount;
		public float trainSpeed;

		public SceneData()
		{
			props = new List<GameObject>();
			enemies = new List<GameObject>();
			trainCars = new List<GameObject>();
			rails = new List<GameObject>();
			enemiesPatterns = new List<GameObject>();
		}

	}

}