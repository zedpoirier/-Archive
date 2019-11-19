using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour {

    public Transform player;
    public Vector3 playerPos;
    public Transform person;
    public Vector3 personPos;
    public Vector3 targetPos;
    public float camSizeTarget = 4f;
    public bool zoomed = false;
    public bool zooming = false;
    public bool winning = false;
    public float timer;
    public float speedFactor = 0.25f;
    public Camera cam;
    public bool loverTime = false;
    	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (!zoomed && !zooming)
        {
            playerPos = player.position;
            //transform.position = new Vector3(playerPos.x, playerPos.y, -10);
        }

        if (zooming)
        {
            CameraMovement(loverTime);
        }

        if (winning)
        {
            WinZoom();
        }
	}

    public void ZoomOnConvo()
    {
        timer = 0.0f;
        Vector3 focusPoint = (personPos + playerPos) / 2;
        targetPos = new Vector3(focusPoint.x, focusPoint.y, -10);
        camSizeTarget *= 0.5f;
        zoomed = true;
        zooming = true;
    }

    public void UnZoomOnConvo()
    {
        timer = 0.0f;
        targetPos = playerPos;
        camSizeTarget *= 2f; 
        zoomed = false;
        zooming = true;
    }

    public void CameraMovement(bool zoom)
    {
        player.GetComponent<PlayerMovement>().stopped = true;
        float x = Mathf.Lerp(transform.position.x, targetPos.x, timer * speedFactor);
        float y = Mathf.Lerp(transform.position.y, targetPos.y, timer * speedFactor);
        transform.position = new Vector3(x, y, -10);

        if (!zoom)
        {
            ZoomEffect();
        }
        else if (zoom)
        {
            if (timer >= 2.0f)
            {
                if (!zoomed)
                {
                    player.GetComponent<PlayerMovement>().stopped = false;
                    zooming = false;
                    loverTime = false;
                }
                else
                {
                    timer = 0;
                    targetPos = playerPos;
                    zoomed = false;
                }
            }
        }
    }

    public void ZoomEffect()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, camSizeTarget, timer * speedFactor);
        if (timer >= 1.0f)
        {
            zooming = false;
            if (!zoomed)
            {
                player.GetComponent<PlayerMovement>().stopped = false;
            }
        }
    }

    public void WinZoom()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 50f, timer * 0.05f);
    }

    public void LoverZoom()
    {
        targetPos = new Vector3(0, 0, -10);
        loverTime = true;
        zooming = true;
        zoomed = true;
        timer = 0;
    }
}
