using Gameplay;
using Gameplay.FoodChain;
using Gameplay.UI;
using VContainer;
using VContainer.Unity;

namespace Container
{
    public class GameplayInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<AnimalsHandler>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<AnimalFactory>(Lifetime.Singleton);
            builder.Register<AnimalCollisionResolver>(Lifetime.Singleton);
            builder.Register<ZooBounds>(Lifetime.Singleton);
            builder.Register<ZooSimulation>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            builder.Register<AnimalStatistics>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
            builder.Register<AnimalSpawner>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
            builder.Register<WorldTextSpawner>(Lifetime.Scoped);

            builder.RegisterEntryPoint<GameplayEntryPoint>();

            builder.RegisterBuildCallback(container => { container.Resolve<AnimalStatistics>(); });
        }
    }
}
