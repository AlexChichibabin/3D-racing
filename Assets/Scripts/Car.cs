using UnityEngine;

namespace Racing
{
    [RequireComponent(typeof(CarChassis))]
    public class Car : MonoBehaviour
    {      
        [SerializeField] private float maxSteerAngle;
        [SerializeField] private float maxBrakeTorque;

        [Header ("Engine")]
        [SerializeField] private AnimationCurve engineTorqueCurve;
        [SerializeField] private float engineMaxTorque;
        [SerializeField] private float engineTorque;
        [SerializeField] private float engineRPM;
        [SerializeField] private float engineMinRPM;
        [SerializeField] private float engineMaxRPM;

        [Header("Gearbox")]
        [SerializeField] private float[] gears;
        [SerializeField] private float finalDriveRatio;

        [SerializeField] private float selectedGear;
        [SerializeField] private float rearGear;
        [SerializeField] private int selectedGearIndex;

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

        private void Awake()
        {
            
        }
        private void Start()
        {
            chassis = GetComponent<CarChassis>();

            for (int i = 0; i < 2; i++)
            {
                chassis.GetWheelAxle(i).MaxSpeed_ = maxSpeed;
                //chassis.GetWheelAxle(i).LinearVelocity_ = LinearVelocity;
            }
        }
        private void Update()
        {
            UpdateEngineTorque();

            //float engineTorque = engineTorqueCurve.Evaluate( LinearVelocity / maxSpeed ) * engineMaxTorque; 

            for (int i = 0; i < 2; i++)
            {
                chassis.GetWheelAxle(i).LinearVelocity_ = LinearVelocity;
            }

            if (LinearVelocity >= maxSpeed) engineTorque = 0;              

            chassis.MotorTorque = ThrottleControl * -engineTorque;
            chassis.SteerAngle = SteerControl * maxSteerAngle;
            chassis.BrakeTorque = BrakeControl * maxBrakeTorque;
            chassis.HandBrakeTorque = HandBrakeControl * maxBrakeTorque * handBrakeFactor;
        }

        private void UpdateEngineTorque()
        {
            engineRPM = engineMinRPM + Mathf.Abs(chassis.GetAverageRpm() * selectedGear * finalDriveRatio);
            engineRPM = Mathf.Clamp(engineRPM, engineMinRPM, engineMaxRPM);

            engineTorque = engineTorqueCurve.Evaluate(engineRPM / engineMaxRPM) * engineMaxTorque * finalDriveRatio * Mathf.Sign(selectedGear * gears[0]);
        }

        //GearBox
        private void ShiftGear(int gearIndex)
        {
            gearIndex = Mathf.Clamp(gearIndex, 0, gears.Length - 1);

            selectedGear = gears[gearIndex];
            selectedGearIndex = gearIndex;
        }
        public void UpGear()
        {
            ShiftGear(selectedGearIndex + 1);
        }
        public void DownGear()
        {
            ShiftGear(selectedGearIndex - 1);
        }
    }
}