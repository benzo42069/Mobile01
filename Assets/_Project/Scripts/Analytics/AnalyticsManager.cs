// PulseStrike | AnalyticsManager | Phase 10
using System.Collections.Generic;
using PulseStrike.Utils;

namespace PulseStrike.Analytics
{
    public class AnalyticsManager : Singleton<AnalyticsManager>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        public void InitializeAnalytics()
        {
            FirebaseWrapper.Initialize();
        }

        public void LogGameStart()
        {
            FirebaseWrapper.LogEvent("game_start");
        }

        public void LogGameOver(int score, int maxCombo, int livesLost, int ringsHit, float runDurationSeconds)
        {
            FirebaseWrapper.LogEvent("game_over", new Dictionary<string, object>
            {
                ["score"] = score,
                ["max_combo"] = maxCombo,
                ["lives_lost"] = livesLost,
                ["rings_hit"] = ringsHit,
                ["run_duration_seconds"] = runDurationSeconds
            });
        }

        public void LogCreditEarn(int amount, string source)
        {
            FirebaseWrapper.LogEvent("credit_earn", new Dictionary<string, object>
            {
                ["amount"] = amount,
                ["source"] = source
            });
        }

        public void LogCreditSpend(int amount, string reason)
        {
            FirebaseWrapper.LogEvent("credit_spend", new Dictionary<string, object>
            {
                ["amount"] = amount,
                ["reason"] = reason
            });
        }

        public void LogPurchase(string productId, float priceUsd, int creditAmount)
        {
            FirebaseWrapper.LogEvent("iap_purchase", new Dictionary<string, object>
            {
                ["product_id"] = productId,
                ["price_usd"] = priceUsd,
                ["credit_amount"] = creditAmount
            });
        }

        public void LogCosmeticEquipped(string cosmeticId, string cosmeticType)
        {
            FirebaseWrapper.LogEvent("cosmetic_equipped", new Dictionary<string, object>
            {
                ["cosmetic_id"] = cosmeticId,
                ["cosmetic_type"] = cosmeticType
            });
        }

        public void LogRetryOffered(int score, bool accepted)
        {
            FirebaseWrapper.LogEvent("retry_offered", new Dictionary<string, object>
            {
                ["score"] = score,
                ["accepted"] = accepted
            });
        }

        public void LogStreakShieldOffered(int streakDays, bool accepted)
        {
            FirebaseWrapper.LogEvent("streak_shield_offered", new Dictionary<string, object>
            {
                ["streak_days"] = streakDays,
                ["accepted"] = accepted
            });
        }

        public void LogDailyReward(int dayInCycle, int streakTotal)
        {
            FirebaseWrapper.LogEvent("daily_reward_claimed", new Dictionary<string, object>
            {
                ["day_in_cycle"] = dayInCycle,
                ["streak_total"] = streakTotal
            });
        }
    }
}
