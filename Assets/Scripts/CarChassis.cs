using UnityEngine;

namespace Racing
{
    public class CarChassis : MonoBehaviour
    {
        [SerializeField] private WheelAxle[] WheelAxles;

        [SerializeField] private float wheelWidth;
        [SerializeField] private float wheelBaseLength;

        //DEBUG
        public float MotorTorque;
        public float SteerAngle;
        public float BrakeTorque;

        private void FixedUpdate()
        {
            UpdateWheelAxes();
        }

        private void UpdateWheelAxes()
        {
            for (int i = 0; i < WheelAxles.Length; i++)
            {
                WheelAxles[i].Update();

                WheelAxles[i].ApplyMotorTorque(MotorTorque);
                WheelAxles[i].ApplySteerAngle(SteerAngle, wheelWidth, wheelBaseLength);
                WheelAxles[i].ApplyBrakeTorque(BrakeTorque);
            }
        }
    }
}