using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Workers
{
	public class ObjectPool
	{
		private List<IPoolable> pooledObjects = new List<IPoolable>();

		public ObjectPool(int size = 0)
		{
			pooledObjects = new List<IPoolable>(size);
		}

	}
}
