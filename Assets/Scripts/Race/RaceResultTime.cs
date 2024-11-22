using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Racing
{
    public class RaceResultTime : MonoBehaviour, IDependency<RaceTimeTracker>, IDependency<RaceStateTracker>, IDependency<RaceLevelController>
    {
        public const string SaveMark = "_player_best_time";

        public event UnityAction ResultUpdated;

        private float playerRecordTime;
        private float commonRecordTime;
        private float currentTime;

        public float PlayerRecordTime => playerRecordTime;
        /*public float GoldTime => goldTime;
        public float SilverTime => silverTime;
        public float BronzeTime => bronzeTime;*/
        public float CurrentTime => currentTime;
        public bool RecordWasSet => playerRecordTime != 0;

        private RaceTimeTracker timeTracker;
        private RaceStateTracker stateTracker;
        private RaceLevelController raceLevelController;
        public void Construct(RaceTimeTracker obj) => timeTracker = obj;
        public void Construct(RaceStateTracker obj) => stateTracker = obj;
        public void Construct(RaceLevelController obj) => raceLevelController = obj;

        private void Start()
        {
            stateTracker.Completed += OnCompleted;
            Load();
        }
        private void OnDestroy()
        {
            stateTracker.Completed -= OnCompleted;
        }
        private void OnCompleted()
        {
            currentTime = timeTracker.CurrentTime;
            float absoluteRecord = raceLevelController.GetAbsoluteRecord();

            if (currentTime < playerRecordTime || playerRecordTime == 0) playerRecordTime = currentTime;
            if (currentTime < absoluteRecord || playerRecordTime == 0) commonRecordTime = currentTime;

            Save();

            ResultUpdated?.Invoke();
        }
        private void Load()                   // Use Saver
        {
            playerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveMark, 0);
        }
        private void Save()
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveMark, playerRecordTime);
        }
    }
}