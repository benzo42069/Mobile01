// PulseStrike | DeathScreen | Phase 11
using PulseStrike.Core;
using TMPro;
using UnityEngine;

namespace PulseStrike.UI
{
    public class DeathScreen : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text bestText;
        [SerializeField] private TMP_Text maxComboText;
        [SerializeField] private GameObject newBestBadge;
        [SerializeField] private GameObject reviveButton;

        public void Show(bool retryEligible)
        {
            root.SetActive(true);
            var score = ScoreManager.Instance.CurrentScore;
            var pb = ScoreManager.Instance.PersonalBest;
            scoreText.text = score.ToString();
            maxComboText.text = $"MAX COMBO: {Progression.ComboTracker.Instance.SessionMaxCombo}";
            var isNewBest = score >= pb;
            newBestBadge.SetActive(isNewBest);
            bestText.gameObject.SetActive(!isNewBest);
            bestText.text = $"BEST: {pb}";
            reviveButton.SetActive(retryEligible);
        }

        public void Hide()
        {
            root.SetActive(false);
        }

        public void OnRetryPressed() => GameManager.Instance.RetryFromGameOver(false);
        public void OnRevivePressed() => GameManager.Instance.RetryFromGameOver(true);
        public void OnHomePressed() => GameManager.Instance.ReturnToMainMenu();
    }
}
