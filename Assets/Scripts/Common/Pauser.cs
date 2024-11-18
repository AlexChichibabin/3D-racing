using UnityEngine;
using UnityEngine.Events;

namespace Racing
{
    public class Pauser : MonoBehaviour
    {
        public event UnityAction<bool> pauseStateChange;

        private bool isPause;
        public bool IsPause => isPause;

        public void ChangePauseState()
        {
            if (isPause == true) UnPause();
            else Pause();
        }
        public void Pause()
        {
            if (isPause == true) return;

            Time.timeScale = 0.0f;
            isPause = true;
            pauseStateChange?.Invoke(isPause);
        }
        public void UnPause()
        {
            if (isPause == false) return;

            Time.timeScale = 1.0f;
            isPause = false;
            pauseStateChange?.Invoke(isPause);
        }
    }
}