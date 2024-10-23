using Racing;
using Unity.VisualScripting;
using UnityEngine;


public interface IDependency<T>
{
    void Construct(T obj);
}

public class SceneDependencies : MonoBehaviour
{
    [SerializeField] private TrackpointCircuit trackpointCircuit;
    [SerializeField] private RaceStateTracker raceStateTracker;
    [SerializeField] private CarInputControl carInputControl;
    [SerializeField] private Car car;
    [SerializeField] private CarCameraController cameraController;
    [SerializeField] private RaceTimeTracker timeTracker;

    private void Bind(MonoBehaviour mono)
    {
        if(mono is IDependency<TrackpointCircuit>) (mono as IDependency<TrackpointCircuit>).Construct(trackpointCircuit);
        if (mono is IDependency<RaceStateTracker>) (mono as IDependency<RaceStateTracker>).Construct(raceStateTracker);
        if (mono is IDependency<CarInputControl>) (mono as IDependency<CarInputControl>).Construct(carInputControl);
        if (mono is IDependency<Car>) (mono as IDependency<Car>).Construct(car);
        if (mono is IDependency<CarCameraController>) (mono as IDependency<CarCameraController>).Construct(cameraController);
        if (mono is IDependency<RaceTimeTracker>) (mono as IDependency<RaceTimeTracker>).Construct(timeTracker);
    }
    private void Awake()
    {
        MonoBehaviour[] monoInScene = FindObjectsOfType<MonoBehaviour>();

        for (int i = 0; i < monoInScene.Length; i++)
        {
            Bind(monoInScene[i]);
        }
    }
}
