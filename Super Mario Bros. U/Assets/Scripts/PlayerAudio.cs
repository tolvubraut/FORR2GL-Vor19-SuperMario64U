using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip grabFlagpoleSound;
    public AudioClip clearSound;
    private AudioSource audioSource;
    private Dictionary<string, AudioClip> audioMappings;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioMappings = new Dictionary<string, AudioClip>();
        
        // Tengja strengi (nöfn á atburðum) við hljóð
        audioMappings.Add("Jump", jumpSound);
        audioMappings.Add("Death", deathSound);
        audioMappings.Add("Grab Flagpole", grabFlagpoleSound);
        audioMappings.Add("Clear", clearSound);
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }

    public float PlayAudio(string eventName)
    {
        audioSource.PlayOneShot(audioMappings[eventName]);
        return audioMappings[eventName].length;  // Skila lengd hljóðklippu
    }
}
