using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashEffect : MonoBehaviour
{
    public GameObject smashParticles;
    public AudioSource smashAudio;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        smashAudio.Play();
        smashParticles.transform.position = collision.contacts[0].point;
        smashParticles.transform.rotation = Quaternion.LookRotation(collision.contacts[0].normal);
        smashParticles.GetComponent<ParticleSystem>().Play();
    }
}
