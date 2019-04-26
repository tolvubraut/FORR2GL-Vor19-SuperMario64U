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
            Vector3 playerPos = other.gameObject.transform.position;
            playerPos.x = transform.position.x;
            other.gameObject.transform.position = playerPos;

            backgroundMusic.Stop();

            playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.isFrozen = true;

            Animator playerAnimator = other.gameObject.GetComponent<Animator>();
            playerAnimator.SetBool("Jump", false);
            playerAnimator.SetBool("On Flagpole", true);

            audioSource.PlayOneShot(flagpoleSound);

            StartCoroutine("WaitThenClearCourse");
        }
    }

    IEnumerator WaitThenClearCourse()
    {
        yield return new WaitForSeconds(2.0f);
        audioSource.PlayOneShot(clearSound);
        playerController.WalkTowardsCastle();
        yield return new WaitForSeconds(clearSoundLength + 1f);
        SceneManager.LoadScene("LevelSplash");
    }
}
