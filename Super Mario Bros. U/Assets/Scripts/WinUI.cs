using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    public void RestartGame()
    {
        VarManager.ResetAll();
        VarManager.GoToLevelSplash();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
