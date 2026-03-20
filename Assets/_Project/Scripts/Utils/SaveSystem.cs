// PulseStrike | SaveSystem | Phase 1
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PulseStrike.Utils
{
    public static class SaveSystem
    {
        public const string KEY_PERSONAL_BEST = "PersonalBest";
        public const string KEY_CREDITS = "PlayerCredits";
        public const string KEY_STREAK_DAYS = "StreakDays";
        public const string KEY_LAST_PLAY_DATE = "LastPlayDate";
        public const string KEY_REWARD_PENDING = "RewardPending";
        public const string KEY_OWNED_COSMETICS = "OwnedCosmetics";
        public const string KEY_EQUIPPED_RING = "EquippedRingSkin";
        public const string KEY_EQUIPPED_BG = "EquippedBackground";
        public const string KEY_HAS_LAUNCHED = "HasLaunched";
        public const string KEY_HAS_PURCHASED = "HasMadePurchase";
        public const string KEY_TOTAL_RUNS = "TotalRuns";
        public const string KEY_MILESTONE_CREDITS = "MilestoneCreditsTracker";
        public const string KEY_DAILY_RARE_UNLOCK = "DailyRareUnlock";
        public const string KEY_SFX_VOLUME = "SFXVolume";
        public const string KEY_MUSIC_VOLUME = "MusicVolume";

        public static void Initialize()
        {
            if (!PlayerPrefs.HasKey(KEY_OWNED_COSMETICS))
            {
                PlayerPrefs.SetString(KEY_OWNED_COSMETICS, "skin_default,bg_void");
            }

            if (!PlayerPrefs.HasKey(KEY_EQUIPPED_RING))
            {
                PlayerPrefs.SetString(KEY_EQUIPPED_RING, "skin_default");
            }

            if (!PlayerPrefs.HasKey(KEY_EQUIPPED_BG))
            {
                PlayerPrefs.SetString(KEY_EQUIPPED_BG, "bg_void");
            }

            PlayerPrefs.Save();
        }

        public static int GetPersonalBest() => PlayerPrefs.GetInt(KEY_PERSONAL_BEST, 0);
        public static void SavePersonalBest(int score)
        {
            PlayerPrefs.SetInt(KEY_PERSONAL_BEST, score);
            PlayerPrefs.Save();
        }

        public static int GetCredits() => PlayerPrefs.GetInt(KEY_CREDITS, 0);
        public static void SaveCredits(int credits)
        {
            PlayerPrefs.SetInt(KEY_CREDITS, credits);
            PlayerPrefs.Save();
        }

        public static int GetStreakDays() => PlayerPrefs.GetInt(KEY_STREAK_DAYS, 0);
        public static void SaveStreakDays(int days)
        {
            PlayerPrefs.SetInt(KEY_STREAK_DAYS, days);
            PlayerPrefs.Save();
        }

        public static DateTime GetLastPlayDate()
        {
            var raw = PlayerPrefs.GetString(KEY_LAST_PLAY_DATE, DateTime.MinValue.ToString("O"));
            return DateTime.TryParse(raw, out var dt) ? dt.Date : DateTime.MinValue;
        }

        public static void SaveLastPlayDate(DateTime date)
        {
            PlayerPrefs.SetString(KEY_LAST_PLAY_DATE, date.ToString("O"));
            PlayerPrefs.Save();
        }

        public static bool GetRewardPending() => PlayerPrefs.GetInt(KEY_REWARD_PENDING, 0) == 1;
        public static void SaveRewardPending(bool pending)
        {
            PlayerPrefs.SetInt(KEY_REWARD_PENDING, pending ? 1 : 0);
            PlayerPrefs.Save();
        }

        public static bool OwnsCosmetic(string id)
        {
            var owned = GetOwnedCosmetics();
            return owned.Contains(id);
        }

        public static void UnlockCosmetic(string id)
        {
            var owned = GetOwnedCosmetics();
            if (!owned.Contains(id))
            {
                owned.Add(id);
                SaveOwnedCosmetics(owned);
            }
        }

        public static string GetEquippedRingSkin() => PlayerPrefs.GetString(KEY_EQUIPPED_RING, "skin_default");
        public static void SaveEquippedRingSkin(string skinId)
        {
            PlayerPrefs.SetString(KEY_EQUIPPED_RING, skinId);
            PlayerPrefs.Save();
        }

        public static string GetEquippedBackground() => PlayerPrefs.GetString(KEY_EQUIPPED_BG, "bg_void");
        public static void SaveEquippedBackground(string themeId)
        {
            PlayerPrefs.SetString(KEY_EQUIPPED_BG, themeId);
            PlayerPrefs.Save();
        }

        public static bool HasLaunched() => PlayerPrefs.GetInt(KEY_HAS_LAUNCHED, 0) == 1;
        public static void SetHasLaunched(bool launched)
        {
            PlayerPrefs.SetInt(KEY_HAS_LAUNCHED, launched ? 1 : 0);
            PlayerPrefs.Save();
        }

        public static bool HasPurchased() => PlayerPrefs.GetInt(KEY_HAS_PURCHASED, 0) == 1;
        public static void SetHasPurchased(bool purchased)
        {
            PlayerPrefs.SetInt(KEY_HAS_PURCHASED, purchased ? 1 : 0);
            PlayerPrefs.Save();
        }

        public static void OnRunComplete()
        {
            var totalRuns = PlayerPrefs.GetInt(KEY_TOTAL_RUNS, 0) + 1;
            PlayerPrefs.SetInt(KEY_TOTAL_RUNS, totalRuns);

            var runsSinceMilestone = PlayerPrefs.GetInt(KEY_MILESTONE_CREDITS, 0) + 1;
            if (runsSinceMilestone >= GameplayConstants.MilestoneRewardInterval)
            {
                Economy.CreditManager.Instance.AddCredits(GameplayConstants.MilestoneRewardCredits, "milestone");
                UI.UIManager.Instance.ShowMilestoneRewardToast(GameplayConstants.MilestoneRewardCredits);
                runsSinceMilestone = 0;
            }

            PlayerPrefs.SetInt(KEY_MILESTONE_CREDITS, runsSinceMilestone);
            PlayerPrefs.Save();
        }


        public static float GetSfxVolume() => PlayerPrefs.GetFloat(KEY_SFX_VOLUME, 0.8f);
        public static void SaveSfxVolume(float volume)
        {
            PlayerPrefs.SetFloat(KEY_SFX_VOLUME, volume);
            PlayerPrefs.Save();
        }

        public static float GetMusicVolume() => PlayerPrefs.GetFloat(KEY_MUSIC_VOLUME, 0f);
        public static void SaveMusicVolume(float volume)
        {
            PlayerPrefs.SetFloat(KEY_MUSIC_VOLUME, volume);
            PlayerPrefs.Save();
        }

        public static void SetDailyRareUnlock(bool unlocked)
        {
            PlayerPrefs.SetInt(KEY_DAILY_RARE_UNLOCK, unlocked ? 1 : 0);
            PlayerPrefs.Save();
        }
        private static HashSet<string> GetOwnedCosmetics()
        {
            var raw = PlayerPrefs.GetString(KEY_OWNED_COSMETICS, "skin_default,bg_void");
            return new HashSet<string>(raw.Split(',').Where(s => !string.IsNullOrWhiteSpace(s)));
        }

        private static void SaveOwnedCosmetics(HashSet<string> owned)
        {
            PlayerPrefs.SetString(KEY_OWNED_COSMETICS, string.Join(",", owned));
            PlayerPrefs.Save();
        }
    }
}
