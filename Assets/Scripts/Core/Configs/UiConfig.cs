using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Configs
{
    [CreateAssetMenu(fileName = "UI_Config", menuName = "Configs/UI Config")]
    public class UiConfig : ScriptableObject
    {
        [field: SerializeField] public AssetReference WorldTextReference { get; private set; }
        [field: SerializeField] public List<UIElementsByKey> UIElementsByKey { get; private set; } = new();
    }
}
