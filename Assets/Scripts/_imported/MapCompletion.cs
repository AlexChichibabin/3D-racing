using System;
using Racing;
using UnityEngine;


public class MapCompletion : MonoBehaviour, IDependency<LevelSequenceController>
{
    public const string m_FileName = "completion.dat";

    [Serializable]
    private class SeasonScore
    {
        [Serializable]
        public class RaceScore
        {
            public RaceInfo race;
            public int score;
        }
        public RaceScore[] racesInSeason;
        public int score;
    }

    //[SerializeField] private GameObject[] m_Seasons;
    [SerializeField] private SeasonScore[] m_CompletionDataPerSeason;
    private LevelSequenceController levelSequenceController;
    public void Construct(LevelSequenceController obj) => levelSequenceController = obj;
    public int TotalScores { private set; get; }

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        Saver<SeasonScore[]>.TryLoad(m_FileName, ref m_CompletionDataPerSeason);
        TotalScores = 0;
        foreach (var score in m_CompletionDataPerSeason)
        {
            TotalScores += score.score;
        }
    }
    public void SaveEpisodeResult(int levelScore)
    {
        foreach (var season in m_CompletionDataPerSeason)
        {
            season.score = 0;
            foreach (var item in season.racesInSeason)
            {   // Сохранение новых очков прохожения
                if (item.race == levelSequenceController.CurrentRace)
                {
                    if (item.score < levelScore)
                    {
                        TotalScores += levelScore - item.score;
                        item.score = levelScore;
                        Saver<SeasonScore[]>.Save(m_FileName, m_CompletionDataPerSeason);
                    }
                    season.score += levelScore;
                }
            }
            print($"Episode complete with score {levelScore}");
        }
            
    }
    public int GetEpisodeScore(RaceInfo m_Race)
    {
        foreach (var season in m_CompletionDataPerSeason)
        {
            foreach (var data in season.racesInSeason)
            {
                if (data.race == m_Race)
                {
                    return data.score;
                }
            }
        }
        return 0;
    }
    public void SetCompletionData(RaceInfo race)
    {

    }
}
