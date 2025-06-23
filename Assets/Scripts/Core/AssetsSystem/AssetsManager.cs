using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Core.AssetsSystem
{
    public class AssetsManager
    {
        private readonly PoolsManager poolsManager;

        public IObjectResolver Resolver { get; private set; }


        public AssetsManager(IObjectResolver resolver, PoolsManager poolsManager)
        {
            this.Resolver = resolver;
            this.poolsManager = poolsManager;
        }


        public void UpdateResolver(IObjectResolver scopeContainer)
        {
            Resolver = scopeContainer;
        }


        public T CreateObject<T>(AssetReference assetReference, Transform parent = null, Vector3? position = null, bool isPooled = false)
            where T : UnityEngine.Component
        {
            T assetPrefab = GetAssetFromReference<T>(assetReference);

            T instance = isPooled ?
                poolsManager.GetFromPool<T>(assetPrefab.gameObject, position, parent) :
                Object.Instantiate(assetPrefab, position ?? Vector3.zero, Quaternion.identity, parent);

            return instance;
        }


        public T CreateObjectWithInjection<T>(AssetReference assetReference, Transform parent = null)
            where T : UnityEngine.Component
        {
            T prefab = GetAssetFromReference<T>(assetReference);
            T instance = Resolver.Instantiate(prefab);
            instance.transform.SetParent(parent, false);

            return instance;
        }


        public void DestroyObject(GameObject gameObject)
        {
            if (poolsManager.IsObjectPooled(gameObject))
            {
                poolsManager.Release(gameObject);
            }
            else
            {
                Object.Destroy(gameObject);
            }
        }


        private T GetAssetFromReference<T>(AssetReference assetReference, string id = "") where T : UnityEngine.Object
        {
            if (typeof(T).IsSubclassOf(typeof(Component)))
            {
                GameObject assetGameObject = LoadAsset<GameObject>(assetReference);

                T component = assetGameObject.GetComponent<T>();

                if (component == null)
                {
                    throw new Exception($"Asset exists, but component of type '{nameof(T)}' is missing.");
                }

                return component;
            }
            else
            {
                return LoadAsset<T>(assetReference);
            }
        }


        private T LoadAsset<T>(AssetReference assetReference) where T : UnityEngine.Object
        {
            return assetReference.Asset == null ?
                assetReference.LoadAssetAsync<T>().WaitForCompletion() :
                (T) assetReference.Asset;
        }
    }
}
