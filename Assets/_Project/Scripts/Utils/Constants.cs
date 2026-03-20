// PulseStrike | Constants | Phase 1
using UnityEngine;

namespace PulseStrike.Utils
{
    public static class Colors
    {
        public static readonly Color BG = new Color(0.031f, 0.043f, 0.063f);
        public static readonly Color Panel = new Color(0.051f, 0.071f, 0.094f);
        public static readonly Color Border = new Color(0.102f, 0.133f, 0.188f);
        public static readonly Color Accent1 = new Color(0.000f, 0.898f, 1.000f);
        public static readonly Color Accent2 = new Color(1.000f, 0.176f, 0.420f);
        public static readonly Color Accent3 = new Color(1.000f, 0.902f, 0.000f);
        public static readonly Color Text = new Color(0.784f, 0.847f, 0.910f);
        public static readonly Color Muted = new Color(0.290f, 0.376f, 0.439f);
        public static readonly Color White = new Color(0.933f, 0.957f, 0.973f);
    }

    public static class GameplayConstants
    {
        public const float MaxRingRadius = 5.5f;
        public const float InitialBandRadius = 3.8f;
        public const float InitialBandWidth = 0.30f;
        public const int InitialLives = 3;
        public const int ScorePerHit = 10;
        public const int RetryCreditCost = 30;
        public const int StreakShieldCost = 50;
        public const float GhostRingProbability = 0.15f;
        public const int MilestoneRewardInterval = 10;
        public const int MilestoneRewardCredits = 3;
    }

    public static class SceneNames
    {
        public const string Bootstrap = "Bootstrap";
        public const string MainMenu = "MainMenu";
        public const string Game = "Game";
    }
}
