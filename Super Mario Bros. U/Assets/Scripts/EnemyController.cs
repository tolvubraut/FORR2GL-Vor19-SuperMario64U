using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public AudioClip squishSound;
    private AudioSource audioSource;
    private Animator animator;
    private Rigidbody2D rb2d;
    private BoxCollider2D enemyCollider;
    private bool goingLeft = false;
    private bool isSquished = false;
    private Vector3 movement = Vector3.left;
    private int groundLayer;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<BoxCollider2D>();
        groundLayer = LayerMask.NameToLayer("Ground");
    }

    // Ef leikmaður hoppar á óvin
    public void Squish()
    {
        isSquished = true;
        animator.SetBool("Squished", true);
        audioSource.PlayOneShot(squishSound);
        // Þyngdarafl hættir að verka á hann
        rb2d.bodyType = RigidbodyType2D.Kinematic;
        enemyCollider.isTrigger = true;
        // Bíða í 1 sek og eyða svo óvini
        StartCoroutine("WaitThenDie");
    }

    IEnumerator WaitThenDie()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    // Ganga í öfuga átt
    void ChangeDirection()
    {
        goingLeft = !goingLeft;
        movement = -movement;
    }

    // Ef óvinur rekst á eitthvað sem er ekki leikmaður eða jörðin, snúa við
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" ||
            other.gameObject.layer == groundLayer)
        {
            return;   
        }
        ChangeDirection();
    }

    // Láta óvin hreyfa sig
    void FixedUpdate()
    {
        if (!isSquished)
        {
            transform.position += movement * speed * Time.fixedDeltaTime;
        }
    }
}
