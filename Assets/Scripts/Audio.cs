using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] songlist;
    public AudioClip placedClip;
    public AudioClip flipClip;

    public float volume = 1;

    public void Start()
    {
        audioSource.Play();
    }

    public void PlacedClip()
    {
        audioSource.PlayOneShot(placedClip, volume);
    }

    public void FlipClip()
    {
        audioSource.PlayOneShot(flipClip, volume);
    }
}
