using System;
using Core.AssetsSystem;
using Core.Configs;
using Core.UI;
using Gameplay;
using Gameplay.UI;
using VContainer.Unity;

namespace Container
{
    public class GameplayEntryPoint : IStartable, IDisposable
    {
        private readonly MainConfig mainConfig;
        private readonly AssetsManager assetsManager;
        private readonly UIManager uiManager;

        private UIElement statisticsPanel;


        public GameplayEntryPoint(MainConfig mainConfig, AssetsManager assetsManager, UIManager uiManager)
        {
            this.mainConfig = mainConfig;
            this.assetsManager = assetsManager;
            this.uiManager = uiManager;
        }


        public void Start()
        {
            InitGameplay();
            StartGameplay();
        }


        private void InitGameplay()
        {
            ZooSetup zooSetup = assetsManager.CreateObject<ZooSetup>(mainConfig.ZooSetupReference);
            zooSetup.Initialize(mainConfig.ZooSize);
        }


        private void StartGameplay()
        {
            statisticsPanel = uiManager.GetUiElement<StatisticsPanel>(GameplayUIElements.StatisticsPanel);
            statisticsPanel.Show();
        }


        public void Dispose()
        {
            if (statisticsPanel != null)
            {
                statisticsPanel.Hide();
            }
        }
    }
}
