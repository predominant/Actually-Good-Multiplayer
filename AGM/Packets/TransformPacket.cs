using Lidgren.Network;

namespace AGM.Packets
{
    public class TransformPacket : Packet
    {
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public float RotationX { get; set; }
        public float RotationY { get; set; }
        public float RotationZ { get; set; }
        public float RotationW { get; set; }
        public string Player { get; set; }
        
        public override void PacketToNetOutgoingMessage(NetOutgoingMessage msg)
        {
            msg.Write((byte)PacketType.TransformPacket);
            this.WriteData(msg);
        }

        public override void NetIncomingMessageToPacket(NetIncomingMessage msg)
        {
            this.ReadData(msg);
        }

        public override void WriteData(NetOutgoingMessage msg)
        {
            msg.Write(this.PositionX);
            msg.Write(this.PositionY);
            msg.Write(this.PositionZ);
            msg.Write(this.RotationX);
            msg.Write(this.RotationY);
            msg.Write(this.RotationZ);
            msg.Write(this.RotationW);
            msg.Write(this.Player);
        }

        public override void ReadData(NetIncomingMessage msg)
        {
            this.PositionX = msg.ReadFloat();
            this.PositionY = msg.ReadFloat();
            this.PositionZ = msg.ReadFloat();
            this.RotationX = msg.ReadFloat();
            this.RotationY = msg.ReadFloat();
            this.RotationZ = msg.ReadFloat();
            this.RotationW = msg.ReadFloat();
            this.Player = msg.ReadString();
        }
    }
}