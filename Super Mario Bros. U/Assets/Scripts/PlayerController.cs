using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isFrozen = false;
    public float speed;
    public float jumpSpeed;
    public LayerMask jumpLayers;
    public CameraController cameraController;
    private Rigidbody2D rb2d;
    private Animator animator;
    private bool isFacingLeft = false;
    private bool isFarLeft = false;
    private PlayerAudio audioManager;
    private PlayerHealth playerHealth;
    private float speedScale;
    private float animSpeedScale;
    private bool walkingTowardsCastle = false;
    private float clearSoundLength;
    private float flagpolePosXFacingLeft;
    private int enemyLayer;


    public void SetFrozen(bool state)
    {
        isFrozen = state;
    }

    // Stilla af hvort leikmaður sé lengst til vinstri (til að stoppa hann af)
    public void SetFarLeft(bool state)
    {
        isFarLeft = state;
    }

    // Klára borð
    public void ClearCourse(float flagpolePosX, float xOffset)
    {
        // Staðsetning leikmanns m.v. fánastöng ef hann snýr til vinstri (ekki hægri)
        flagpolePosXFacingLeft = flagpolePosX + xOffset;
        // Leikmaður á að byrja á að snúa til hægri
        if (isFacingLeft)
        {
            Flip();
        }
        // Grípa í fánastöng
        GrabFlagpole(flagpolePosX - xOffset);
        cameraController.SetStatic(true);  // Festa myndavél áður en leikmaður snýr sér
        // Bíða síðan og klára borð
        StartCoroutine("WaitThenClearCourse");
    }

    void MovePlayerXTo(float newPosX)
    {
        Vector3 playerPos = transform.position;
        playerPos.x = newPosX;
        transform.position = playerPos;
    }

    // Grípa í fánastöng
    void GrabFlagpole(float flagpolePosX)
    {
        // Stilla animator rétt
        animator.SetBool("Jump", false);
        animator.SetBool("On Flagpole", true);
        
        // Færa leikmann á réttan stað
        MovePlayerXTo(flagpolePosX);
        SetFrozen(true);  // Leikmaður má ekki hreyfa sig
        // Spila hljóð
        audioManager.PlayAudio("Grab Flagpole");
    }

    IEnumerator WaitThenClearCourse()
    {
        // Bíða í 2 sekúndur
        yield return new WaitForSeconds(1.5f);
        Flip();  // Snúa leikmanni til vinstri

        // Færa leikmann á réttan stað m.v. hvernig hann snýr
        MovePlayerXTo(flagpolePosXFacingLeft);

        // Bíða í 0.5 sekúndur
        yield return new WaitForSeconds(0.5f);
        Flip();  // Snúa leikmanni aftur til hægri
        animator.SetBool("On Flagpole", false);  // Láta leikmann sleppa fánastöng

        cameraController.SetStatic(false);  // Losa myndavél þegar leikmaður er að fara af fánastöng

        // Spila clear hljóð og ganga í átt að kastala
        clearSoundLength = audioManager.PlayAudio("Clear");  // Geyma lengd í clearSoundLength til að vita hvenær á að fara í næsta borð
        walkingTowardsCastle = true;
        
        // Bíða þar til clear hljóð er búið + 1 sek og fara í næsta borð
        yield return new WaitForSeconds(clearSoundLength + 1f);
        
        // Fara í næsta borð
        VarManager.GoToNextLevel();
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = this.transform.parent.gameObject.GetComponent<Animator>();  // Animator er á foreldri svo dauði geti haft "relative position"
        audioManager = GetComponent<PlayerAudio>();
        enemyLayer = LayerMask.NameToLayer("Enemy");
        playerHealth = GetComponent<PlayerHealth>();
    }

    // Ef leikmaður snýr til hægri, snúa honum til vinstri og öfugt
    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x = -currentScale.x;
        transform.localScale = currentScale;
        isFacingLeft = !isFacingLeft;
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 1f, jumpLayers);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        bool isRunning = Input.GetButton("Run");

        // Leikmaður fer 50% hraðar ef hann hleypur
        if (isRunning)
        {
            speedScale = 1.5f;
            animSpeedScale = 2f;  // Animation er keyrt á 2x ef leikmaður hleypur
        }
        else
        {
            speedScale = 1f;
            animSpeedScale = 1f;
        }

        Vector3 movement = new Vector3(moveHorizontal, 0f, 0f);

        // Færa leikmann ef hann er ekki frosinn
        if (!isFrozen)
        {
            // Leikmaður má ekki hreyfa sig til vinstri ef hann er lengst til vinstri
            if (!isFarLeft || moveHorizontal > 0f)
            {
                transform.position += movement * speed * speedScale * Time.fixedDeltaTime;
            }

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
                (moveHorizontal < 0 && !isFacingLeft || moveHorizontal > 0 && isFacingLeft))
            {
                Flip();
            }

            // Gönguhraði fyrir animation
            animator.SetFloat("Speed", Mathf.Abs(moveHorizontal) * animSpeedScale);
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
