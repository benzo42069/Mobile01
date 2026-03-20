// PulseStrike | FirebaseWrapper | Phase 10
using System.Collections.Generic;
using UnityEngine;

namespace PulseStrike.Analytics
{
    public static class FirebaseWrapper
    {
        public static void LogEvent(string eventName, Dictionary<string, object> parameters = null)
        {
            // TODO: Hook into FirebaseAnalytics.LogEvent when Firebase package is present.
            if (parameters == null)
            {
                Debug.Log($"[Analytics] {eventName}");
                return;
            }

            Debug.Log($"[Analytics] {eventName} | params={parameters.Count}");
        }

        public static void Initialize()
        {
            // TODO: Initialize Firebase dependencies and fetch Remote Config values.
            Debug.Log("[Analytics] FirebaseWrapper.Initialize called.");
        }
    }
}
