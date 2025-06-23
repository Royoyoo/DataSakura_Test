using Gameplay.FoodChain;
using Gameplay.FoodChain.Behaviours;
using Gameplay.Movement;
using UnityEngine;

namespace Gameplay
{
    public class Animal
    {
        public readonly Rigidbody Rigidbody;

        public FoodChainType FoodChainType;
        public IMoveBehaviour MoveBehaviour;
        public IFoodChainBehaviour FoodChainBehaviour;


        public Animal(Rigidbody rigidbody)
        {
            this.Rigidbody = rigidbody;
        }
    }
}
