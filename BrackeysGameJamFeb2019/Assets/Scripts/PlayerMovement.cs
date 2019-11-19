using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 10;
    public float halfSpeed = 5;
    public bool stopped = false;
    public bool centering = false;
    public float xDir;
    public float yDir;
	
	// Update is called once per frame
	void Update () {
        xDir = Input.GetAxisRaw("Horizontal");
        yDir = Input.GetAxisRaw("Vertical");

        if (!stopped)
        {
            if (xDir != 0 && yDir != 0)
            {
                transform.Translate(new Vector3(xDir, yDir, 0) * halfSpeed * Time.deltaTime);
            }
            else if (xDir != 0 || yDir != 0)
            {
                transform.Translate(new Vector3(xDir, yDir, 0) * speed * Time.deltaTime);
            }
        }

        if (centering)
        {
            transform.position = Vector3.zero;
        }
    }

    public void Stop()
    {
        stopped = true;
    }

    public void UnStop()
    {
        stopped = false;
    }

    public void Center()
    {
        stopped = true;
        centering = true;
    }
}
