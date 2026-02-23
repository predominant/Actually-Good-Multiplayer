using Lidgren.Network;

namespace AGM.Packets
{
    public class LocalPlayerPacket : Packet
    {
        public string Id { get; set; }
        public string Name { get; set; }
        
        public override void PacketToNetOutgoingMessage(NetOutgoingMessage msg)
        {
            msg.Write((byte)PacketType.LocalPlayerPacket);
            this.WriteData(msg);
        }

        public override void NetIncomingMessageToPacket(NetIncomingMessage msg)
        {
            this.ReadData(msg);
        }

        public override void WriteData(NetOutgoingMessage msg)
        {
            msg.Write(this.Id);
            msg.Write(this.Name);
        }

        public override void ReadData(NetIncomingMessage msg)
        {
            this.Id = msg.ReadString();
            this.Name = msg.ReadString();
        }
    }
}