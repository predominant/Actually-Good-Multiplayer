using AGM.Core;
using UnityEngine;

namespace AGM.Client.Windows
{
    public class DebugWindow : MonoBehaviour
    {
        private bool _showWindow = false;
        
        private Rect _windowRect = new Rect(200, 200, 400, 300);
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                _showWindow = !_showWindow;
            }
        }
        
        void OnGUI()
        {
            if (_showWindow)
            {
                _windowRect = GUILayout.Window(67890, _windowRect, DebugWindowGUI, "AGM Debug");
            }
        }
        
        void DebugWindowGUI(int windowId)
        {
            GUILayout.Label("AGM Debug Information");
            if (NetworkManager.Instance != null)
            {
                GUILayout.Label($"Connection State: {NetworkManager.Instance.ConnectionState}");
            }
            else
            {
                GUILayout.Label("NetworkManager instance not found.");
            }
            
            if (GUILayout.Button("Close"))
            {
                _showWindow = false;
            }
            
            GUI.DragWindow(new Rect(0, 0, 10000, 20));
        }
    }
}