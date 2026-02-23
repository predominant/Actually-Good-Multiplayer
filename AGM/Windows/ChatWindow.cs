using System.Collections.Generic;
using AGM.Core;
using AGM.NetworkMessages;
using UnityEngine;
using EventType = UnityEngine.EventType;

namespace AGM.Client.Windows
{
public class ChatWindow : MonoBehaviour
    {
        private bool showWindow = false;
        private string inputText = "";
        private Vector2 scrollPosition;
        private List<string> chatMessages;
        private NetworkManager networkManager;
        private Rect windowRect = new Rect(20, 20, 400, 300);
        
        void Start()
        {
            chatMessages = new List<string>();
            networkManager = GetComponent<NetworkManager>();
            if (networkManager != null)
            {
                networkManager.OnChatMessageReceived += OnChatReceived;
            }
            GameEvents.onGamePause.Add(OnGamePause);
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T) && !showWindow)
            {
                showWindow = true;
            }
            
            if (Input.GetKeyDown(KeyCode.Escape) && this.showWindow)
            {
                this.showWindow = false;
            }
        }
        
        void OnGUI()
        {
            if (!this.showWindow)
                return;

            this.windowRect = GUILayout.Window(12345, this.windowRect, ChatWindowGUI, "Multiplayer Chat");
        }
        
        void ChatWindowGUI(int windowId)
        {
            GUILayout.BeginVertical();
            
            // Chat messages area
            this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, GUILayout.Height(200));
            foreach (var message in this.chatMessages)
            {
                GUILayout.Label(message);
            }
            GUILayout.EndScrollView();
            
            // Input area
            GUILayout.BeginHorizontal();
            this.inputText = GUILayout.TextField(this.inputText);
            
            if (GUILayout.Button("Send") || (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return))
            {
                if (!string.IsNullOrEmpty(inputText.Trim()))
                {
                    this.networkManager?.SendChatMessage(this.inputText);
                    this.inputText = "";
                }
            }
            GUILayout.EndHorizontal();
            
            if (GUILayout.Button("Close"))
            {
                this.showWindow = false;
            }
            
            GUILayout.EndVertical();
            GUI.DragWindow();
        }
        
        private void OnChatReceived(ChatMessage message)
        {
            var formattedMessage = $"[{message.PlayerName}]: {message.Message}";
            this.networkManager.Logger.Info($"[AGM][Chat] {formattedMessage}");
            this.chatMessages.Add(formattedMessage);
            if (this.chatMessages.Count > 50)
            {
                this.chatMessages.RemoveAt(0);
            }
            this.scrollPosition.y = float.MaxValue;
        }
        
        private void OnGamePause()
        {
            this.showWindow = false;
        }
        
        void OnDestroy()
        {
            GameEvents.onGamePause.Remove(OnGamePause);
        }
    }
    
}