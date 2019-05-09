using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public AudioSource backgroundAudio;
    private PlayerAudio audioManager;
    private PlayerController playerController;
    private Rigidbody2D rb2d;
    private BoxCollider2D playerCollider;
    private Animator playerAnimator;
    private bool isDying = false;

    void Start()
    {
        audioManager = GetComponent<PlayerAudio>();
        playerController = GetComponent<PlayerController>();
        rb2d = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerAnimator = this.transform.parent.gameObject.GetComponent<Animator>();  // Animator er á foreldri svo dauði geti haft "relative position"
    }

    IEnumerator WaitThenRestartScene()
    {
        yield return new WaitForSeconds(5.0f);
        VarManager.lives--;
        // Ef leikmaður á fleiri en 0 líf eftir, byrja aftur á borði
        if (VarManager.lives > 0)
        {
            VarManager.GoToLevelSplash();
        }
        // Annars er leik lokið
        else
        {
            VarManager.GoToGameOver();
        }
    }

    // Ef óvinur rekst á leikmann
    public void HitByEnemy()
    {
        // Frysta leikmann
        playerController.SetFrozen(true);
        rb2d.bodyType = RigidbodyType2D.Kinematic;
        playerCollider.isTrigger = true;

        // Stilla af animation
        playerAnimator.SetBool("Jump", false);
        playerAnimator.SetTrigger("Death");

        // Deyja
        Die();
    }

    public void Die()
    {
        // Ef leikmaður er ekki þegar að deyja, stoppa tónlist, spila dauðahljóð og endurræsa senu
        if (!isDying)
        {
            playerController.SetFrozen(true);
            isDying = true;
            backgroundAudio.Stop();
            audioManager.PlayAudio("Death");
            StartCoroutine("WaitThenRestartScene");
        }
    }
}
