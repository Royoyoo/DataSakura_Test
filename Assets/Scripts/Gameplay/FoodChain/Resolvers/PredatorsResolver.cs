using Gameplay.FoodChain.Behaviours;
using UnityEngine;

namespace Gameplay.FoodChain.Resolvers
{
    public class PredatorsResolver : IFoodChainResolver
    {
        public void ResolveCollision(Animal animal, Animal otherAnimal, AnimalsHandler animalsHandler)
        {
            Animal hunter = Random.value > 0.5 ? animal : otherAnimal;
            Animal prey = hunter == animal ? otherAnimal : animal;

            PredatorBehaviour predatorBehaviour = (PredatorBehaviour) hunter.FoodChainBehaviour;
            predatorBehaviour.Eat(hunter);
            animalsHandler.DestroyAnimal(prey);
        }
    }
}
