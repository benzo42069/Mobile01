// PulseStrike | IAPManager | Phase 7
using PulseStrike.Analytics;
using PulseStrike.Utils;
using UnityEngine;
#if UNITY_PURCHASING
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
#endif

namespace PulseStrike.Economy
{
#if UNITY_PURCHASING
    public class IAPManager : Singleton<IAPManager>, IDetailedStoreListener
#else
    public class IAPManager : Singleton<IAPManager>
#endif
    {
        public const string ProductStarter = "com.yourstudio.pulsestrike.credits_starter";
        public const string ProductStandard = "com.yourstudio.pulsestrike.credits_standard";

#if UNITY_PURCHASING
        private IStoreController storeController;
        private IExtensionProvider extensionProvider;
#endif

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        public void InitializePurchasing()
        {
#if UNITY_PURCHASING
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct(ProductStarter, ProductType.Consumable);
            builder.AddProduct(ProductStandard, ProductType.Consumable);
            UnityPurchasing.Initialize(this, builder);
#else
            Debug.LogWarning("Unity IAP package is not installed. IAPManager.InitializePurchasing is a no-op.");
#endif
        }

        public bool ShouldShowStarterPack()
        {
            return !SaveSystem.HasPurchased();
        }

        public void PurchaseProduct(string productId)
        {
#if UNITY_PURCHASING
            storeController?.InitiatePurchase(productId);
#else
            Debug.LogWarning($"PurchaseProduct no-op for {productId} without Unity IAP package.");
#endif
        }

#if UNITY_PURCHASING
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            storeController = controller;
            extensionProvider = extensions;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.LogError($"IAP Init Failed: {error}");
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.LogError($"IAP Init Failed: {error} ({message})");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            var product = purchaseEvent.purchasedProduct;
            switch (product.definition.id)
            {
                case ProductStarter:
                    CreditManager.Instance.AddCredits(100, "iap_starter");
                    break;
                case ProductStandard:
                    CreditManager.Instance.AddCredits(600, "iap_standard");
                    break;
            }

            SaveSystem.SetHasPurchased(true);
            AnalyticsManager.Instance.LogPurchase(product.definition.id, (float)product.metadata.localizedPrice, product.definition.id == ProductStarter ? 100 : 600);
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.LogError($"IAP Failed: {failureDescription.reason}");
            UI.UIManager.Instance.ShowPurchaseError(failureDescription.reason.ToString());
        }
#endif
    }
}
