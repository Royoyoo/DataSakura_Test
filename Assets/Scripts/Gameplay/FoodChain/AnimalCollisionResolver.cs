using System.Collections.Generic;
using Gameplay.FoodChain.Resolvers;
using UnityEngine;

namespace Gameplay.FoodChain
{
    public class AnimalCollisionResolver
    {
        private readonly AnimalsHandler animalsHandler;

        private readonly Dictionary<(FoodChainType, FoodChainType), IFoodChainResolver> foodChainResolvers = new();


        public AnimalCollisionResolver(AnimalsHandler animalsHandler)
        {
            this.animalsHandler = animalsHandler;

            foodChainResolvers.Add((FoodChainType.Predator, FoodChainType.Prey), new PredatorPreyResolver());
            foodChainResolvers.Add((FoodChainType.Predator, FoodChainType.Predator), new PredatorsResolver());
        }


        public void HandleAnimalCollision(Rigidbody animalRigidbody, Rigidbody otherAnimalRigidbody)
        {
            Animal animal = animalsHandler.GetAnimal(animalRigidbody);
            Animal otherAnimal = animalsHandler.GetAnimal(otherAnimalRigidbody);

            if (animal == null || otherAnimal == null)
            {
                // Collision already resolved by another call
                return;
            }

            if (!foodChainResolvers.TryGetValue((animal.FoodChainType, otherAnimal.FoodChainType), out IFoodChainResolver foodChainResolver) &&
                !foodChainResolvers.TryGetValue((otherAnimal.FoodChainType, animal.FoodChainType), out foodChainResolver))
            {
                Debug.LogWarning(
                    $"Couldn't find FoodChainResolver for pair: {animal.FoodChainType}, {otherAnimal.FoodChainType}");

                return;
            }

            foodChainResolver.ResolveCollision(animal, otherAnimal, animalsHandler);
        }
    }
}
