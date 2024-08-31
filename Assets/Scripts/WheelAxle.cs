using System;
using UnityEngine;

namespace Racing
{
    [System.Serializable]
    public class WheelAxle
    {
        [SerializeField] private WheelCollider leftWheelCollider;
        [SerializeField] private WheelCollider rightWheelCollider;

        [SerializeField] private Transform leftWheelMesh;
        [SerializeField] private Transform rightWheelMesh;

        [SerializeField] private bool isMotor;
        [SerializeField] private bool isSteer;

        private WheelHit leftWheelHit;
        private WheelHit rightWheelHit;


        //public API
        public void Update()
        {
            UpdateWheelHits();

            ApplyAntiRoll();
            ApplyDownForce();
            CorrectStiffness();

            SyncMeshTransform();

        }

        public void ApplySteerAngle(float steerAngle, float wheelWidth, float wheelBaseLength)
        {
            if (isSteer == false) return;

            //Akkerman angle:
            float radius = Mathf.Abs(wheelBaseLength * Mathf.Tan( Mathf.Deg2Rad * ( 90 - Mathf.Abs(steerAngle) )));
            float angleSign = Mathf.Sign(steerAngle);

            if (angleSign > 0)
            {
                leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(  wheelBaseLength / (radius - (wheelWidth / 2) ) ) * angleSign;
                rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(  wheelBaseLength / (radius + (wheelWidth / 2) ) ) * angleSign;
            }
            else if (angleSign < 0)
            {
                leftWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(  wheelBaseLength / (radius - (wheelWidth / 2) ) ) * angleSign;
                rightWheelCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(  wheelBaseLength / (radius + (wheelWidth / 2) ) ) * angleSign;
            }
            else
            {
                leftWheelCollider.steerAngle = 0;
                rightWheelCollider.steerAngle = 0;
            }
        }
        public void ApplyMotorTorque(float motorTorque)
        {
            if (isMotor == false) return;

            leftWheelCollider.motorTorque = motorTorque;
            rightWheelCollider.motorTorque = motorTorque;
        }
        public void ApplyBrakeTorque(float brakeTorque)
        {
            leftWheelCollider.brakeTorque = brakeTorque;
            rightWheelCollider.brakeTorque = brakeTorque;
        }

        //private
        private void SyncMeshTransform()
        {
            UpdateWheelTransform(leftWheelCollider, leftWheelMesh);
            UpdateWheelTransform(rightWheelCollider, rightWheelMesh);
        }

        private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
        {
            Vector3 position;
            Quaternion rotation;

            wheelCollider.GetWorldPose(out position, out rotation);
            wheelTransform.position = position;
            wheelTransform.rotation = rotation;
        }
        private void UpdateWheelHits()
        {
            leftWheelCollider.GetGroundHit(out leftWheelHit);
            rightWheelCollider.GetGroundHit(out rightWheelHit);
        }
        private void ApplyAntiRoll()
        {
            
        }
        private void ApplyDownForce()
        {
            
        }
        private void CorrectStiffness()
        {
            
        }
    }
}