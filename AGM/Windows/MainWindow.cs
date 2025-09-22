using System;
using AGM.Core;
using UnityEngine;

namespace AGM.Client.Windows
{
    public class MainWindow : MonoBehaviour
    {
        // Main window for multiplayer controls
        private bool _showWindow = true;
        
        private Rect _windowRect = new Rect(100, 100, 300, 200);
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
                this._showWindow = !this._showWindow;
        }
        
        void OnGUI()
        {
            if (!_showWindow || NetworkManager.Instance == null)
                return;

            this._windowRect = GUILayout.Window(54321, this._windowRect, this.MainWindowGUI, "Multiplayer");
        }
        
        void MainWindowGUI(int windowId)
        {
            GUILayout.Label("Multiplayer Controls");
            
            if (NetworkManager.Instance.ConnectionState == ConnectionState.Disconnected)
            {
                if (GUILayout.Button("Start Server"))
                    NetworkManager.Instance?.StartServer();

                if (GUILayout.Button("Connect to Server"))
                    NetworkManager.Instance?.ConnectToServer("127.0.0.1", 7777);
            }
            else if (NetworkManager.Instance.ConnectionState == ConnectionState.ClientConnected)
            {
                if (GUILayout.Button("Disconnect"))
                    NetworkManager.Instance?.DisconnectClient();
            }
            else if (NetworkManager.Instance.ConnectionState == ConnectionState.ServerRunning)
            {
                if (GUILayout.Button("Stop Server"))
                    NetworkManager.Instance?.StopServer();
            }

            if (GUILayout.Button("Close"))
                _showWindow = false;

            GUI.DragWindow(new Rect(0, 0, 10000, 20));
        }

        public void OnApplicationQuit()
        {
            this.OnExit();
        }
        
        public void OnDestroy()
        {
            this.OnExit();
        }
        
        public void OnExit()
        {
            NetworkManager.Instance?.OnDestroy();
        }
    }
}