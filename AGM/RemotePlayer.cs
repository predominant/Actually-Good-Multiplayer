using System.Net;
using System.Net.Sockets;

namespace AGM.Core
{
    public class RemotePlayer
    {
        public IPEndPoint EndPoint { get; set; }
        public TcpClient TcpClient { get; set; }
        public string PlayerId { get; set; }
        public string PlayerName { get; set; }
    }
}