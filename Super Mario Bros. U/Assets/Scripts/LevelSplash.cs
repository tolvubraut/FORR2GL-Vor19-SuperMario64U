using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSplash : MonoBehaviour
{
    public Text worldText;
    public Text livesText;
    private Canvas canvas;

    void Start()
    {
        canvas = GetComponent<Canvas>();

        // Ná í fjölda lífa og setja bil fyrir framan ef færri en 10
        string livesString = VarManager.lives.ToString();
        livesString = livesString.PadLeft(2);

        // Skrifa réttan texta
        worldText.text = $"WORLD {VarManager.worldName}";
        livesText.text = $"× {livesString}";

        // Hlaða inn borði eftir 4 (3.5 + 0.5) sekúndur
        StartCoroutine("WaitThenLoadWorld");
    }

    IEnumerator WaitThenLoadWorld()
    {
        yield return new WaitForSeconds(3.5f);
        canvas.enabled = false;  // Hætta að sýna canvas til að fá svartan skjá í smástund
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene($"World{VarManager.worldName}");
    }
}
