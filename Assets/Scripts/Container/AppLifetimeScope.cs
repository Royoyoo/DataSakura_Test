using Core.AssetsSystem;
using Core.Configs;
using Core.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Container
{
    public class AppLifetimeScope : LifetimeScope
    {
        [SerializeField] private MainConfig mainConfig;
        [SerializeField] private UiConfig uiConfig;


        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance<MainConfig>(mainConfig);
            builder.RegisterInstance<UiConfig>(uiConfig);

            builder.Register<AssetsManager>(Lifetime.Singleton);
            builder.Register<PoolsManager>(Lifetime.Singleton);
            builder.Register<UIManager>(Lifetime.Singleton);

            builder.Register<GameplayInstaller>(Lifetime.Singleton);

            builder.RegisterEntryPoint<AppInitEntryPoint>().AsSelf();
        }
    }
}
