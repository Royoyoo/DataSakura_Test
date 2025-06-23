namespace Gameplay.FoodChain.Resolvers
{
    public interface IFoodChainResolver
    {
        void ResolveCollision(Animal animal, Animal otherAnimal, AnimalsHandler animalsHandler);
    }
}
