// PulseStrike | ScoreManager | Phase 4
using PulseStrike.Progression;
using PulseStrike.Utils;
using UnityEngine;

namespace PulseStrike.Core
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        [SerializeField] private float currentScore;
        [SerializeField] private int personalBest;
        [SerializeField] private int ringsHit;

        public int CurrentScore => Mathf.FloorToInt(currentScore);
        public int PersonalBest => personalBest;
        public int RingsHit => ringsHit;

        protected override void Awake()
        {
            base.Awake();
            personalBest = SaveSystem.GetPersonalBest();
        }

        public void AddHit()
        {
            var multiplier = ComboTracker.Instance.GetMultiplier();
            var points = GameplayConstants.ScorePerHit * multiplier;
            currentScore += points;
            ringsHit++;

            UI.UIManager.Instance.UpdateScore(CurrentScore);
            if (CurrentScore > personalBest)
            {
                personalBest = CurrentScore;
                SaveSystem.SavePersonalBest(personalBest);
            }

            UI.UIManager.Instance.ShowScorePopup(points, GetRandomRingPosition());
        }

        public Vector2 GetRandomRingPosition()
        {
            var angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            var screenRadius = Camera.main.WorldToScreenPoint(Vector3.right * Mechanics.TargetBand.Instance.bandRadius).x - (Screen.width / 2f);
            return new Vector2(
                (Screen.width / 2f) + Mathf.Cos(angle) * screenRadius,
                (Screen.height / 2f) + Mathf.Sin(angle) * screenRadius
            );
        }

        public void ResetForRun()
        {
            currentScore = 0f;
            ringsHit = 0;
            UI.UIManager.Instance.UpdateScore(CurrentScore);
        }
    }
}
