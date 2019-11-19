using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour {

    public float timer;
    public float hideTime = 2;
    public bool hiding = false;
    public ParticleSystem particles;
    public Transform player;
    public float distance = 100;
    public float hideRange = 0.5f;
    public bool usePrewarm;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update () {
        distance = Vector3.Distance(player.position, transform.position);
        if (distance < hideRange)
        {
            Hide();
            timer = 0;
        }


        if (hiding)
        {
            timer += Time.deltaTime;
            if (timer >= hideTime)
            {
                hiding = false;
                timer = 0;
                particles.Play();
            }
        }
               
	}

    void Hide()
    {
        hiding = true;
        particles.Stop();
        var main = particles.main;
        main.prewarm = usePrewarm;
    }

}
