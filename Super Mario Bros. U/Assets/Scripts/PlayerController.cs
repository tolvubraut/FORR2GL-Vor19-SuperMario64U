using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public LayerMask jumpLayers;
    private Rigidbody2D rb2d;
    private Animator animator;
    private bool facingLeft;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Ef leikmaður snýr til hægri, snúa honum til vinstri og öfugt
    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x = -currentScale.x;
        transform.localScale = currentScale;
        facingLeft = !facingLeft;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0f, 0f);
        
        // Ef leikmaður er færður til vinstri og sneri áður til hægri eða öfugt, snúa honum við
        if (moveHorizontal < 0 && !facingLeft || moveHorizontal > 0 && facingLeft)
        {
            Flip();
        }

        // Færa leikmann
        transform.position += movement * speed * Time.fixedDeltaTime;

        // Hopp
        if (Input.GetButton("Jump") && Physics2D.Raycast(transform.position, Vector2.down, 1f, jumpLayers))
        {
            Vector2 jump = new Vector2(0f, jumpSpeed);
            rb2d.AddForce(jump, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
        }

        // Hraði fyrir animation
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
    }
}
