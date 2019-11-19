using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeverMove : MonoBehaviour
{
    public Transform parent;
    public bool lockX = false;
    public bool lockY = false;
    public bool lockZ = false;

    private float xStart;
    private float yStart;
    private float zStart;

    void Start()
    {
        xStart = parent.position.x;
        yStart = parent.position.y;
        zStart = parent.position.z;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        if (lockY) { transform.position = new Vector3(pos.x, yStart, pos.z); }
    }
}
