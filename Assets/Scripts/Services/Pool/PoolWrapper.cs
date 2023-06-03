using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class PoolWrapper<T> where T : Component
{
	private const bool collectionChecks = true;

	public IObjectPool<T> Pool;

	private string[] _prefabPath;
	private int count = 0;

	public PoolWrapper(string[] prefabPath, int defaultCapacity, int maxPoolSize)
	{
		_prefabPath = prefabPath;
		Pool = new ObjectPool<T>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject,
			collectionChecks, defaultCapacity, maxPoolSize);
	}

	T CreatePooledItem()
	{
		count++;
		count %= _prefabPath.Length;

		var prefab = Resources.Load(_prefabPath[count]);
		var gameObject = GameObject.Instantiate(prefab);
		return gameObject.GetComponent<T>();
	}

	// Called when an item is returned to the pool using Release
	void OnReturnedToPool(Component view)
	{
		view.gameObject.SetActive(false);
	}

	// Called when an item is taken from the pool using Get
	void OnTakeFromPool(Component view)
	{
		view.gameObject.SetActive(true);
	}

	// If the pool capacity is reached then any items returned will be destroyed.
	// We can control what the destroy behavior does, here we destroy the GameObject.
	void OnDestroyPoolObject(Component system)
	{
		GameObject.Destroy(system.gameObject);
	}
}
