using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSplash : MonoBehaviour
{
    public Text worldText;
    public Text livesText;
    public VarManager varManager;

    void Start()
    {
        // Ná í fjölda lífa og setja bil fyrir framan ef færri en 10
        string livesString = VarManager.lives.ToString();
        livesString = livesString.PadLeft(2);

        // Skrifa réttan texta
        worldText.text = $"WORLD {VarManager.worldName}";
        livesText.text = $"× {livesString}";

        // Hlaða inn borði eftir 3 sekúndur
        StartCoroutine("WaitThenLoadWorld");
    }

    IEnumerator WaitThenLoadWorld()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene($"World{VarManager.worldName}");
    }
}
