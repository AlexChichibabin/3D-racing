using System;
using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    public enum RaceState
    {
        Preparation,
        CountDown,
        Race,
        Passed
    }

    public class RaceStateTracker : MonoBehaviour
    {
        public event UnityAction PreparatiionStarted;
        public event UnityAction Started;
        public event UnityAction Completed;
        public event UnityAction<TrackPoint> TrackPointPassed;
        public event UnityAction<int> LapCompleted;

        [SerializeField] private TrackpointCircuit trackpointCircuit;
        [SerializeField] private int lapToComplete;

        private RaceState state;
        public RaceState State => state;

        private void Start()
        {
            StartState(RaceState.Preparation);

            trackpointCircuit.TrackPointTriggered += OnTrackPointTriggered;
            trackpointCircuit.LapCompleted += OnLapCompleted;
        }
        private void OnDestroy()
        {
            trackpointCircuit.TrackPointTriggered -= OnTrackPointTriggered;
            trackpointCircuit.LapCompleted += OnLapCompleted;
        }
        private void StartState(RaceState state)
        {
            this.state = state;
        }
        private void OnTrackPointTriggered(TrackPoint tracks)
        {
            TrackPointPassed?.Invoke(tracks);
        }
        private void OnLapCompleted(int lapAmount)
        {
            if (trackpointCircuit.Type == TrackType.Sprint)
            {
                Completed?.Invoke();
            }
            if (trackpointCircuit.Type == TrackType.Circular)
            {
                if (lapAmount == lapToComplete) Completed?.Invoke();
                else CompleteLap(lapAmount);
            }
        }
        public void LaunchPreparationStart()
        {
            if (state != RaceState.Preparation) return;
            PreparatiionStarted?.Invoke();
        }
        private void StartRace()
        {
            if (state != RaceState.CountDown) return;
            StartState(RaceState.Race);
            Started?.Invoke();
        }
        private void CompleteRace()
        {
            if (state != RaceState.Race) return;
            StartState(RaceState.Passed);
            Completed.Invoke();
        }
        private void CompleteLap(int lapAmount)
        {
            LapCompleted?.Invoke(lapAmount);
        }
    }
}