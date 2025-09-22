using System;
using AGM.Client.Windows;
using AGM.Core;
using UnityEngine;

namespace AGM
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class ActuallyGoodMultiplayer : MonoBehaviour
    {
        private MainWindow _mainWindow;
        private ChatWindow _chatWindow;
        private DebugWindow _debugWindow;
        
        public static ActuallyGoodMultiplayer Instance { get; private set; }
        
        private NetworkManager _networkManager;

        void Start()
        {
            if (Instance != null && Instance != this)
            {
                GameObject.Destroy(this);
                return;
            }
            Instance = this;
            
            GameObject.DontDestroyOnLoad(this);
            
            this._networkManager = new NetworkManager();
            
            this._mainWindow = this.gameObject.AddComponent<MainWindow>();
            this._chatWindow = this.gameObject.AddComponent<ChatWindow>();
            this._debugWindow = this.gameObject.AddComponent<DebugWindow>();
        }
        
        void Update()
        {
        }

        public void DebugLog(string msg)
        {
            this.DebugLog(LogType.Log, msg);
        }
        
        public void DebugLog(LogType logType, string msg)
        {
            // Detect if we're in KSP or console
            if (Application.isBatchMode)
            {
                // Console mode
                switch (logType)
                {
                    case LogType.Warning:
                        Console.WriteLine("[AGM] WARNING: " + msg);
                        break;
                    case LogType.Error:
                        Console.WriteLine("[AGM] ERROR: " + msg);
                        break;
                    case LogType.Log:
                    default:
                        Console.WriteLine("[AGM] " + msg);
                        break;
                }
                return;
            }
            else
            {
                switch (logType)
                {
                    case LogType.Warning:
                        Debug.LogWarning("[AGM] " + msg);
                        break;
                    case LogType.Error:
                        Debug.LogError("[AGM] " + msg);
                        break;
                    case LogType.Log:
                    default:
                        Debug.Log("[AGM] " + msg);
                        break;
                }
            }
            
        }
    }
}