﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Kalla á Die() aðferð PlayerHealth klasans ef leikmaður dettur niður
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().Die();
        }
        // Ef eitthvað annað en leikmaður dettur niður, eyða því
        else
        {
            Destroy(other.gameObject);
        }
    }
}
