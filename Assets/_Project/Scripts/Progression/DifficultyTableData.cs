// PulseStrike | DifficultyTableData | Phase 1
using System;
using UnityEngine;

namespace PulseStrike.Progression
{
    [CreateAssetMenu(fileName = "DifficultyTable", menuName = "PulseStrike/Difficulty Table")]
    public class DifficultyTableData : ScriptableObject
    {
        [Serializable]
        public struct DifficultyThreshold
        {
            public int hitsRequired;
            public float ringSpeed;
            public float bandWidth;
            public int maxRings;
            public float spawnInterval;
            public bool ghostsEnabled;
            public bool bandRotates;
        }

        public DifficultyThreshold[] thresholds;
    }
}
