using Gameplay.FoodChain.Behaviours;

namespace Gameplay.FoodChain.Resolvers
{
    public class PredatorPreyResolver : IFoodChainResolver
    {
        public void ResolveCollision(Animal animal, Animal otherAnimal, AnimalsHandler animalsHandler)
        {
            Animal predator = animal.FoodChainType == FoodChainType.Predator ? animal : otherAnimal;
            Animal prey = animal.FoodChainType == FoodChainType.Prey ? animal : otherAnimal;

            PredatorBehaviour predatorBehaviour = (PredatorBehaviour) predator.FoodChainBehaviour;
            predatorBehaviour.Eat(predator);
            animalsHandler.DestroyAnimal(prey);
        }
    }
}
