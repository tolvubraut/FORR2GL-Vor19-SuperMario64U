using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform levelEnd;
    private Camera camera;
    private Vector3 offset;

    void Start()
    {
        camera = GetComponent<Camera>();
        offset = transform.position - player.transform.position;  // Fjarlægð frá leikmanni
    }

    void LateUpdate()
    {
        // Færa myndavél ef leikmaður er á miðjum skjánum og myndavélin er ekki farin fram hjá enda borðsins
        if (camera.WorldToScreenPoint(player.transform.position).x > Screen.width / 2 &&
            camera.WorldToScreenPoint(levelEnd.transform.position).x > Screen.width)
        {
            Vector3 newPos = player.transform.position + offset;
            newPos.y = transform.position.y;  // y-staðsetningin á alltaf að vera sú sama
            transform.position = newPos;
        }
    }
}
