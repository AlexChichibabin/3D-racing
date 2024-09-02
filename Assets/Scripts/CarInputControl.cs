using UnityEngine;

namespace Racing
{
    public class CarInputControl : MonoBehaviour
    {
        [SerializeField] private Car car;
        [SerializeField] private AnimationCurve brakeCurve;
        [SerializeField][Range(0.0f, 1.0f)] private float autoBrakeStrength = 0.5f;

        private void Update()
        {
            float verticalAxis = Input.GetAxis("Vertical");
            if (Input.GetAxis("Jump") == 0)
            {
                if (Mathf.Sign(-verticalAxis) == Mathf.Sign(car.WheelSpeed) || Mathf.Abs(car.WheelSpeed) < 0.5f)
                {
                    car.ThrottleControl = verticalAxis;
                    car.BrakeControl = 0;
                }
                else if (verticalAxis != 0)
                {
                    car.ThrottleControl = 0;
                    car.BrakeControl = brakeCurve.Evaluate(Mathf.Abs(car.WheelSpeed) / car.MaxSpeed);
                }
            }
            else car.BrakeControl = Input.GetAxis("Jump");
            

            car.SteerControl = Input.GetAxis("Horizontal");
            UpdateAutoBrake();
            Debug.Log($"{-verticalAxis}, {(int)car.WheelSpeed}, {Mathf.Abs(car.WheelSpeed / car.MaxSpeed)}");

        }
        private void UpdateAutoBrake()
        {
            if (Input.GetAxis("Vertical") == 0)
            {
                car.BrakeControl = brakeCurve.Evaluate(Mathf.Abs(car.WheelSpeed) / car.MaxSpeed) * autoBrakeStrength;
            }
        }
    }
}