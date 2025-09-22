// using System.Collections.Generic;
// using System.IO;
// using AGM.NetworkMessages;
// using UnityEngine;
//
// namespace AGM.Core
// {
//     public class VesselSynchronizer : MonoBehaviour
//     {
//         private Dictionary<string, RemoteVessel> remoteVessels;
//         private float lastSendTime;
//         private const float SEND_INTERVAL = 0.1f; // 10 Hz
//         
//         void Awake()
//         {
//             this.remoteVessels = new Dictionary<string, RemoteVessel>();
//         }
//         
//         public void SendVesselUpdate(Vessel vessel, NetworkManager netManager)
//         {
//             if (Time.time - lastSendTime < SEND_INTERVAL)
//                 return;
//             lastSendTime = Time.time;
//
//             var update = new VesselUpdate
//             {
//                 VesselId = vessel.id.ToString(),
//                 PlayerId = SystemInfo.deviceUniqueIdentifier,
//                 Position = vessel.GetWorldPos3D(),
//                 Velocity = vessel.obt_velocity,
//                 Rotation = vessel.transform.rotation,
//                 UniversalTime = Planetarium.GetUniversalTime(),
//                 BodyName = vessel.mainBody.bodyName
//             };
//
//             // Serialize VesselUpdate to byte[]
//             byte[] updateBytes;
//             using (var ms = new MemoryStream())
//             using (var writer = new BinaryWriter(ms))
//             {
//                 update.Serialize(writer);
//                 updateBytes = ms.ToArray();
//             }
//
//             var message = new NetworkMessage
//             {
//                 Type = MessageType.VesselUpdate,
//                 Data = updateBytes
//             };
//             netManager.SendMessage(message);
//         }
//         
//         public void ProcessVesselUpdate(VesselUpdate update)
//         {
//             // Don't process our own vessel updates
//             if (update.PlayerId == SystemInfo.deviceUniqueIdentifier)
//                 return;
//                 
//             if (!remoteVessels.ContainsKey(update.VesselId))
//             {
//                 this.CreateRemoteVessel(update);
//             }
//             else
//             {
//                 this.UpdateRemoteVessel(update);
//             }
//         }
//         
//         private void CreateRemoteVessel(VesselUpdate update)
//         {
//             // Create a simple representation of remote vessel
//             var go = new GameObject($"RemoteVessel_{update.VesselId}");
//             var remoteVessel = go.AddComponent<RemoteVessel>();
//             remoteVessel.Initialize(update);
//             
//             this.remoteVessels[update.VesselId] = remoteVessel;
//         }
//         
//         private void UpdateRemoteVessel(VesselUpdate update)
//         {
//             this.remoteVessels[update.VesselId].UpdateFromNetwork(update);
//         }
//     }
//     
// }