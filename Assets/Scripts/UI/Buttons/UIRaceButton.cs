using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Racing
{
    public class UIRaceButton : UISelectableButton, IScriptableObjectProperty, IDependency<LevelSequenceController>
    {
        [SerializeField] private RaceInfo raceInfo;
        [SerializeField] private Image icon;
        [SerializeField] private Text title;

        private RaceLevel raceLevel;
        [SerializeField] private LevelSequenceController levelSequenceController;
        public void Construct(LevelSequenceController obj) => levelSequenceController = obj;

        private void Start()
        {
            ApplyProperty(raceInfo);
            raceLevel = GetComponent<RaceLevel>();
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            //Debug.Log(levelSequenceController);
            //SceneManager.LoadScene(raceInfo.SceneName);
            levelSequenceController.StartRace(raceLevel);
        }
        public void ApplyProperty(ScriptableObject property)
        {
            if (property == null) return;
            if (property is RaceInfo == false) return;

            raceInfo = property as RaceInfo;

            icon.sprite = raceInfo.Icon;
            title.text = raceInfo.Title;
        }


    }
}