using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Racing
{
    public class UIRaceButton : UISelectableButton
    {
        [SerializeField] private RaceInfo raceInfo;
        [SerializeField] private Image icon;
        [SerializeField] private Text title;

        private void Start()
        {
            ApplyProperty(raceInfo);
        }
        public void ApplyProperty(RaceInfo property)
        {
            if (raceInfo == null) return;

            raceInfo = property;

            icon.sprite = raceInfo.Icon;
            title.text = raceInfo.Title;
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            SceneManager.LoadScene(raceInfo.SceneName);
        }
    }
}