// PulseStrike | TapHandler | Phase 2
using PulseStrike.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PulseStrike.Mechanics
{
    public class TapHandler : MonoBehaviour
    {
        private void Update()
        {
            if (Touchscreen.current != null)
            {
                foreach (var touch in Touchscreen.current.touches)
                {
                    if (touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
                    {
                        OnTap();
                        return;
                    }
                }
            }

            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                OnTap();
            }
        }

        private void OnTap()
        {
            GameManager.Instance.ProcessTap();
        }
    }
}
