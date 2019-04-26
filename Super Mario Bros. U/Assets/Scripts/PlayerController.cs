using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isFrozen = false;
    public float speed;
    public float jumpSpeed;
    public LayerMask jumpLayers;
    private Rigidbody2D rb2d;
    private Animator animator;
    private bool facingLeft;
    private PlayerAudio audioManager;
    private bool walkingTowardsCastle = false;

    // Ganga í átt að kastala þegar borði er lokið
    public void WalkTowardsCastle()
    {
        // Alltaf snúa til hægri
        if (facingLeft)
        {
            Flip();
        }
        walkingTowardsCastle = true;
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioManager = GetComponent<PlayerAudio>();
    }

    // Ef leikmaður snýr til hægri, snúa honum til vinstri og öfugt
    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x = -currentScale.x;
        transform.localScale = currentScale;
        facingLeft = !facingLeft;
    }

    // TODO: Replace this with something more accurate
    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 1f, jumpLayers);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0f, 0f);

        // Færa leikmann ef hann er ekki frosinn
        if (!isFrozen)
        {
            transform.position += movement * speed * Time.fixedDeltaTime;

            // Athuga hvort leikmaður sé á jörðinni
            if (IsGrounded()) {
                // Ef hann velur að hoppa, setja hopp-animation af stað
                if (Input.GetButton("Jump"))
                {
                    Vector2 jump = new Vector2(0f, jumpSpeed);
                    rb2d.AddForce(jump, ForceMode2D.Impulse);
                    animator.SetBool("Jump", true);
                    // Spila hopphljóð ef ekkert hljóð er í gangi
                    if (!audioManager.IsPlaying())
                    {
                        audioManager.PlayAudio("Jump");
                    }
                }
                // Ef hann er á jörðinni og ekki að hoppa, stoppa hopp-animation
                else
                {
                    animator.SetBool("Jump", false);
                }
            }

            // Ef leikmaður er ekki að hoppa, færir sig til vinstri og sneri áður til hægri (eða öfugt), snúa honum við
            if (!animator.GetBool("Jump") &&
                (moveHorizontal < 0 && !facingLeft || moveHorizontal > 0 && facingLeft))
            {
                Flip();
            }

            // Gönguhraði fyrir animation
            animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        }
        // Ef leikmaður er frosinn á animation-hraði hans að vera 0
        else
        {
            animator.SetFloat("Speed", 0f);
        }
        // Ef leikmaður er á leið að kastala, hreyfa hann sjálfkrafa
        if (walkingTowardsCastle)
        {
            transform.position += Vector3.right * speed * Time.fixedDeltaTime;
            animator.SetFloat("Speed", 1f);
        }
    }
}
