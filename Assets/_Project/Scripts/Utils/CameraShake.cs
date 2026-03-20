// PulseStrike | CameraShake | Phase 6
using UnityEngine;

namespace PulseStrike.Utils
{
    public class CameraShake : MonoBehaviour
    {
        private static CameraShake instance;
        private float shakeDuration;
        private float shakeMagnitude;
        private Vector3 originalPos;

        private void Awake()
        {
            instance = this;
            originalPos = transform.localPosition;
        }

        private void Update()
        {
            if (shakeDuration <= 0f)
            {
                transform.localPosition = originalPos;
                return;
            }

            transform.localPosition = originalPos + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            shakeDuration -= Time.deltaTime;
            shakeMagnitude = Mathf.Max(0f, shakeMagnitude - (Time.deltaTime * shakeMagnitude));
        }

        public static void Trigger(float duration, float magnitude)
        {
            if (instance == null)
            {
                return;
            }

            instance.shakeDuration = duration;
            instance.shakeMagnitude = magnitude;
        }
    }
}
