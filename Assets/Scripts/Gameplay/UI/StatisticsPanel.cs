using System.Linq;
using Core.UI;
using TMPro;
using UnityEngine;
using VContainer;

namespace Gameplay.UI
{
    public class StatisticsPanel : UIElement
    {
        [SerializeField] private TMP_Text statisticsText;


        [Inject] private readonly AnimalStatistics animalStatistics;


        public override void Show()
        {
            base.Show();

            animalStatistics.OnStatisticsUpdated += UpdateStatisticsText;

            UpdateStatisticsText();
        }


        public override void Hide()
        {
            base.Hide();

            animalStatistics.OnStatisticsUpdated -= UpdateStatisticsText;
        }


        private void OnDestroy()
        {
            if (animalStatistics != null)
            {
                animalStatistics.OnStatisticsUpdated -= UpdateStatisticsText;
            }
        }


        private void UpdateStatisticsText()
        {
            if (statisticsText != null)
            {
                statisticsText.text = string.Join(separator: "\n", values: animalStatistics.DeadStats.Select(pair => $"{pair.Key}: {pair.Value}"));
            }
        }
    }
}
