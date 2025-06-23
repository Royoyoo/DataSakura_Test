using System;
using System.Collections.Generic;
using Core.AssetsSystem;
using Core.Configs;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class AnimalsHandler : IStartable, IDisposable
    {
        public event Action<Animal> OnAnimalDied;

        private readonly IObjectResolver resolver;
        private readonly MainConfig mainConfig;
        private readonly ZooBounds zooBounds;
        private readonly AnimalSpawner animalSpawner;
        private readonly AnimalFactory animalFactory;
        private readonly AssetsManager assetsManager;

        private readonly Dictionary<Rigidbody, Animal> animals = new();

        public IReadOnlyDictionary<Rigidbody, Animal> Animals => animals;


        public AnimalsHandler(
            IObjectResolver resolver,
            MainConfig mainConfig,
            ZooBounds zooBounds,
            AnimalSpawner animalSpawner,
            AnimalFactory animalFactory,
            AssetsManager assetsManager)
        {
            this.resolver = resolver;
            this.mainConfig = mainConfig;
            this.zooBounds = zooBounds;
            this.animalSpawner = animalSpawner;
            this.animalFactory = animalFactory;
            this.assetsManager = assetsManager;
        }


        public void Start()
        {
            foreach (AnimalData animalData in mainConfig.AnimalDatas)
            {
                resolver.Inject(animalData.FoodChainBehaviour);
            }

            animalSpawner.OnSpawnAnimalRequested += SpawnAnimal;
        }


        public void Dispose()
        {
            animals.Clear();

            animalSpawner.OnSpawnAnimalRequested -= SpawnAnimal;
        }


        public Animal GetAnimal(Rigidbody rigidbody)
        {
            return animals.GetValueOrDefault(rigidbody);
        }


        public void DestroyAnimal(Animal animal)
        {
            animals.Remove(animal.Rigidbody);
            OnAnimalDied?.Invoke(animal);

            assetsManager.DestroyObject(animal.Rigidbody.gameObject);
        }


        private void SpawnAnimal()
        {
            Vector3 animalPosition = zooBounds.GetRandomPointInBounds();
            AnimalData animalData = mainConfig.AnimalDatas[Random.Range(0, mainConfig.AnimalDatas.Count)];

            Animal newAnimal = animalFactory.CreateAnimal(animalData, animalPosition);
            animals.Add(newAnimal.Rigidbody, newAnimal);
        }
    }
}
