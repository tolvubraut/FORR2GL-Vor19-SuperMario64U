using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    // Byrja leik
    public void StartGame()
    {
        VarManager.GoToLevelSplash();
    }
    // Endurræsa leik (núllstilla allt og byrja)
    public void RestartGame()
    {
        VarManager.ResetAll();
        StartGame();
    }
    // Loka leik
    public void QuitGame()
    {
        Application.Quit();
    }
}
