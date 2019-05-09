using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    void Update()
    {
        // Fara á upphafsskjá ef notandi ýtir á Cancel (esc)
        if (Input.GetButton("Cancel"))
        {
            VarManager.GoToMainMenu();
        }
    }
}
