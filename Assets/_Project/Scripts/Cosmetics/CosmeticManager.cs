// PulseStrike | CosmeticManager | Phase 8
using System.Collections.Generic;
using System.Linq;
using PulseStrike.Analytics;
using PulseStrike.Utils;
using UnityEngine;

namespace PulseStrike.Cosmetics
{
    public class CosmeticManager : Singleton<CosmeticManager>
    {
        [SerializeField] private List<RingSkinData> ringSkins = new();
        [SerializeField] private List<BackgroundThemeData> backgroundThemes = new();

        public RingSkinData EquippedRingSkin { get; private set; }
        public BackgroundThemeData EquippedBackground { get; private set; }

        public IReadOnlyList<RingSkinData> RingSkins => ringSkins;
        public IReadOnlyList<BackgroundThemeData> BackgroundThemes => backgroundThemes;

        protected override void Awake()
        {
            base.Awake();
            LoadEquippedCosmetics();
        }

        public bool IsOwned(string cosmeticId) => SaveSystem.OwnsCosmetic(cosmeticId);

        public void UnlockCosmetic(string cosmeticId)
        {
            SaveSystem.UnlockCosmetic(cosmeticId);
        }

        public bool TryBuyRingSkin(RingSkinData skin)
        {
            if (SaveSystem.OwnsCosmetic(skin.skinId))
            {
                EquipRingSkin(skin.skinId);
                return true;
            }

            if (!Economy.CreditManager.Instance.CanAfford(skin.creditCost))
            {
                return false;
            }

            Economy.CreditManager.Instance.SpendCredits(skin.creditCost, $"cosmetic_{skin.skinId}");
            SaveSystem.UnlockCosmetic(skin.skinId);
            EquipRingSkin(skin.skinId);
            return true;
        }

        public bool TryBuyBackground(BackgroundThemeData bg)
        {
            if (SaveSystem.OwnsCosmetic(bg.themeId))
            {
                EquipBackground(bg.themeId);
                return true;
            }

            if (!Economy.CreditManager.Instance.CanAfford(bg.creditCost))
            {
                return false;
            }

            Economy.CreditManager.Instance.SpendCredits(bg.creditCost, $"cosmetic_{bg.themeId}");
            SaveSystem.UnlockCosmetic(bg.themeId);
            EquipBackground(bg.themeId);
            return true;
        }

        public void EquipRingSkin(string skinId)
        {
            var skin = ringSkins.FirstOrDefault(s => s.skinId == skinId) ?? ringSkins.FirstOrDefault(s => s.isDefault);
            if (skin == null)
            {
                return;
            }

            EquippedRingSkin = skin;
            SaveSystem.SaveEquippedRingSkin(skin.skinId);
            AnalyticsManager.Instance.LogCosmeticEquipped(skin.skinId, "ring_skin");
        }

        public void EquipBackground(string themeId)
        {
            var bg = backgroundThemes.FirstOrDefault(t => t.themeId == themeId) ?? backgroundThemes.FirstOrDefault(t => t.isDefault);
            if (bg == null)
            {
                return;
            }

            EquippedBackground = bg;
            SaveSystem.SaveEquippedBackground(bg.themeId);
            AnalyticsManager.Instance.LogCosmeticEquipped(bg.themeId, "background");
        }

        public void LoadEquippedCosmetics()
        {
            EquipRingSkin(SaveSystem.GetEquippedRingSkin());
            EquipBackground(SaveSystem.GetEquippedBackground());
        }
    }
}
