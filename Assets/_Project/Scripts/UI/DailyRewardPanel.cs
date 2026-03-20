// PulseStrike | DailyRewardPanel | Phase 11
using PulseStrike.Economy;
using TMPro;
using UnityEngine;

namespace PulseStrike.UI
{
    public class DailyRewardPanel : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text rewardText;

        public void Show()
        {
            var streak = DailyRewardManager.Instance.StreakDays;
            var day = ((streak - 1) % 7) + 1;
            titleText.text = $"DAY {day} STREAK!";
            rewardText.text = $"{DailyRewardManager.Instance.GetRewardForDay(day)} CREDITS";
            root.SetActive(true);
        }

        public void Claim()
        {
            DailyRewardManager.Instance.ClaimDailyReward();
            Invoke(nameof(Hide), 1f);
        }

        public void Hide()
        {
            root.SetActive(false);
        }
    }
}
