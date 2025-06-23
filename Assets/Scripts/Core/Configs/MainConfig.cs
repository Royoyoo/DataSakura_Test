using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Configs
{
    [CreateAssetMenu(fileName = "Main_Config", menuName = "Configs/Main Config")]
    public class MainConfig : ScriptableObject
    {
        [field: SerializeField] public float ZooSize { get; private set; }
        [field: SerializeField] public AssetReference ZooSetupReference { get; private set; }
        [field: SerializeField] public float MinSpawnDelay { get; private set; }
        [field: SerializeField] public float MaxSpawnDelay { get; private set; }
        [field: SerializeField] public LayerMask AnimalLayerMask { get; private set; }
        [field: SerializeField] public List<AnimalData> AnimalDatas { get; private set; } = new();
    }
}
