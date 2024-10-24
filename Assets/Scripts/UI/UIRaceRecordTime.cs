using System;
using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UIRaceRecordTime : MonoBehaviour, IDependency<RaceResultTime>, IDependency<RaceStateTracker>
    {
        [SerializeField] private GameObject goldRecordObject;
        [SerializeField] private GameObject playerRecordObject;
        [SerializeField] private Text goldRecordTime;
        [SerializeField] private Text playerRecordTime;

        private RaceResultTime resultTime;
        private RaceStateTracker stateTracker;
        public void Construct(RaceResultTime obj) => resultTime = obj;
        public void Construct(RaceStateTracker obj) => stateTracker = obj;

        private void Start()
        {
            stateTracker.Started += OnStarted;
            stateTracker.Completed += OnCompleted;

            goldRecordObject.SetActive(false);
            playerRecordObject.SetActive(false);
        }
        private void OnDestroy()
        {
            stateTracker.Started -= OnStarted;
            stateTracker.Completed -= OnCompleted;
        }
        private void OnStarted()
        {
            if (resultTime.PlayerRecordTime > resultTime.GoldTime || resultTime.RecordWasSet == false)
            {
                goldRecordObject.SetActive(true);
                goldRecordTime.text = StringTime.SecondToTimeString(resultTime.GoldTime);
            }
            if (resultTime.RecordWasSet == true)
            {
                playerRecordObject.SetActive(true);
                playerRecordTime.text = StringTime.SecondToTimeString(resultTime.PlayerRecordTime);
            }

        }
        private void OnCompleted()
        {
            goldRecordObject.SetActive(false);
            playerRecordObject.SetActive(false);
        }
    }
}