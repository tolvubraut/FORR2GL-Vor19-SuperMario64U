using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Geymir breytur og aðferðir sem þarf að vera hægt að sækja "globally"
public class VarManager : MonoBehaviour
{
    // Klasi fyrir borð
    public class Stage
    {
        private int world;
        private int level;
        public Stage(int worldNo, int levelNo)
        {
            world = worldNo;
            level = levelNo;
        }

        public int GetWorldNo()
        {
            return world;
        }

        public int GetLevelNo()
        {
            return level;
        }

        // Skilar nafni borðs á því formi sem er sýnt í leiknum
        public string GetStageName()
        {
            return $"{world}-{level}";
        }
    }
    // Array með þeim borðum sem eru til
    private static Stage[] stagesAvailable = new Stage[]
    {
        new Stage(1, 1),
        // new Stage(1, 2)
    };
    // Index núverandi borðs
    private static int currentStageIndex = 0;
    // Núverandi borð
    public static Stage currentStage = stagesAvailable[0];
    // Líf sem leikmaður hefur
    public static int lives = 5;
    public static int coins = 0;

    // Ná í pening
    public static void CollectCoin()
    {
        coins++;
        // Ef leikmaður fær 100 peninga fær hann aukalíf
        if (coins > 99)
        {
            lives++;
            coins = 0;
        }
    }

    // Fara í næsta borð
    public static void GoToNextLevel()
    {
        currentStageIndex++;
        if (currentStageIndex >= stagesAvailable.Length)
        {
            GoToWinScreen();
        }
        else
        {
            currentStage = stagesAvailable[currentStageIndex];
            GoToLevelSplash();
        }
    }

    // Hlaða inn level splash
    public static void GoToLevelSplash()
    {
        SceneManager.LoadScene("LevelSplash");
    }

    // Hlaða inn "game over"
    public static void GoToGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public static void GoToWinScreen()
    {
        SceneManager.LoadScene("Win");
    }

    // Núllstilla leik
    public static void ResetAll()
    {
        currentStageIndex = 0;
        currentStage = stagesAvailable[0];
        lives = 5;
        coins = 0;
    }
}
