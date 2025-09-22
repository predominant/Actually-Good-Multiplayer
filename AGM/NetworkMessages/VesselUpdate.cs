using System.IO;
using UnityEngine;

namespace AGM.NetworkMessages
{
    public class VesselUpdate
    {
        public string VesselId;
        public string PlayerId;
        public Vector3 Position;
        public Vector3 Velocity;
        public Quaternion Rotation;
        public double UniversalTime;
        public string BodyName;

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(VesselId ?? "");
            writer.Write(PlayerId ?? "");
            writer.Write(Position.x);
            writer.Write(Position.y);
            writer.Write(Position.z);
            writer.Write(Velocity.x);
            writer.Write(Velocity.y);
            writer.Write(Velocity.z);
            writer.Write(Rotation.x);
            writer.Write(Rotation.y);
            writer.Write(Rotation.z);
            writer.Write(Rotation.w);
            writer.Write(UniversalTime);
            writer.Write(BodyName ?? "");
        }

        public static VesselUpdate Deserialize(BinaryReader reader)
        {
            var update = new VesselUpdate();
            update.VesselId = reader.ReadString();
            update.PlayerId = reader.ReadString();
            update.Position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            update.Velocity = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            update.Rotation = new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            update.UniversalTime = reader.ReadDouble();
            update.BodyName = reader.ReadString();
            return update;
        }
    }
}