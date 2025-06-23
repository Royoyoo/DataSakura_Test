using UnityEngine;
using VContainer.Unity;

namespace Gameplay
{
    public class ZooSimulation : IFixedTickable
    {
        private readonly AnimalsHandler animalsHandler;
        private readonly ZooBounds zooBounds;


        public ZooSimulation(AnimalsHandler animalsHandler, ZooBounds zooBounds)
        {
            this.animalsHandler = animalsHandler;
            this.zooBounds = zooBounds;
        }


        public void FixedTick()
        {
            MoveAnimals();
        }


        private void MoveAnimals()
        {
            foreach ((Rigidbody rigidbody, Animal animal) in animalsHandler.Animals)
            {
                if (rigidbody != null)
                {
                    animal.MoveBehaviour.PhysicsUpdate(zooBounds);
                }
            }
        }
    }
}
