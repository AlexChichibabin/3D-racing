using UnityEngine;

namespace Racing
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private Car car;
        [SerializeField][Range(0.0f, 1.0f)] private float normalizedSpeedShake;
        [SerializeField] private float shakeAmount;
        private void Update()
        {
            if(car.NormalizedLinearVelocity >= normalizedSpeedShake)
                transform.localPosition += Random.insideUnitSphere * shakeAmount * Time.deltaTime;
        }
    }
}