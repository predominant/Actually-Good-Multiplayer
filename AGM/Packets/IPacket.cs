using Lidgren.Network;

namespace AGM.Packets
{
    public interface IPacket
    {
        void PacketToNetOutgoingMessage(NetOutgoingMessage msg);
        void NetIncomingMessageToPacket(NetIncomingMessage msg);
        void WriteData(NetOutgoingMessage msg);
        void ReadData(NetIncomingMessage msg);
    }
}