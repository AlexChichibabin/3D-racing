using UnityEngine;

namespace Racing
{
    public class CarCameraController : MonoBehaviour
    {
        [SerializeField] private Car car;
        [SerializeField] new private Camera camera;
        [SerializeField] private CarCameraShaker cameraShaker;
        [SerializeField] private CarCameraFovCorrector cameraFovCorrector;
        [SerializeField] private CarCameraFollower cameraFollower;
        [SerializeField] private CarCameraPostProcessing cameraPostProcessing;

        private void Awake()
        {
            cameraShaker.SetProperties(car, camera);
            cameraFovCorrector.SetProperties(car, camera);
            cameraFollower.SetProperties(car, camera);
            cameraPostProcessing.SetProperties(car, camera);
        }
    }
}