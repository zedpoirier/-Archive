using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour { 

    public Transform orbitPoint;
    public float orbitSpeed = 5.0f;

    void Update() {
        transform.RotateAround(orbitPoint.position, Vector3.right, orbitSpeed * Time.deltaTime);
    }
}
