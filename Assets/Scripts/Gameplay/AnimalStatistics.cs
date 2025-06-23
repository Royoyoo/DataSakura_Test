using System;
using System.Collections.Generic;
using Gameplay.FoodChain;

namespace Gameplay
{
    public class AnimalStatistics : IDisposable
    {
        public event Action OnStatisticsUpdated;


        private readonly AnimalsHandler animalsHandler;

        public Dictionary<FoodChainType, int> DeadStats { get; private set; } = new();


        public AnimalStatistics(AnimalsHandler animalsHandler)
        {
            this.animalsHandler = animalsHandler;

            DeadStats.Add(FoodChainType.Predator, 0);
            DeadStats.Add(FoodChainType.Prey, 0);

            animalsHandler.OnAnimalDied += LogDeadStats;
        }


        public void Dispose()
        {
            animalsHandler.OnAnimalDied -= LogDeadStats;
        }


        private void LogDeadStats(Animal animal)
        {
            DeadStats.TryAdd(animal.FoodChainType, 0);

            DeadStats[animal.FoodChainType]++;

            OnStatisticsUpdated?.Invoke();
        }
    }
}
