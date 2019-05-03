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
        // Færa myndavél ef leikmaður er á miðjum skjánum og myndavélin er ekki farin fram hjá enda borðsins
        if (camera.WorldToScreenPoint(playerTransform.position).x > Screen.width / 2 &&
            camera.WorldToScreenPoint(levelEnd.transform.position).x > Screen.width)
        {
            Vector3 newPos = playerTransform.position + offset;
            newPos.y = transform.position.y;  // y-staðsetningin á alltaf að vera sú sama
            transform.position = newPos;
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
