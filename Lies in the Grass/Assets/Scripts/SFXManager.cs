using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioClip[] clips;

    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();    
    }

    public void PlayClip(int index) {
        source.clip = clips[index];
        source.Play();
    }
}
