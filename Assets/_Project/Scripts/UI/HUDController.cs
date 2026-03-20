// PulseStrike | HUDController | Phase 11
using TMPro;
using UnityEngine;

namespace PulseStrike.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreLabel;
        [SerializeField] private TMP_Text comboLabel;

        private void Start()
        {
            if (scoreLabel != null)
            {
                scoreLabel.fontStyle = FontStyles.Bold;
            }

            if (comboLabel != null)
            {
                comboLabel.fontStyle = FontStyles.Bold;
            }
        }
    }
}
