using Racing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSequenceController : MonoBehaviour
{
    public static string MainMenuSceneNickname = "MainMenu";
    //public static string MapSceneNickname = "LevelMap";

    public RaceLevel CurrentRace { get; private set; }

    //public int CurrentLevel { get; private set; }

    public bool LastLevelResult { get; private set; }

    public void StartRace(RaceLevel race)
    {
        CurrentRace = race;
        //CurrentLevel = 0;
        //if (CurrentRace.Levels.Length > 0 && CurrentRace.Levels[CurrentLevel] != null)
        if (CurrentRace != null)
            SceneManager.LoadScene(CurrentRace.Race.SceneName);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(CurrentRace.Race.SceneName);
    }

    public void FinishCurrentLevel(bool success)
    {
        LastLevelResult = success;

        //ResultPanelController.Instance.ShowResults(LevelStatistics, success);

    }

    /*public void AdvanceLevel()
    {
        if (CurrentRace)
        {
            CurrentLevel++;

            if (CurrentRace.Levels.Length <= CurrentLevel)
            {
                SceneManager.LoadScene(MapSceneNickname);
            }
            else
            {
                SceneManager.LoadScene(CurrentRace.Levels[CurrentLevel]);
            }
        }
        else
        {
            SceneManager.LoadScene(MapSceneNickname);
        }
    }*/
}
