using Gameplay.FoodChain.Behaviours;
using Gameplay.Movement.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "AnimalData", menuName = "Configs/Animal Data")]
    public class AnimalData : ScriptableObject
    {
        public AssetReference AnimalModelReference;

        [SerializeReference] public IFoodChainBehaviour FoodChainBehaviour;
        [SerializeReference] public IMoveBehaviourData MoveBehaviourData;
    }
}
