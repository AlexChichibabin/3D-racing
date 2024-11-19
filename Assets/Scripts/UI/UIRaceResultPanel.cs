using System;
using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UIRaceResultPanel : MonoBehaviour, IDependency<RaceResultTime>, IDependency<RaceTimeTracker>
    {
        [SerializeField] private Text goldTime;
        [SerializeField] private Text recordTime;
        [SerializeField] private Text currentResultTime;

        private RaceTimeTracker raceTimeTracker;
        private RaceResultTime raceResultTime;
        public void Construct(RaceTimeTracker obj) => raceTimeTracker = obj;
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
            UpdateResultsOnPanel();

            gameObject.SetActive(true);
        }
        private void UpdateResultsOnPanel()
        {
            goldTime.text = StringTime.SecondToTimeString(raceResultTime.GoldTime);
            recordTime.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);
            currentResultTime.text = StringTime.SecondToTimeString(raceResultTime.CurrentTime);
        }
    }
}