using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Camera camera;
    public float speed;
    public AudioClip squishSound;
    private AudioSource audioSource;
    private Animator animator;
    private Rigidbody2D rb2d;
    private BoxCollider2D enemyCollider;
    private bool goingLeft = false;
    private bool isSquished = false;
    private bool isActive = false;
    private Vector3 movement = Vector3.left;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<BoxCollider2D>();
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

    void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D contactPoint = other.GetContact(0);

        // Ef óvinur rekst á leikmann, athuga á hvaða hlið
        if (other.gameObject.tag == "Player")
        {

            // Ef áreksturinn er að ofan hoppaði leikmaðurinn á óvininn og óvinurinn deyr
            if (contactPoint.normal.y == -1)
            {
                other.gameObject.GetComponent<PlayerController>().BounceOffEnemy();
                Squish();
            }
            // Annars fór óvinurinn á leikmanninn og leikmaðurinn meiðir sig
            else
            {
                other.gameObject.GetComponent<PlayerHealth>().HitByEnemy();
            }
        }
        // Ef óvinur rekst á eitthvað á x-ásnum, snúa við
        else if (contactPoint.normal.y == 0)
        {
            ChangeDirection();
        }
    }

    // Láta óvin hreyfa sig
    void FixedUpdate()
    {
        // Ef myndavél kemur nálægt óvini, virkja hann
        if (!isActive && camera.WorldToScreenPoint(transform.position).x < Screen.width + 100f)  // Virkjast þegar myndavél er innan við 100 einingar frá óvini
        {
            isActive = true;
        }
        // Óvinir hreyfa sig ef þeir eru aktífir og ekki búið að kremja þá
        if (isActive && !isSquished)
        {
            transform.position += movement * speed * Time.fixedDeltaTime;
        }
    }
}
