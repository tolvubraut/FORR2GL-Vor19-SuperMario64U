using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public AudioClip bumpSound;
    private Animator animator;
    private AudioSource audioSource;


    void Start()
    {
        animator = this.transform.parent.gameObject.GetComponent<Animator>();  // Animator er á parent svo hægt sé að færa kassann "relatively"
        audioSource = GetComponent<AudioSource>();
    }

    void HitByPlayer()
    {
        animator.SetTrigger("Hit");
        // Ef ekki er þegar verið að spila "bump" hljóð, spila það
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(bumpSound);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Ef leikmaður rekst á kassann, athuga á hvaða hlið áreksturinn er
        if (other.gameObject.tag == "Player")
        {
            ContactPoint2D contactPoint = other.GetContact(0);
            
            // Ef áreksturinn er að neðan
            if (contactPoint.normal.y == 1)
            {
                HitByPlayer();
            }
        }
    }
}
