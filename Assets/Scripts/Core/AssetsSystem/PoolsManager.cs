using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Core.AssetsSystem
{
    public class PoolsManager
    {
        private Dictionary<GameObject, ObjectPool<GameObject>> pools = new();
        private Dictionary<GameObject, GameObject> instanceByPrefab = new();


        public T GetFromPool<T>(GameObject poolPrefab, Vector3? position = null, Transform parent = null) where T : Component
        {
            if (!pools.TryGetValue(poolPrefab, out ObjectPool<GameObject> pool))
            {
                pool = CreatePool(poolPrefab);
            }

            GameObject obj = pool.Get();
            // obj.transform.SetParent(parent);
            obj.transform.SetPositionAndRotation(position ?? Vector3.zero, Quaternion.identity);

            instanceByPrefab.Add(obj, poolPrefab);

            return obj.GetComponent<T>();
        }


        public bool IsObjectPooled(GameObject gameObject)
        {
            return instanceByPrefab.TryGetValue(gameObject, out GameObject prefab) && pools.ContainsKey(prefab);
        }


        public void Release(GameObject gameObject)
        {
            if (!instanceByPrefab.TryGetValue(gameObject, out GameObject prefab))
            {
                throw new Exception("Released object not from pool!");
            }

            pools[prefab].Release(gameObject);
            instanceByPrefab.Remove(gameObject);
        }


        private ObjectPool<GameObject> CreatePool(GameObject poolPrefab)
        {
            ObjectPool<GameObject> pool = new(
                createFunc: () => Object.Instantiate(poolPrefab),
                actionOnGet: gameObject => gameObject.SetActive(true),
                actionOnRelease: gameObject => gameObject.SetActive(false),
                actionOnDestroy: Object.Destroy
            );

            pools.Add(poolPrefab, pool);

            return pool;
        }
    }
}
