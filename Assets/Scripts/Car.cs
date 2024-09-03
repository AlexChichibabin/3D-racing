using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(CarChassis))]
    public class Car : MonoBehaviour
    {      
        [SerializeField] private float maxSteerAngle;
        [SerializeField] private float maxBrakeTorque;

        [SerializeField] private AnimationCurve engineTorqueCurve;
        [SerializeField] private float maxMotorTorque;
        [SerializeField] private float maxSpeed;

        [SerializeField] private float handBrakeFactor = 1.0f;
        public float LinearVelocity => chassis.LinearVelocity;
        public float WheelSpeed => chassis.GetWheelSpeed();
        public float MaxSpeed => maxSpeed;

        private CarChassis chassis;
        public CarChassis Chassis => chassis;

        //DEBUG
        public float ThrottleControl;
        public float SteerControl;
        public float BrakeControl;
        public float HandBrakeControl;

        private void Start()
        {
            chassis = GetComponent<CarChassis>();

            for (int i = 0; i < 2; i++)
            {
                chassis.GetWheelAxle(i).MaxSpeed_ = maxSpeed;
                chassis.GetWheelAxle(i).LinearVelocity_ = LinearVelocity;
            }
        }
        private void Update()
        {
            float engineTorque = engineTorqueCurve.Evaluate( LinearVelocity / maxSpeed ) * maxMotorTorque;

            if (LinearVelocity >= maxSpeed) engineTorque = 0;              

            chassis.MotorTorque = ThrottleControl * -engineTorque;
            chassis.SteerAngle = SteerControl * maxSteerAngle;
            chassis.BrakeTorque = BrakeControl * maxBrakeTorque;
            chassis.HandBrakeTorque = HandBrakeControl * maxBrakeTorque * handBrakeFactor;
        }
    }
}