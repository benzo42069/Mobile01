// PulseStrike | BootstrapManager | Phase 1
using PulseStrike.Analytics;
using PulseStrike.Economy;
using PulseStrike.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PulseStrike.Core
{
    public class BootstrapManager : MonoBehaviour
    {
        private void Start()
        {
            SaveSystem.Initialize();
            CreditManager.Instance.InitializeCredits();
            DailyRewardManager.Instance.InitializeDailyState();
            AnalyticsManager.Instance.InitializeAnalytics();

            if (!SaveSystem.HasLaunched())
            {
                SaveSystem.SetHasLaunched(true);
            }

            SceneManager.LoadScene(SceneNames.MainMenu, LoadSceneMode.Single);
        }
    }
}
