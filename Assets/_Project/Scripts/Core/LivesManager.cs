// PulseStrike | LivesManager | Phase 4
using PulseStrike.Progression;
using PulseStrike.Utils;
using UnityEngine;

namespace PulseStrike.Core
{
    public class LivesManager : Singleton<LivesManager>
    {
        [SerializeField] private int currentLives = GameplayConstants.InitialLives;
        [SerializeField] private int maxLives = GameplayConstants.InitialLives;
        [SerializeField] private int livesLost;

        public int CurrentLives => currentLives;
        public int LivesLost => livesLost;

        public void LoseLife()
        {
            currentLives--;
            livesLost++;
            UI.UIManager.Instance.UpdateLivesDisplay(currentLives);
            ComboTracker.Instance.ResetCombo();

            if (currentLives <= 0)
            {
                GameManager.Instance.OnGameOver();
            }
        }

        public void RestoreLife(int amount)
        {
            currentLives = Mathf.Min(maxLives, currentLives + amount);
            UI.UIManager.Instance.UpdateLivesDisplay(currentLives);
        }

        public void ResetForRun()
        {
            currentLives = maxLives;
            livesLost = 0;
            UI.UIManager.Instance.UpdateLivesDisplay(currentLives);
        }
    }
}
