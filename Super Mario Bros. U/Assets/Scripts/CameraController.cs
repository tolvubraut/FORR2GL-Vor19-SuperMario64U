using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Transform playerTransform;
    private PlayerController playerController;
    private SpriteRenderer playerSpriteRenderer;
    public Transform levelEnd;
    private Camera camera;
    private Vector3 offset;
    private bool isStatic = false;
    private float smoothTime = 10f;  // Tími sem myndavél má taka til að færa sig ef hún er eftir á (ef hún var stöðug)
    private float smoothStartTime = -1f;  // Upphafsstillingin er -1 til að hægt sé að vita hvort hafi verið skrifað yfir hana
    private float smoothTimeComplete;

    public void SetStatic(bool state)
    {
        isStatic = state;
    }

    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
        playerController = player.GetComponent<PlayerController>();
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        camera = GetComponent<Camera>();
        offset = transform.position - player.transform.position;  // Fjarlægð frá leikmanni
    }

    void LateUpdate()
    {
        // Ef myndavélin er stöðug, geyma núverandi tíma
        if (isStatic)
        {
            smoothStartTime = Time.time;
            return;
        }
        // Ef myndavélin var stöðug en er það ekki lengur, reikna út hve stórt hlutfall af hreyfingunni er búið m.v. smoothTime
        if (smoothStartTime != -1f)
        {
            smoothTimeComplete = (Time.time - smoothStartTime) / smoothTime;
        }

        // Færa myndavél ef leikmaður er á miðjum skjánum og myndavélin er ekki farin fram hjá enda borðsins
        if (camera.WorldToScreenPoint(playerTransform.position).x > Screen.width / 2 &&
            camera.WorldToScreenPoint(levelEnd.transform.position).x > Screen.width)
        {
            Vector3 newPos = playerTransform.position + offset;
            newPos.y = transform.position.y;  // y-staðsetningin á alltaf að vera sú sama
            // Ef myndavélin var stöðug, nota slerp til að færa hana
            if (smoothStartTime != -1f)
            {
                transform.position = Vector3.Slerp(transform.position, newPos, smoothTimeComplete);
            }
            // Annars færa hana beint
            else
            {
                transform.position = newPos;
            }
        }
        // Ef leikmaður er lengst til vinstri á skjánum, stoppa hann af
        if (camera.WorldToScreenPoint(playerTransform.position).x < playerSpriteRenderer.sprite.rect.width)
        {
            playerController.SetFarLeft(true);
        }
        else
        {
            playerController.SetFarLeft(false);
        }
    }
}
