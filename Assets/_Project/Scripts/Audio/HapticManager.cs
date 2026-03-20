// PulseStrike | HapticManager | Phase 9
using System.Runtime.InteropServices;
using UnityEngine;

namespace PulseStrike.Audio
{
    public static class HapticManager
    {
#if UNITY_IOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void _TriggerImpact(int style);

        [DllImport("__Internal")]
        private static extern void _TriggerNotification(int type);
#endif

        public static void Hit()
        {
#if UNITY_IOS && !UNITY_EDITOR
            _TriggerImpact(1);
#endif
        }

        public static void Miss()
        {
#if UNITY_IOS && !UNITY_EDITOR
            _TriggerImpact(2);
#endif
        }

        public static void UITap()
        {
#if UNITY_IOS && !UNITY_EDITOR
            _TriggerImpact(0);
#endif
        }

        public static void Success()
        {
#if UNITY_IOS && !UNITY_EDITOR
            _TriggerNotification(1);
#endif
        }
    }
}
