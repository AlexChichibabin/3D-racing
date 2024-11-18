using System;
using UnityEngine;

namespace Racing
{
    public class UIPausePanel : MonoBehaviour, IDependency<Pauser>
    {
        [SerializeField] private GameObject pausePanel;

        private Pauser pauser;
        public void Construct(Pauser obj) => pauser = obj;
        private void Start()
        {
            pausePanel.SetActive(false);
            pauser.pauseStateChange += OnPauseStateChange;
        }
        private void OnDestroy()
        {
            pauser.pauseStateChange -= OnPauseStateChange;
        }
        private void OnPauseStateChange(bool isPause)
        {
            pausePanel.SetActive(isPause);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauser.ChangePauseState();
            }
        }
    }
}