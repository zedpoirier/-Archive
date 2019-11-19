using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform player;
    public Zoom zoomer;
    public float smoothSpeed = 5f;
    public Vector3 offset;
    	
	// Update is called once per frame
	void FixedUpdate () {
        if (zoomer.winning)
        {
            transform.position = new Vector3(0, 0, -10);
        }
        else if (!zoomer.zoomed && !zoomer.zooming)
        {
            Following();
        }
    }

    public void Following()
    {
        Vector3 targetPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
