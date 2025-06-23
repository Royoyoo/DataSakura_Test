using Core.AssetsSystem;
using VContainer.Unity;

namespace Container
{
    public class AppInitEntryPoint : IInitializable
    {
        private readonly LifetimeScope appScope;
        private readonly AssetsManager assetsManager;
        private readonly GameplayInstaller gameplayInstaller;

        private LifetimeScope gameplayScope;


        public AppInitEntryPoint(LifetimeScope appScope, AssetsManager assetsManager, GameplayInstaller gameplayInstaller)
        {
            this.appScope = appScope;
            this.assetsManager = assetsManager;
            this.gameplayInstaller = gameplayInstaller;
        }


        public void Initialize()
        {
            CreateChildScope();
        }


        public void DisposeChildScope()
        {
            gameplayScope.Dispose();
            gameplayScope = null;
            assetsManager.UpdateResolver(appScope.Container);

            CreateChildScope();
        }


        private void CreateChildScope()
        {
            gameplayScope = appScope.CreateChild(gameplayInstaller);
            assetsManager.UpdateResolver(gameplayScope.Container);
        }
    }
}
