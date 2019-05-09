using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    public AudioClip bumpSound;
    public AudioClip itemSound;
    public GameObject itemPrefab;
    private Animator animator;
    private AudioSource audioSource;
    private bool isActive = true;

    void Start()
    {
        animator = this.transform.parent.gameObject.GetComponent<Animator>();  // Animator er á parent svo hægt sé að færa kassann "relatively"
        audioSource = GetComponent<AudioSource>();
    }

    void HitByPlayer()
    {
        audioSource.PlayOneShot(bumpSound);
        // Ef ekki er búið að hoppa undir kassann
        if (isActive)
        {
            animator.SetTrigger("Hit");
            isActive = false;  // Bara hægt að hoppa einu sinni á kassann
            // Búa til hlut upp úr kassa
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
            // Spila hljóð hlutar
            audioSource.PlayOneShot(itemSound);
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
