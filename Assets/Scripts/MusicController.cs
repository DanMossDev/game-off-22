using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    AudioSource audioSource;

    public static MusicController Instance {get; private set;}
    void Awake() 
    {
        if (Instance != null && Instance != this)  Destroy(this); 
        else Instance = this; 
    }
    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Play(AudioClip[] song, float volume = 1)
    {
        audioSource.loop = true;
        audioSource.clip = song[Random.Range(0, song.Length)];
        audioSource.Play();
    }
    public void Stop()
    {
        audioSource.Stop();
    }
}
