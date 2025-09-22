using Lidgren.Network;

namespace AGM
{
    public class Server
    {
        private NetServer _server;
        
        public Server()
        {
            var config = new NetPeerConfiguration("AGM")
            {
                Port = 12345,
                MaximumConnections = 100
            };
            this._server = new NetServer(config);
        }

        public void Start()
        {
            this._server.Start();
        }
        
        public void Stop()
        {
            this._server.Shutdown("Server shutting down");
        }
    }
}