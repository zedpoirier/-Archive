using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCamera : MonoBehaviour
{
    private Transform startParent;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        startParent = transform.parent;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Attach();
    }

    // Update is called once per frame
    void Update()
    {
        ChooseParent();
    }

    private void ChooseParent()
    {
        if (player.position.z < 3.3f || player.position.z > 31f)
        {
            Eject();
        }
        else
        {
            Attach();
        }
    }

    private void Attach()
    {
        if (transform.parent != player)
        {
            transform.SetParent(player);
        }
    }

    private void Eject()
    {
        if (transform.parent != startParent)
        {
            transform.SetParent(startParent);
        }
    }
}
