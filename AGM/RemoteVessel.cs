using AGM.NetworkMessages;
using UnityEngine;

namespace AGM.Core
{
    public class RemoteVessel : MonoBehaviour
    {
        public string VesselId { get; private set; }
        public string PlayerId { get; private set; }
        private UnityEngine.Vector3 _targetPosition;
        private UnityEngine.Quaternion _targetRotation;
        private UnityEngine.Vector3 _targetVelocity;
        
        public void Initialize(VesselUpdate update)
        {
            VesselId = update.VesselId;
            PlayerId = update.PlayerId;
            UpdateFromNetwork(update);
        }
        
        public void UpdateFromNetwork(VesselUpdate update)
        {
            _targetPosition = update.Position;
            _targetRotation = update.Rotation;
            _targetVelocity = update.Velocity;
        }
        
        void Update()
        {
            // Smooth interpolation to target position/rotation
            transform.position = UnityEngine.Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * 10f);
            transform.rotation = UnityEngine.Quaternion.Lerp(transform.rotation, _targetRotation, Time.deltaTime * 10f);
        }
    }
}