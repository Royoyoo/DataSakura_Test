using System.Collections.Generic;
using Core.AssetsSystem;
using Core.Configs;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.UI
{
    public class UIManager
    {
        private const string uiLayerTopCanvasPath = "/UI/Canvas/";

        private readonly AssetsManager assetsManager;
        private readonly UiConfig uiConfig;

        private Transform uiElementsRoot;
        private Dictionary<string, UIElement> elements = new();


        public Transform UIElementsRoot => uiElementsRoot;


        public UIManager(AssetsManager assetsManager, UiConfig uiConfig)
        {
            this.assetsManager = assetsManager;
            this.uiConfig = uiConfig;

            uiElementsRoot = GameObject.Find(uiLayerTopCanvasPath).transform;
        }


        public T GetUiElement<T>(string id, Transform root = null) where T : UIElement
        {
            if (!elements.TryGetValue(id, out UIElement element))
            {
                AssetReference assetReference = uiConfig.UIElementsByKey.Find(asset => asset.Key == id).UIElementReference;
                T uiElement = assetsManager.CreateObjectWithInjection<T>(assetReference, root ? root : UIElementsRoot);
                SortElementsBySortOrder(uiElement);
                elements.Add(id, uiElement);
            }
            else
            {
                assetsManager.Resolver.Inject((T) element);
            }

            return (T) elements[id];
        }


        private void SortElementsBySortOrder(UIElement uiElement)
        {
            for (int i = uiElementsRoot.childCount - 2; i >= 0; i--)
            {
                UIElement childUiElement = uiElementsRoot.GetChild(i).GetComponent<UIElement>();

                if (childUiElement != null && uiElement.SortOrder >= childUiElement.SortOrder)
                {
                    uiElement.transform.SetSiblingIndex(i + 1);

                    return;
                }
            }

            uiElement.transform.SetAsFirstSibling();
        }
    }
}
