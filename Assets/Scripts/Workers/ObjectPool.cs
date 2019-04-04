/// Limerence Games
/// The Last Hope
/// Curator: Dmitri Kuzmin
/// to be commented

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Interfaces;

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
