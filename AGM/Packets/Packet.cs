using Lidgren.Network;

namespace AGM.Packets
{
    public abstract class Packet : IPacket
    {
        public abstract void PacketToNetOutgoingMessage(NetOutgoingMessage msg);
        public abstract void NetIncomingMessageToPacket(NetIncomingMessage msg);
        public abstract void WriteData(NetOutgoingMessage msg);
        public abstract void ReadData(NetIncomingMessage msg);
    }
}