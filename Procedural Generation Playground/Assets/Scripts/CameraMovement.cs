using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    public float timer; // start with a negative for a delay

    Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0)
        {
            transform.position = new Vector3(timer * speed, 0, 0) + startPos;
        }
    }
}
