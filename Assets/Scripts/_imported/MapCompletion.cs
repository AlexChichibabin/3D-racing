using System;
using System.Collections.Generic;
using Racing;
using UnityEngine;

namespace Racing
{
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

        [SerializeField] private SeasonScore[] m_CompletionDataPerSeason;
        private LevelSequenceController levelSequenceController;
        public void Construct(LevelSequenceController obj) => levelSequenceController = obj;
        public int TotalScores { private set; get; }

        private void Awake()
        {
            Saver<SeasonScore[]>.TryLoad(m_FileName, ref m_CompletionDataPerSeason);
            TotalScores = 0;
            foreach (var season in m_CompletionDataPerSeason)
            {
                foreach (var score in season.racesInSeason)
                {
                    TotalScores += score.score;
                }
            }
        }
        public void SaveEpisodeResult(int levelScore)
        {
            foreach (var season in m_CompletionDataPerSeason)
            {
                season.score = 0;
                foreach (var item in season.racesInSeason)
                {   // Сохранение новых очков прохожения
                    if (item.race == levelSequenceController.CurrentRace.Race)
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
        public void SetCompletionInitialize(int SeasonsLength, int RacesLength)
        {
            m_CompletionDataPerSeason = new SeasonScore[SeasonsLength];
            for (int i = 0; i < SeasonsLength; i++)
            {
                m_CompletionDataPerSeason[i] = new SeasonScore();
                m_CompletionDataPerSeason[i].racesInSeason = new SeasonScore.RaceScore[RacesLength];
            }
        }
        public void SetCompletionData(RaceInfo race, int currentSeason, int currentRace)
        {
            m_CompletionDataPerSeason[currentSeason].racesInSeason[currentRace] = new SeasonScore.RaceScore();
            m_CompletionDataPerSeason[currentSeason].racesInSeason[currentRace].race = race;
        }
    }
}