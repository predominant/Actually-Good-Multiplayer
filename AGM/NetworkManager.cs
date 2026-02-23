using System;
using System.Threading;
using AGM.Core;
using AGM.Logging;
using AGM.NetworkMessages;
using Lidgren.Network;
using UnityEngine;
using ILogger = UnityEngine.ILogger;

namespace AGM
{
    public class NetworkManager
    {
        public static NetworkManager Instance { get; private set; }
        public Logging.ILogger Logger { get; private set; }
        
        private NetServer _server;
        private NetClient _client;
        
        public ConnectionState ConnectionState = ConnectionState.Disconnected;
        public NetPeerConfiguration ServerConfig;
        public NetPeerConfiguration ClientConfig;
        
        public NetworkManager(Logging.ILogger logger)
        {
            this.Logger = logger;
            
            this.Logger.Info("NetworkManager initializing...");
            if (Instance != null && Instance != this)
            {
                return;
            }
            Instance = this;

            this.ServerConfig = new NetPeerConfiguration("AGM");
            this.ServerConfig.Port = 6969;
            this.ServerConfig.MaximumConnections = 1000;
            this.Logger.Info("NetworkManager initialized server settings.");

            this.ClientConfig = new NetPeerConfiguration("AGM");
            this.ClientConfig.AutoFlushSendQueue = false;
            this.Logger.Info("NetworkManager initialized client settings.");
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
            this.Logger.Info("Server started on port " + this.ServerConfig.Port);
        }
        
        public void StopServer()
        {
            if (this._server != null)
            {
                this._server.Shutdown("Server shutting down");
                this._server = null;
                this.ConnectionState = ConnectionState.Disconnected;
                this.Logger.Info("Server stopped.");
            }
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public void ConnectToServer(string ip, int port)
        {
            this._client = new NetClient(this.ClientConfig);
            // this._client.RegisterReceivedCallback(new System.Threading.SendOrPostCallback(this.ClientMessageReceived));
            
            this.Logger.Info($"Connecting to server at {ip}:{port}...");
            this.ConnectionState = ConnectionState.ClientConnected;
        }

        // For console apps: manually poll for messages
        public void PollClientMessages()
        {
            if (this._client == null) return;
            ClientMessageReceived(this._client);
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public void DisconnectClient()
        {
            if (this._client == null)
                return;

            this._client.Disconnect("Client disconnecting");
            this._client = null;
            this.ConnectionState = ConnectionState.Disconnected;
            this.Logger.Info("Client disconnected.");
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
                        this.Logger.Info($"Client status changed: {status}");
                        if (status == NetConnectionStatus.Disconnected)
                        {
                            this.ConnectionState = ConnectionState.Disconnected;
                        }
                        break;
                    case NetIncomingMessageType.Data:
                        this.Logger.Info($"Received data message of length: {msg.LengthBytes}");
                        // var chatMsg = ChatMessage.Deserialize(msg);
                        // OnChatMessageReceived?.Invoke(chatMsg);
                        break;
                    default:
                        this.Logger.Info($"Unhandled message type: {msg.MessageType}");
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
                this.Logger.Info("Cannot send chat message, client not connected.");
                return;
            }
            
            var outgoingMsg = this._client.CreateMessage(text);
            this._client.SendMessage(outgoingMsg, NetDeliveryMethod.ReliableOrdered);
            this._client.FlushSendQueue();
            this.Logger.Info($"Sent chat message: {text}");
        }
    }
}