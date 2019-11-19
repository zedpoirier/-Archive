using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearAfterComplete : MonoBehaviour {

    public int neededChecks = 4;
    public int checks = 0;
    public Zoom zoomer;

    Animator anim;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        zoomer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Zoom>();
    }
    // Update is called once per frame
    void Update () {
        if (checks == neededChecks)
        {
            GetComponent<Collider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
            checks++;
        }
	}

    public void Found()
    {
        GetComponent<Collider2D>().enabled = false;
        anim.SetTrigger("Win");
        Debug.Log("Me?");
    }
}
