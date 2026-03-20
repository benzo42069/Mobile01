// PulseStrike | DailyRewardManager | Phase 6
using System;
using PulseStrike.Analytics;
using PulseStrike.Utils;

namespace PulseStrike.Economy
{
    public class DailyRewardManager : Singleton<DailyRewardManager>
    {
        private int streakDays;
        private bool rewardPendingToday;

        public int StreakDays => streakDays;
        public bool RewardPendingToday => rewardPendingToday;

        public void InitializeDailyState()
        {
            streakDays = SaveSystem.GetStreakDays();
            var today = DateTime.Today;
            var last = SaveSystem.GetLastPlayDate();
            var daysDiff = (today - last).Days;

            if (daysDiff <= 0)
            {
                rewardPendingToday = false;
            }
            else if (daysDiff == 1)
            {
                streakDays = Math.Max(1, streakDays + 1);
                rewardPendingToday = true;
            }
            else
            {
                OnStreakBroken();
                streakDays = 1;
                rewardPendingToday = true;
            }

            SaveSystem.SaveStreakDays(streakDays);
            SaveSystem.SaveRewardPending(rewardPendingToday);
            SaveSystem.SaveLastPlayDate(today);
        }

        public void OnStreakBroken()
        {
            if (CreditManager.Instance.CanAfford(GameplayConstants.StreakShieldCost))
            {
                UI.UIManager.Instance.ShowStreakShieldPrompt(streakDays, GameplayConstants.StreakShieldCost, accepted =>
                {
                    AnalyticsManager.Instance.LogStreakShieldOffered(streakDays, accepted);
                    if (accepted)
                    {
                        CreditManager.Instance.SpendCredits(GameplayConstants.StreakShieldCost, "streak_shield");
                    }
                });
            }
            else
            {
                streakDays = 0;
            }
        }

        public void ClaimDailyReward()
        {
            var day = ((streakDays - 1) % 7) + 1;
            var reward = GetRewardForDay(day);
            CreditManager.Instance.AddCredits(reward, "daily_reward");
            if (day == 7)
            {
                UnlockDaySevenCosmetic();
            }

            rewardPendingToday = false;
            SaveSystem.SaveRewardPending(false);
            AnalyticsManager.Instance.LogDailyReward(day, streakDays);
        }

        public int GetRewardForDay(int day)
        {
            return day switch
            {
                1 => 5,
                2 => 5,
                3 => 8,
                4 => 8,
                5 => 12,
                6 => 12,
                7 => 20,
                _ => 5
            };
        }

        private void UnlockDaySevenCosmetic()
        {
            SaveSystem.UnlockCosmetic("skin_crimson");
            SaveSystem.SetDailyRareUnlock(true);
        }
    }
}
