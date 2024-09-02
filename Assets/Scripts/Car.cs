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
        public float LinearVelocity => chassis.LinearVelocity;
        public float WheelSpeed => chassis.GetWheelSpeed();
        public float MaxSpeed => maxSpeed;

        private CarChassis chassis;

        //DEBUG
        public float ThrottleControl;
        public float SteerControl;
        public float BrakeControl;
        //public float HandBrakeControl;

        private void Start()
        {
            chassis = GetComponent<CarChassis>();
        }
        private void Update()
        {
            float engineTorque = engineTorqueCurve.Evaluate( LinearVelocity / maxSpeed ) * maxMotorTorque;

            if (LinearVelocity >= maxSpeed) engineTorque = 0;
            

            chassis.MotorTorque = ThrottleControl * -engineTorque;
            chassis.SteerAngle = SteerControl * maxSteerAngle;
            chassis.BrakeTorque = BrakeControl * maxBrakeTorque;
            //chassis. = HandBrakeControl;
        }
    }
}