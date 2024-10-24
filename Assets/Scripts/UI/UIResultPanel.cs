using System;
using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class UIResultPanel : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceResultTime>
    {
        [SerializeField] private Text goldTime;
        [SerializeField] private Text recordTime;
        [SerializeField] private Text currentResultTime;

        private RaceStateTracker raceStateTracker;
        private RaceResultTime raceResultTime;
        public void Construct(RaceStateTracker obj) => raceStateTracker = obj;
        public void Construct(RaceResultTime obj) => raceResultTime = obj;

        private void Start()
        {
            raceStateTracker.Completed += OnCompleted;

            gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            raceStateTracker.Completed -= OnCompleted;
        }
        private void OnCompleted()
        {
            gameObject.SetActive(true);
        }
    }
}