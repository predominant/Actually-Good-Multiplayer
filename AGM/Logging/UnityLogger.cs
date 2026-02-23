using UnityEngine;

namespace AGM.Logging
{
    public class UnityLogger : ILogger
    {
        public void Info(string message) => UnityEngine.Debug.Log($"[AGM] {message}");
        public void Warning(string message) => UnityEngine.Debug.LogWarning($"[AGM] {message}");
        public void Error(string message) => UnityEngine.Debug.LogError($"[AGM] {message}");
        public void Debug(string message) => UnityEngine.Debug.Log($"[AGM] {message}");
    }
}