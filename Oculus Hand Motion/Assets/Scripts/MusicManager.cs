using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> audioClips = new List<AudioClip>();
    private System.Random rand = new System.Random();

    public void PlayMusic()
    {
        int musicNum = rand.Next(audioClips.Count);
        audioSource.clip = audioClips[musicNum];
        audioSource.Play();
    }

    public void MusicStop()
    {
        audioSource.Stop();
    }
}
