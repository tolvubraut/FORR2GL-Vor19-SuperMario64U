using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public static int lives = 5;
    public AudioSource backgroundAudio;
    private PlayerAudio audioManager;

    void Start()
    {
        audioManager = GetComponent<PlayerAudio>();
    }

    IEnumerator WaitThenRestartScene()
    {
        yield return new WaitForSeconds(5.0f);
        lives--;
        // Ef leikmaður á fleiri en 0 líf eftir, byrja aftur á borði
        if (lives > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
