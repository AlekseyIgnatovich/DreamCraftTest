using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolService
{
	private Dictionary<Type, object> _pools = new ();
	
	public void AddPool<T>(string[] path, int capacity, int maxPoolSize) where T: Component
	{
		var pool = new PoolWrapper<T>(path, capacity, maxPoolSize);
		
		_pools.Add(typeof(T), pool);
	}
	
	public IObjectPool<T> GetPool<T>() where T: Component
	{
		if(_pools.TryGetValue(typeof(T), out var wrapper))
		{
			return ((PoolWrapper<T>)wrapper).Pool;
		}

		return null;
	}
}
