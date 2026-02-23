using Lidgren.Network;

namespace AGM.Packets
{
    public class PlayerDisconnectPacket : Packet
    {
        public string Player { get; set; }
        
        public override void PacketToNetOutgoingMessage(NetOutgoingMessage msg)
        {
            msg.Write((byte)PacketType.PlayerDisconnectPacket);
            this.WriteData(msg);
        }

        public override void NetIncomingMessageToPacket(NetIncomingMessage msg)
        {
            this.ReadData(msg);
        }

        public override void WriteData(NetOutgoingMessage msg)
        {
            msg.Write(this.Player);
        }

        public override void ReadData(NetIncomingMessage msg)
        {
            this.Player = msg.ReadString();
        }
    }
}