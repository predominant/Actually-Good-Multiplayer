using System.IO;

namespace AGM.NetworkMessages
{
    public class ChatMessage
    {
        public string PlayerId;
        public string PlayerName;
        public string Message;
        public long Timestamp;

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(PlayerId ?? "");
            writer.Write(PlayerName ?? "");
            writer.Write(Message ?? "");
            writer.Write(Timestamp);
        }

        public static ChatMessage Deserialize(BinaryReader reader)
        {
            var msg = new ChatMessage();
            msg.PlayerId = reader.ReadString();
            msg.PlayerName = reader.ReadString();
            msg.Message = reader.ReadString();
            msg.Timestamp = reader.ReadInt64();
            return msg;
        }
    }
}