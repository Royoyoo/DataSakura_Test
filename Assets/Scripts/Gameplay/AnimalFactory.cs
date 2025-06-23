using Core.AssetsSystem;
using UnityEngine;

namespace Gameplay
{
    public class AnimalFactory
    {
        private readonly AssetsManager assetsManager;


        public AnimalFactory(AssetsManager assetsManager)
        {
            this.assetsManager = assetsManager;
        }


        public Animal CreateAnimal(AnimalData animalData, Vector3 animalPosition)
        {
            Rigidbody animalRigidbody =
                assetsManager.CreateObject<Rigidbody>(animalData.AnimalModelReference, position: animalPosition, isPooled: true);

            Animal animal = new(animalRigidbody);

            animal.MoveBehaviour = animalData.MoveBehaviourData.CreateMoveBehaviour(animal.Rigidbody);
            animal.FoodChainBehaviour = animalData.FoodChainBehaviour.CreateFoodChainBehaviour(animal);

            return animal;
        }
    }
}
