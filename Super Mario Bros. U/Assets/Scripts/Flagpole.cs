using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flagpole : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public Rigidbody2D flag;
    private PlayerController playerController;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerController = other.gameObject.GetComponent<PlayerController>();

            // Finna xOffset, þ.e. fjarlægð frá x-staðsetningu fánastangar sem leikmaður á að vera á
            float xOffset = GetComponent<SpriteRenderer>().bounds.size.x;
            
            // Stoppa bakgrunnstónlist
            backgroundMusic.Stop();

            // Kveikja á þyngdarkröftum á fána svo hann detti niður
            flag.bodyType = RigidbodyType2D.Dynamic;

            // Setja af stað lok borðs
            playerController.ClearCourse(transform.position.x, xOffset);
        }
    }
}
