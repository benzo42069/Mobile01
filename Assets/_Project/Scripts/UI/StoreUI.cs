// PulseStrike | StoreUI | Phase 11
using PulseStrike.Cosmetics;
using PulseStrike.Economy;
using TMPro;
using UnityEngine;

namespace PulseStrike.UI
{
    public class StoreUI : MonoBehaviour
    {
        [SerializeField] private GameObject panelRoot;
        [SerializeField] private TMP_Text creditsLabel;

        public void Show()
        {
            panelRoot.SetActive(true);
            RefreshCredits();
        }

        public void Hide()
        {
            panelRoot.SetActive(false);
        }

        public void BuyRing(RingSkinData skin)
        {
            if (!CosmeticManager.Instance.TryBuyRingSkin(skin))
            {
                UIManager.Instance.ShowPurchaseError("NOT ENOUGH CREDITS");
            }

            RefreshCredits();
        }

        public void BuyBackground(BackgroundThemeData background)
        {
            if (!CosmeticManager.Instance.TryBuyBackground(background))
            {
                UIManager.Instance.ShowPurchaseError("NOT ENOUGH CREDITS");
            }

            RefreshCredits();
        }

        public void PurchaseIAPStarter() => IAPManager.Instance.PurchaseProduct(IAPManager.ProductStarter);
        public void PurchaseIAPStandard() => IAPManager.Instance.PurchaseProduct(IAPManager.ProductStandard);

        private void RefreshCredits()
        {
            creditsLabel.text = CreditManager.Instance.CurrentCredits.ToString();
        }
    }
}
