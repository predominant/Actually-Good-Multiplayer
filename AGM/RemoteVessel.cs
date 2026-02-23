using AGM.NetworkMessages;
using UnityEngine;

namespace AGM.Core
{
    public class RemoteVessel : MonoBehaviour
    {
        public string VesselId { get; private set; }
        public string PlayerId { get; private set; }
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;
        private Vector3 _targetVelocity;
        
        public float InterpolationSpeed = 10f;
        
        public void Initialize(VesselUpdate update)
        {
            this.VesselId = update.VesselId;
            this.PlayerId = update.PlayerId;
            this.UpdateFromNetwork(update);
        }
        
        public void UpdateFromNetwork(VesselUpdate update)
        {
            this._targetPosition = update.Position;
            this._targetRotation = update.Rotation;
            this._targetVelocity = update.Velocity;
        }
        
        void Update()
        {
            // Smooth interpolation to target position/rotation
            this.transform.position = Vector3.Lerp(this.transform.position, this._targetPosition, Time.deltaTime * this.InterpolationSpeed);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this._targetRotation, Time.deltaTime * this.InterpolationSpeed);
        }
    }
}