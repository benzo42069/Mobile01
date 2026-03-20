// PulseStrike | GameManager | Phase 3
using System.Collections.Generic;
using PulseStrike.Analytics;
using PulseStrike.Audio;
using PulseStrike.Mechanics;
using PulseStrike.Progression;
using PulseStrike.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PulseStrike.Core
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private GameState currentState = GameState.Idle;
        [SerializeField] private float runStartTime;
        [SerializeField] private bool retryUsedThisRun;

        public GameState CurrentState => currentState;

        protected override void Awake()
        {
            base.Awake();
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
        }

        private void Start()
        {
            ResetRunStateToIdle();
        }

        public void ProcessTap()
        {
            if (currentState == GameState.Idle)
            {
                StartGameplay();
                return;
            }

            if (currentState != GameState.Playing)
            {
                return;
            }

            var activeRings = RingSpawner.Instance.ActiveRings;
            if (activeRings.Count == 0)
            {
                return;
            }

            var sorted = new List<RingController>(activeRings);
            sorted.Sort((a, b) =>
            {
                var da = Mathf.Abs(a.currentRadius - TargetBand.Instance.bandRadius);
                var db = Mathf.Abs(b.currentRadius - TargetBand.Instance.bandRadius);
                return da.CompareTo(db);
            });

            foreach (var ring in sorted)
            {
                if (ring == null || ring.isGhost)
                {
                    continue;
                }

                var delta = Mathf.Abs(ring.currentRadius - TargetBand.Instance.bandRadius);
                var halfBand = TargetBand.Instance.bandWidth / 2f;
                if (delta <= halfBand)
                {
                    OnRingHit(ring);
                    return;
                }
            }

            OnMiss();
        }

        public void OnRingHit(RingController ring)
        {
            ring.SetAlive(false);
            ring.TriggerShatterEffect();
            ScoreManager.Instance.AddHit();
            ComboTracker.Instance.IncrementCombo();
            HapticManager.Hit();
            AudioManager.Instance.PlayHitSound(ComboTracker.Instance.CurrentCombo);
            DifficultyManager.Instance.OnHit();
            Destroy(ring.gameObject, 0.1f);
        }

        public void OnMiss()
        {
            var hasNonGhost = false;
            foreach (var ring in RingSpawner.Instance.ActiveRings)
            {
                if (ring != null && !ring.isGhost)
                {
                    hasNonGhost = true;
                    break;
                }
            }

            if (!hasNonGhost)
            {
                return;
            }

            LivesManager.Instance.LoseLife();
            HapticManager.Miss();
            AudioManager.Instance.PlayMissSound();
            CameraShake.Trigger(0.15f, 0.3f);
            UI.UIManager.Instance.FlashScreenEdge(Colors.Accent2, 0.3f);
        }

        public void OnRingEscaped()
        {
            if (currentState == GameState.Playing)
            {
                LivesManager.Instance.LoseLife();
            }
        }

        public void PauseGame()
        {
            if (currentState != GameState.Playing)
            {
                return;
            }

            currentState = GameState.Paused;
            Time.timeScale = 0f;
            UI.UIManager.Instance.ShowPauseOverlay(true);
        }

        public void ResumeGame()
        {
            if (currentState != GameState.Paused)
            {
                return;
            }

            Time.timeScale = 1f;
            currentState = GameState.Playing;
            UI.UIManager.Instance.ShowPauseOverlay(false);
        }

        public void OnGameOver()
        {
            if (currentState == GameState.GameOver)
            {
                return;
            }

            currentState = GameState.GameOver;
            RingSpawner.Instance.StopSpawning();
            Invoke(nameof(DestroyActiveRings), 0.5f);
            SaveSystem.OnRunComplete();
            var runSeconds = Time.time - runStartTime;
            AnalyticsManager.Instance.LogGameOver(ScoreManager.Instance.CurrentScore, ComboTracker.Instance.SessionMaxCombo, LivesManager.Instance.LivesLost, ScoreManager.Instance.RingsHit, runSeconds);
            UI.UIManager.Instance.ShowDeathScreen(ShouldOfferRetry());
        }

        public void RetryFromGameOver(bool revive)
        {
            if (currentState != GameState.GameOver)
            {
                return;
            }

            if (revive)
            {
                if (!Economy.CreditManager.Instance.CanAfford(GameplayConstants.RetryCreditCost))
                {
                    return;
                }

                Economy.CreditManager.Instance.SpendCredits(GameplayConstants.RetryCreditCost, "retry");
                LivesManager.Instance.RestoreLife(1);
                retryUsedThisRun = true;
                currentState = GameState.Playing;
                RingSpawner.Instance.StartSpawning();
                UI.UIManager.Instance.HideDeathScreen();
                return;
            }

            ResetRunStateToIdle();
        }

        public void ReturnToMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneNames.MainMenu, LoadSceneMode.Single);
        }

        public bool ShouldOfferRetry()
        {
            if (!Economy.CreditManager.Instance.CanAfford(GameplayConstants.RetryCreditCost)) return false;
            if (ScoreManager.Instance.CurrentScore < ScoreManager.Instance.PersonalBest * 0.5f) return false;
            if (retryUsedThisRun) return false;
            return true;
        }

        private void StartGameplay()
        {
            currentState = GameState.Playing;
            runStartTime = Time.time;
            RingSpawner.Instance.StartSpawning();
            UI.UIManager.Instance.ShowTapToStart(false);
            AnalyticsManager.Instance.LogGameStart();
        }

        private void ResetRunStateToIdle()
        {
            Time.timeScale = 1f;
            currentState = GameState.Idle;
            retryUsedThisRun = false;
            ScoreManager.Instance.ResetForRun();
            ComboTracker.Instance.ResetForRun();
            LivesManager.Instance.ResetForRun();
            DifficultyManager.Instance.ResetForRun();
            RingSpawner.Instance.DestroyAllRings();
            RingSpawner.Instance.StopSpawning();
            UI.UIManager.Instance.ShowTapToStart(true);
            UI.UIManager.Instance.HideDeathScreen();
        }

        private void DestroyActiveRings()
        {
            RingSpawner.Instance.DestroyAllRings();
        }
    }
}
