using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] songs;

    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void ChangeSong(int index) {
        source.clip = songs[index];
        source.Play();
        source.loop = true;
    }
}
