// PulseStrike | MainMenu | Phase 11
using PulseStrike.Economy;
using PulseStrike.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PulseStrike.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Text personalBestText;
        [SerializeField] private TMP_Text creditsText;
        [SerializeField] private TMP_Text streakText;
        [SerializeField] private TMP_Text versionText;

        private void Start()
        {
            personalBestText.text = $"BEST: {SaveSystem.GetPersonalBest()}";
            creditsText.text = CreditManager.Instance.CurrentCredits.ToString();
            streakText.text = $"STREAK: {DailyRewardManager.Instance.StreakDays}";
            versionText.text = Application.version;

            if (DailyRewardManager.Instance.RewardPendingToday)
            {
                UIManager.Instance.OpenDailyReward();
            }
        }

        public void OnPlayPressed()
        {
            SceneManager.LoadScene(SceneNames.Game);
        }

        public void OnStorePressed()
        {
            UIManager.Instance.OpenStore();
        }
    }
}
