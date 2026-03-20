// PulseStrike | BackgroundThemeData | Phase 1
using UnityEngine;

namespace PulseStrike.Cosmetics
{
    [CreateAssetMenu(fileName = "BackgroundTheme", menuName = "PulseStrike/Background Theme")]
    public class BackgroundThemeData : ScriptableObject
    {
        public string themeId;
        public string themeName;
        public Color backgroundColor;
        public Gradient backgroundGradient;
        public Sprite backgroundSprite;
        public Material backgroundMaterial;
        public int creditCost;
        public bool isDefault;
    }
}
