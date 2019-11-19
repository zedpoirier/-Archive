using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Vector3 Location;

    // Start is called before the first frame update
    void Start()
    {
        Location = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
