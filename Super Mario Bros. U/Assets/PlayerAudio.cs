using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip jumpSound;
    private AudioSource audioSource;
    private Dictionary<string, AudioClip> audioMappings;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Map event name strings to sounds
        audioMappings = new Dictionary<string, AudioClip>();
        audioMappings.Add("Jump", jumpSound);
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
