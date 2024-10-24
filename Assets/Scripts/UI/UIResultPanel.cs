using System;
using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UIResultPanel : MonoBehaviour, /*IDependency<RaceStateTracker>,*/ IDependency<RaceResultTime>, IDependency<RaceTimeTracker>
    {
        [SerializeField] private Text goldTime;
        [SerializeField] private Text recordTime;
        [SerializeField] private Text currentResultTime;

        private RaceTimeTracker raceTimeTracker;
        //private RaceStateTracker raceStateTracker;
        private RaceResultTime raceResultTime;
        public void Construct(RaceTimeTracker obj) => raceTimeTracker = obj;
        //public void Construct(RaceStateTracker obj) => raceStateTracker = obj;
        public void Construct(RaceResultTime obj) => raceResultTime = obj;

        private void Start()
        {
            raceResultTime.ResultUpdated += OnResultUpdated;

            gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            raceResultTime.ResultUpdated -= OnResultUpdated;
        }
        private void OnResultUpdated()
        {
            goldTime.text = StringTime.SecondToTimeString(raceResultTime.GoldTime);
            recordTime.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);
            currentResultTime.text = StringTime.SecondToTimeString(raceResultTime.CurrentTime);
            gameObject.SetActive(true);
        }
    }
}