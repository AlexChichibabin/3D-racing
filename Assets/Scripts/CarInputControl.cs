using UnityEngine;

namespace Racing
{
    public class CarInputControl : MonoBehaviour
    {
        [SerializeField] private Car car;
        [SerializeField] private AnimationCurve brakeCurve;
        [SerializeField] private AnimationCurve steerCurve;
        [SerializeField][Range(0.0f, 1.0f)] private float autoBrakeStrength = 0.5f;


        private float wheelSpeed;
        private float verticalAxis;
        private float horizontalAxis;
        private float handBrakeAxis;



        private void Update()
        {
            wheelSpeed = car.WheelSpeed;
            UpdateAxis();

            if (handBrakeAxis == 0)
            {
                UpdateThrottle();
                UpdateAutoBrake();
            }
            car.HandBrakeControl = handBrakeAxis;
            
            UpdateSteer();

            //Debug.Log($"{car.SteerControl}, {(int)wheelSpeed}, {car.WheelSpeed / car.MaxSpeed}");

        }
        private void UpdateThrottle()
        {
            if (Mathf.Sign(-verticalAxis) == Mathf.Sign(wheelSpeed) || Mathf.Abs(wheelSpeed) < 0.5f)
            {
                car.ThrottleControl = verticalAxis;
                car.BrakeControl = 0;
            }
            else if (verticalAxis != 0)
            {
                car.ThrottleControl = 0;
                car.BrakeControl = brakeCurve.Evaluate(Mathf.Abs(wheelSpeed) / car.MaxSpeed);
            }
        }
        private void UpdateSteer()
        {
            car.SteerControl = steerCurve.Evaluate(-car.WheelSpeed / car.MaxSpeed) * horizontalAxis;
        }
        private void UpdateAutoBrake()
        {
            if (verticalAxis == 0)
            {
                car.BrakeControl = brakeCurve.Evaluate(Mathf.Abs(car.WheelSpeed) / car.MaxSpeed) * autoBrakeStrength;
            }
        }private void UpdateAxis()
        {
            verticalAxis = Input.GetAxis("Vertical");
            horizontalAxis = Input.GetAxis("Horizontal");
            handBrakeAxis = Input.GetAxis("Jump");
        }
    }
}