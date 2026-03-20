// PulseStrike | DifficultyManager | Phase 5
using System.Linq;
using PulseStrike.Mechanics;
using PulseStrike.Utils;
using UnityEngine;

namespace PulseStrike.Progression
{
    public class DifficultyManager : Singleton<DifficultyManager>
    {
        [SerializeField] private DifficultyTableData difficultyTable;
        [SerializeField] private int currentHitCount;

        public float CurrentRingSpeed { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            ApplyThreshold(GetCurrentThreshold());
        }

        public void OnHit()
        {
            currentHitCount++;
            CheckThresholds();
        }

        public void CheckThresholds()
        {
            ApplyThreshold(GetCurrentThreshold());
        }

        public void ApplyThreshold(DifficultyTableData.DifficultyThreshold t)
        {
            CurrentRingSpeed = t.ringSpeed;
            RingSpawner.Instance.SetSpawnInterval(t.spawnInterval);
            RingSpawner.Instance.SetMaxRings(t.maxRings);
            RingSpawner.Instance.SetRingSpeed(t.ringSpeed);
            TargetBand.Instance.SetBandWidth(t.bandWidth);
            RingSpawner.Instance.SetGhostsEnabled(t.ghostsEnabled);
        }

        public void ResetForRun()
        {
            currentHitCount = 0;
            CheckThresholds();
        }

        private DifficultyTableData.DifficultyThreshold GetCurrentThreshold()
        {
            if (difficultyTable == null || difficultyTable.thresholds == null || difficultyTable.thresholds.Length == 0)
            {
                return new DifficultyTableData.DifficultyThreshold
                {
                    hitsRequired = 0,
                    ringSpeed = 1.5f,
                    bandWidth = GameplayConstants.InitialBandWidth,
                    maxRings = 1,
                    spawnInterval = 1.8f,
                    ghostsEnabled = false,
                    bandRotates = false
                };
            }

            return difficultyTable.thresholds.Last(t => t.hitsRequired <= currentHitCount);
        }
    }
}
