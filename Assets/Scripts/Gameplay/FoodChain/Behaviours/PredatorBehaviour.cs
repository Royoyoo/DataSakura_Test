using Core.Configs;
using Gameplay.UI;
using VContainer;

namespace Gameplay.FoodChain.Behaviours
{
    public class PredatorBehaviour : IFoodChainBehaviour
    {
        [Inject] private MainConfig mainConfig;
        [Inject] private WorldTextSpawner worldTextSpawner;
        [Inject] private AnimalCollisionResolver animalCollisionResolver;


        public IFoodChainBehaviour CreateFoodChainBehaviour(Animal animal)
        {
            animal.FoodChainType = FoodChainType.Predator;

            AnimalCollisionReporter collisionReporter = animal.Rigidbody.gameObject.AddComponent<AnimalCollisionReporter>();
            collisionReporter.Initialize(animal.Rigidbody, mainConfig.AnimalLayerMask, animalCollisionResolver);

            return this;
        }


        public void Eat(Animal self)
        {
            worldTextSpawner.SpawnWorldText("Tasty!", self.Rigidbody.transform);
        }
    }
}
