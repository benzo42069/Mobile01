// PulseStrike | UIManager | Phase 11
using System;
using PulseStrike.Audio;
using PulseStrike.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PulseStrike.UI
{
    public class UIManager : Singleton<UIManager>
    {
        [Header("HUD")]
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text comboText;
        [SerializeField] private TMP_Text creditText;
        [SerializeField] private Image[] lifeIcons;
        [SerializeField] private GameObject tapToStartOverlay;
        [SerializeField] private GameObject pauseOverlay;

        [Header("Panels")]
        [SerializeField] private DeathScreen deathScreen;
        [SerializeField] private StoreUI storeUI;
        [SerializeField] private DailyRewardPanel dailyRewardPanel;

        [Header("Effects")]
        [SerializeField] private Image screenEdgeFlash;

        public void UpdateScore(int score)
        {
            if (scoreText != null)
            {
                scoreText.text = score.ToString();
            }
        }

        public void UpdateCombo(int combo)
        {
            if (comboText == null)
            {
                return;
            }

            comboText.gameObject.SetActive(combo >= 5);
            comboText.text = $"COMBO {combo}";
            comboText.color = Colors.Accent1;
        }

        public void UpdateLivesDisplay(int currentLives)
        {
            for (var i = 0; i < lifeIcons.Length; i++)
            {
                lifeIcons[i].color = i < currentLives ? Colors.Accent1 : Colors.Muted;
            }
        }

        public void UpdateCreditDisplay(int credits)
        {
            if (creditText != null)
            {
                creditText.text = credits.ToString();
            }
        }

        public void ShowTapToStart(bool show)
        {
            if (tapToStartOverlay != null)
            {
                tapToStartOverlay.SetActive(show);
            }
        }

        public void ShowPauseOverlay(bool show)
        {
            if (pauseOverlay != null)
            {
                pauseOverlay.SetActive(show);
            }
        }

        public void ShowDeathScreen(bool retryEligible)
        {
            if (deathScreen != null)
            {
                deathScreen.Show(retryEligible);
            }
        }

        public void HideDeathScreen()
        {
            if (deathScreen != null)
            {
                deathScreen.Hide();
            }
        }

        public void ShowScorePopup(int points, Vector2 screenPosition)
        {
            // TODO: Spawn score popup prefab from Effects/ScorePopup and set anchored position.
        }

        public void FlashScreenEdge(Color color, float duration)
        {
            if (screenEdgeFlash == null)
            {
                return;
            }

            screenEdgeFlash.color = color;
            screenEdgeFlash.gameObject.SetActive(true);
            CancelInvoke(nameof(HideScreenEdgeFlash));
            Invoke(nameof(HideScreenEdgeFlash), duration);
        }

        public void ShowPurchaseError(string reason)
        {
            Debug.LogError($"Purchase failed: {reason}");
        }

        public void ShowMilestoneRewardToast(int credits)
        {
            Debug.Log($"Milestone reward granted: {credits} credits");
        }

        public void ShowStreakShieldPrompt(int streakDays, int cost, Action<bool> callback)
        {
            // TODO: Replace with dedicated streak shield modal UI.
            callback?.Invoke(false);
        }

        public void OpenStore()
        {
            AudioManager.Instance.PlayUIButtonSound();
            storeUI?.Show();
        }

        public void OpenDailyReward()
        {
            dailyRewardPanel?.Show();
        }

        private void HideScreenEdgeFlash()
        {
            if (screenEdgeFlash != null)
            {
                screenEdgeFlash.gameObject.SetActive(false);
            }
        }
    }
}
