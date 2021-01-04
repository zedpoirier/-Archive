using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicalTracker : MonoBehaviour
{
    public Transform targetTracker;
    [Range(0f,1f)]
    public float maxSpeed = 0.5f;
    public Vector3 velocity;
    public float speed;

    Vector3 prevPosition;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        velocity = transform.position - prevPosition;
        speed = velocity.magnitude;

        Vector3 newPosition;
        newPosition = Vector3.Lerp(transform.position, targetTracker.position, maxSpeed);
        transform.localPosition = newPosition;
        prevPosition = newPosition;

        Quaternion newRotation;
        newRotation = Quaternion.Lerp(transform.rotation, targetTracker.rotation, maxSpeed);
        transform.localRotation = newRotation;
    }
}
