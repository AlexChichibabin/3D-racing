using UnityEngine;
using UnityEngine.UI;

namespace Racing
{
    public class RaceLevel : MonoBehaviour, IDependency<MapCompletion>, IScriptableObjectProperty
    {
        private MapCompletion mapCompletion;
        private RaceInfo m_Race;
        [SerializeField] private Text m_NameText;
        private int m_StarsAmount;
        public void Construct(MapCompletion obj) => mapCompletion = obj;
        //[SerializeField] private LevelVisualScores m_VisualScores;
        //[SerializeField] private MapSceneAnimation m_MapAnimation;
        public bool IsComplete { get { return gameObject.activeSelf && m_StarsAmount > 0; } }
        private void Awake()
        {
            //m_VisualScores = GetComponentInChildren<LevelVisualScores>();
        }
        private void Start()
        {
            ApplyProperty(m_Race);
        }
        public void AnimationBeforeLoadLevel()
        {
            //m_MapAnimation.AnimationOnLoad(m_Episode);
        }
        public void Initialize()
        {
            m_StarsAmount = mapCompletion.GetEpisodeScore(m_Race);
            //m_VisualScores.SetStars(m_StarsAmount); TODO
        }
        public int GetStarsAmount() => m_StarsAmount; // Не нужен ли return?
        public void ApplyProperty(ScriptableObject property)
        {
            if (property == null) return;
            if (property is RaceInfo == false) return;

            m_Race = property as RaceInfo;
            m_NameText.text = m_Race.Title;
        }
    }
}