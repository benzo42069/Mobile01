// PulseStrike | RingSkinData | Phase 1
using UnityEngine;

namespace PulseStrike.Cosmetics
{
    [CreateAssetMenu(fileName = "RingSkin", menuName = "PulseStrike/Ring Skin")]
    public class RingSkinData : ScriptableObject
    {
        public string skinId;
        public string displayName;
        public Sprite ringSprite;
        public Material ringMaterial;
        public Color primaryColor;
        public Color secondaryColor;
        public int creditCost;
        public bool isDefault;
        public bool isOwned;
        public Sprite thumbnailSprite;
    }
}
