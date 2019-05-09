using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEventHandler : MonoBehaviour
{
    void Update()
    {
        // Loka leik ef notandi ýtir á Cancel (esc)
        if (Input.GetButton("Cancel"))
        {
            Application.Quit();
        }
    }
}
