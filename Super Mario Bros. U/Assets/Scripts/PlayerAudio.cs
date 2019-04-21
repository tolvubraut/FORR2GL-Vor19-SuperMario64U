using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip deathSound;
    private AudioSource audioSource;
    private Dictionary<string, AudioClip> audioMappings;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioMappings = new Dictionary<string, AudioClip>();
        
        // Tengja strengi (nöfn á atburðum) við hljóð
        audioMappings.Add("Jump", jumpSound);
        audioMappings.Add("Death", deathSound);
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }

    public void PlayAudio(string eventName)
    {
        audioSource.PlayOneShot(audioMappings[eventName]);
    }
}
