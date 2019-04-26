using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flagpole : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public Rigidbody2D flag;
    public AudioClip flagpoleSound;
    public AudioClip clearSound;
    private AudioSource audioSource;
    private float clearSoundLength;
    private PlayerController playerController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        clearSoundLength = clearSound.length;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerController = other.gameObject.GetComponent<PlayerController>();
            Animator playerAnimator = other.gameObject.GetComponent<Animator>();

            // Láta leikmann vera á sömu x-staðsetningu og fánastöngin
            Vector3 playerPos = other.gameObject.transform.position;
            playerPos.x = transform.position.x;
            other.gameObject.transform.position = playerPos;

            // Stoppa bakgrunnstónlist
            backgroundMusic.Stop();

            // Frysta leikmann
            playerController.isFrozen = true;

            // Stilla breytur f. animator
            playerAnimator.SetBool("Jump", false);
            playerAnimator.SetBool("On Flagpole", true);

            // Spila hljóð þegar leikmaður fer á fánastöng
            audioSource.PlayOneShot(flagpoleSound);

            StartCoroutine("WaitThenClearCourse");
        }
    }

    IEnumerator WaitThenClearCourse()
    {
        // Bíða í 2 sekúndur
        yield return new WaitForSeconds(2.0f);
        // Spila clear hljóð og ganga í átt að kastala
        audioSource.PlayOneShot(clearSound);
        playerController.WalkTowardsCastle();
        // Bíða þar til clear hljóð er búið + 1 sek og fara í næsta borð
        yield return new WaitForSeconds(clearSoundLength + 1f);
        // Velja næsta borð
        VarManager.SetNextLevel();
        SceneManager.LoadScene("LevelSplash");
    }
}
