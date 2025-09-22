using System;
using System.Threading;
using AGM.Core;
using AGM.NetworkMessages;
using Lidgren.Network;
using UnityEngine;

namespace AGM
{
    public class NetworkManager
    {
        public static NetworkManager Instance { get; private set; }
        
        private NetServer _server;
        private NetClient _client;
        
        public ConnectionState ConnectionState = ConnectionState.Disconnected;
        public NetPeerConfiguration ServerConfig;
        public NetPeerConfiguration ClientConfig;
        
        public NetworkManager()
        {
            ActuallyGoodMultiplayer.Instance.DebugLog("[AGM] NetworkManager initializing...");
            if (Instance != null && Instance != this)
            {
                return;
            }
            Instance = this;
            
            this.ServerConfig = new NetPeerConfiguration("AGM");
            this.ServerConfig.Port = 6969;
            this.ServerConfig.MaximumConnections = 100;
            ActuallyGoodMultiplayer.Instance.DebugLog("[AGM] NetworkManager initialized server settings.");
            
            this.ClientConfig = new NetPeerConfiguration("AGM");
            this.ClientConfig.AutoFlushSendQueue = false;
            ActuallyGoodMultiplayer.Instance.DebugLog("[AGM] NetworkManager initialized client settings.");
        }

        public void OnDestroy()
        {
            this.StopServer();
            this.DisconnectClient();
        }

        public void StartServer()
        {
            this._server = new NetServer(this.ServerConfig);
            this._server.Start();
            this.ConnectionState = ConnectionState.ServerRunning;
            ActuallyGoodMultiplayer.Instance.DebugLog("[AGM] Server started on port " + this.ServerConfig.Port);
        }
        
        public void StopServer()
        {
            if (this._server != null)
            {
                this._server.Shutdown("Server shutting down");
                this._server = null;
                this.ConnectionState = ConnectionState.Disconnected;
                ActuallyGoodMultiplayer.Instance.DebugLog("[AGM] Server stopped.");
            }
        }
        
        public void ConnectToServer(string ip, int port)
        {
            this._client = new NetClient(this.ClientConfig);
            this._client.RegisterReceivedCallback(new SendOrPostCallback(this.ClientMessageReceived));
            
            ActuallyGoodMultiplayer.Instance.DebugLog("[AGM] Connecting to server at " + ip + ":" + port);
            this.ConnectionState = ConnectionState.ClientConnected;
        }
        
        public void DisconnectClient()
        {
            if (this._client == null)
                return;

            this._client.Disconnect("Client disconnecting");
            this._client = null;
            this.ConnectionState = ConnectionState.Disconnected;
            ActuallyGoodMultiplayer.Instance.DebugLog("[AGM] Client disconnected.");
        }
        
        private void ClientMessageReceived(object peer)
        {
            var client = (NetClient)peer;
            NetIncomingMessage msg;
            while ((msg = client.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.StatusChanged:
                        var status = (NetConnectionStatus)msg.ReadByte();
                        ActuallyGoodMultiplayer.Instance.DebugLog("[AGM] Client status changed: " + status);
                        if (status == NetConnectionStatus.Disconnected)
                        {
                            this.ConnectionState = ConnectionState.Disconnected;
                        }
                        break;
                    case NetIncomingMessageType.Data:
                        Debug.Log("[AGM] Received data message of length: " + msg.LengthBytes);
                        // var chatMsg = ChatMessage.Deserialize(msg);
                        // OnChatMessageReceived?.Invoke(chatMsg);
                        break;
                    default:
                        ActuallyGoodMultiplayer.Instance.DebugLog("[AGM] Unhandled message type: " + msg.MessageType);
                        break;
                }
                client.Recycle(msg);
            }
        }
        
        public Action<ChatMessage> OnChatMessageReceived;
        
        public void SendChatMessage(string text)
        {
            if (this._client == null || this.ConnectionState != ConnectionState.ClientConnected)
            {
                ActuallyGoodMultiplayer.Instance.DebugLog(LogType.Warning, "[AGM] Cannot send chat message, client not connected.");
                return;
            }
            
            var outgoingMsg = this._client.CreateMessage(text);
            this._client.SendMessage(outgoingMsg, NetDeliveryMethod.ReliableOrdered);
            this._client.FlushSendQueue();
            ActuallyGoodMultiplayer.Instance.DebugLog("[AGM] Sent chat message: " + text);
        }
    }
}