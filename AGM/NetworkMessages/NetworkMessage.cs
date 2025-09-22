using System.IO;

namespace AGM.NetworkMessages
{
    public class NetworkMessage
    {
        public MessageType Type;
        public byte[] Data;

        public void Serialize(BinaryWriter writer)
        {
            writer.Write((byte)Type);
            writer.Write(Data.Length);
            writer.Write(Data);
        }

        public static NetworkMessage Deserialize(BinaryReader reader)
        {
            var type = (MessageType)reader.ReadByte();
            var length = reader.ReadInt32();
            var data = reader.ReadBytes(length);
            return new NetworkMessage { Type = type, Data = data };
        }
    }

}