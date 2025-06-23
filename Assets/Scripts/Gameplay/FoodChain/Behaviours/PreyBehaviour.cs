namespace Gameplay.FoodChain.Behaviours
{
    public class PreyBehaviour : IFoodChainBehaviour
    {
        public IFoodChainBehaviour CreateFoodChainBehaviour(Animal animal)
        {
            animal.FoodChainType = FoodChainType.Prey;

            return null;
        }
    }
}
