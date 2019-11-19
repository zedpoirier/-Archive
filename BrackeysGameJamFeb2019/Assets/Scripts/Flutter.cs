using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flutter : MonoBehaviour {

    public ParticleSystem particles;
    public float particleCooldown = 2.0f;
    public float timer;

    // Use this for initialization
    void Start () {
        timer = particleCooldown;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
	}

    public void SpawnFlutter()
    {
        if (timer > particleCooldown)
        {
            ParticleSystem flutter = Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(flutter.gameObject, 2.0f);
            timer = 0;
        }
    }
}
