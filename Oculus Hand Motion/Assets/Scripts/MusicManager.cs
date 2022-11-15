using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> audioClips = new List<AudioClip>();


    private void Awake()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
    }

    void Update()
    {
        
    }
}
