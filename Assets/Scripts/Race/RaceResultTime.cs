using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Racing
{
    public class RaceResultTime : MonoBehaviour, IDependency<RaceTimeTracker>, IDependency<RaceStateTracker>
    {
        public const string SaveMark = "_player_best_time";

        public event UnityAction ResultUpdated;

        [SerializeField] private float goldTime;

        private float playerRecordTime;
        private float currentTime;

        public float PlayerRecordTime => playerRecordTime;
        public float GoldTime => goldTime;
        public float CurrentTime => currentTime;
        public bool RecordWasSet => playerRecordTime != 0;

        private RaceTimeTracker timeTracker;
        private RaceStateTracker stateTracker;
        public void Construct(RaceTimeTracker obj) => timeTracker = obj;
        public void Construct(RaceStateTracker obj) => stateTracker = obj;

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

            float absoluteRecord = GetAbsoluteRecord();
            
            if (currentTime < absoluteRecord || playerRecordTime == 0)
            {
                playerRecordTime = currentTime;
                Save();
            }
            ResultUpdated?.Invoke();
        }
        public float GetAbsoluteRecord()
        {
            if(playerRecordTime < goldTime && playerRecordTime != 0)
                return playerRecordTime;
            else 
                return goldTime;
        }
        private void Load()
        {
            playerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveMark, 0);
        }
        private void Save()
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveMark, playerRecordTime);
        }
    }
}