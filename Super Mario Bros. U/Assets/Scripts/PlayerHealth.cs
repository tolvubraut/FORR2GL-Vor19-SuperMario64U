using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public AudioSource backgroundAudio;
    private PlayerAudio audioManager;

    void Start()
    {
        audioManager = GetComponent<PlayerAudio>();
    }

    IEnumerator WaitThenRestartScene()
    {
        yield return new WaitForSeconds(5.0f);
        VarManager.lives--;
        // Ef leikmaður á fleiri en 0 líf eftir, byrja aftur á borði
        if (VarManager.lives > 0)
        {
            SceneManager.LoadScene("LevelSplash");
        }
        // Annars er leik lokið
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void Die()
    {
        backgroundAudio.Stop();
        audioManager.PlayAudio("Death");
        StartCoroutine("WaitThenRestartScene");
    }
}
