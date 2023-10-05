using System.Collections.Generic;
using LOONACIA.Unity.Pool;
using UnityEngine;
using UnityEngine.Pool;

namespace LOONACIA.Unity.Managers
{
	public class PoolManager
	{
		private readonly Dictionary<string, GameObject> _originals = new();

		private readonly Dictionary<string, IObjectPool<Poolable>> _registeredPools = new();

		private Transform Root
		{
			get
			{
				if (GameObject.Find("@Pool_Root") is not { } root)
				{
					root = new() { name = "@Pool_Root" };
				}

				return root.transform;
			}
		}

		public Poolable Get(string name)
		{
			if (!_originals.TryGetValue(name, out GameObject original))
			{
				original = Resources.Load<GameObject>($"Prefabs/{name}");
				_originals.Add(name, original);
			}

			return Get(original);
		}

		public Poolable Get(GameObject prefab)
		{
			Init(prefab);
			return _registeredPools[prefab.name].Get();
		}

		public void Release(Poolable pooledObject)
		{
			pooledObject.transform.parent = Root;
			pooledObject.Pool.Release(pooledObject);
		}

		private Poolable Create(GameObject original)
		{
			var obj = Object.Instantiate(original);
			obj.name = original.name;
			var poolable = obj.GetOrAddComponent<Poolable>();
			poolable.Pool = _registeredPools[original.name];
			return poolable;
		}

		private void OnGet(Poolable pooledObject)
		{
			pooledObject.gameObject.SetActive(true);
		}

		private void OnRelease(Poolable pooledObject)
		{
			pooledObject.gameObject.SetActive(false);
		}

		private void OnDestroy(Poolable pooledObject)
		{
			Object.Destroy(pooledObject.gameObject);
		}

		private void Init(GameObject original)
		{
			if (_registeredPools.ContainsKey(original.name))
			{
				return;
			}

			IObjectPool<Poolable> pool = new ObjectPool<Poolable>
			(
				createFunc: () => Create(original),
				actionOnGet: OnGet,
				actionOnRelease: OnRelease,
				actionOnDestroy: OnDestroy
			);

			_registeredPools.Add(original.name, pool);
		}
	}
}