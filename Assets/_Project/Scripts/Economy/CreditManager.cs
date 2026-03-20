// PulseStrike | CreditManager | Phase 6
using PulseStrike.Analytics;
using PulseStrike.Utils;

namespace PulseStrike.Economy
{
    public class CreditManager : Singleton<CreditManager>
    {
        private int currentCredits;

        public int CurrentCredits => currentCredits;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        public void InitializeCredits()
        {
            currentCredits = SaveSystem.GetCredits();
        }

        public void AddCredits(int amount, string source)
        {
            currentCredits += amount;
            SaveSystem.SaveCredits(currentCredits);
            AnalyticsManager.Instance.LogCreditEarn(amount, source);
            UI.UIManager.Instance.UpdateCreditDisplay(currentCredits);
        }

        public void SpendCredits(int amount, string reason)
        {
            if (currentCredits < amount)
            {
                throw new InsufficientCreditsException();
            }

            currentCredits -= amount;
            SaveSystem.SaveCredits(currentCredits);
            AnalyticsManager.Instance.LogCreditSpend(amount, reason);
            UI.UIManager.Instance.UpdateCreditDisplay(currentCredits);
        }

        public bool CanAfford(int amount)
        {
            return currentCredits >= amount;
        }
    }
}
