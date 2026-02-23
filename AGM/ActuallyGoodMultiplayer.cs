using System;
using AGM.Client.Windows;
using AGM.Core;
using AGM.Logging;
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
            
            this._networkManager = new NetworkManager(new UnityLogger());
            
            this._mainWindow = this.gameObject.AddComponent<MainWindow>();
            this._chatWindow = this.gameObject.AddComponent<ChatWindow>();
            this._debugWindow = this.gameObject.AddComponent<DebugWindow>();
        }
        
        void Update()
        {
        }
    }
}