// PulseStrike | ComboTracker | Phase 4
using PulseStrike.Audio;
using PulseStrike.Utils;
using UnityEngine;

namespace PulseStrike.Progression
{
    public class ComboTracker : Singleton<ComboTracker>
    {
        [SerializeField] private int currentCombo;
        [SerializeField] private int sessionMaxCombo;

        public int CurrentCombo => currentCombo;
        public int SessionMaxCombo => sessionMaxCombo;

        public void IncrementCombo()
        {
            currentCombo++;
            if (currentCombo > sessionMaxCombo)
            {
                sessionMaxCombo = currentCombo;
            }

            UI.UIManager.Instance.UpdateCombo(currentCombo);
        }

        public float GetComboAudioPitch()
        {
            return Mathf.Clamp(1f + (currentCombo * 0.02f), 1f, 1.5f);
        }

        public void ResetCombo()
        {
            currentCombo = 0;
            UI.UIManager.Instance.UpdateCombo(currentCombo);
            AudioManager.Instance.PlayComboBreakSound();
        }

        public int GetMultiplier()
        {
            if (currentCombo >= 50) return 3;
            if (currentCombo >= 25) return 2;
            if (currentCombo >= 10) return 2;
            return 1;
        }

        public void ResetForRun()
        {
            currentCombo = 0;
            sessionMaxCombo = 0;
            UI.UIManager.Instance.UpdateCombo(currentCombo);
        }
    }
}
