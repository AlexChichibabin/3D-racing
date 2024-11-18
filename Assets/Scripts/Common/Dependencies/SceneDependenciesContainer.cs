using UnityEngine;

namespace Racing
{
    public class SceneDependenciesContainer : Dependency
    {
        [SerializeField] private TrackpointCircuit trackpointCircuit;
        [SerializeField] private RaceStateTracker raceStateTracker;
        [SerializeField] private CarInputControl carInputControl;
        [SerializeField] private Car car;
        [SerializeField] private CarCameraController cameraController;
        [SerializeField] private RaceTimeTracker timeTracker;
        [SerializeField] private RaceResultTime resultTime;



        protected override void BindAll(MonoBehaviour monoBehaviourInScene)
        {
            Bind<TrackpointCircuit>(trackpointCircuit, monoBehaviourInScene);
            Bind<RaceStateTracker>(raceStateTracker, monoBehaviourInScene);
            Bind<CarInputControl>(carInputControl, monoBehaviourInScene);
            Bind<Car>(car, monoBehaviourInScene);
            Bind<CarCameraController>(cameraController, monoBehaviourInScene);
            Bind<RaceTimeTracker>(timeTracker, monoBehaviourInScene);
            Bind<RaceResultTime>(resultTime, monoBehaviourInScene);
        }
        private void Awake()
        {
            FindAllObjectsToBind();
        }
    }
}