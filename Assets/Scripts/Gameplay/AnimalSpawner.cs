using System;
using Core.Configs;
using UnityEngine;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class AnimalSpawner : ITickable
    {
        public event Action OnSpawnAnimalRequested;

        private readonly MainConfig mainConfig;

        private float nextSpawnDelay = 0f;


        public AnimalSpawner(MainConfig mainConfig)
        {
            this.mainConfig = mainConfig;

            nextSpawnDelay = GetNextSpawnDelay();
        }


        public void Tick()
        {
            nextSpawnDelay -= Time.deltaTime;

            if (nextSpawnDelay <= 0f)
            {
                OnSpawnAnimalRequested?.Invoke();
                nextSpawnDelay += GetNextSpawnDelay();
            }
        }


        private float GetNextSpawnDelay()
        {
            return Random.Range(mainConfig.MinSpawnDelay, mainConfig.MaxSpawnDelay);
        }
    }
}
