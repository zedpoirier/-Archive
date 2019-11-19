using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public List <GameObject> PatrolRoute;
    public float RotationSpeed;
    public float MoveSpeed;
    public int CurrentWaypoint;

    private Rigidbody rb;
    private Vector3 destination;
    public bool Turning = true;
    public bool Moving = false;
    private float timer = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ChangeTarget();
    }

    void Update()
    {
        Vector3 WaypointDirection = PatrolRoute[CurrentWaypoint].transform.position - transform.position;
        float WaypointDistance = WaypointDirection.magnitude;

        if (Turning == true)
        {
            timer += Time.deltaTime;
            Quaternion rot = Quaternion.LookRotation(WaypointDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, timer * 0.5f);
            if (timer >= 1f)
            {
                timer = 0f;
                Moving = true;
                Turning = false;
            }
        }
        if (Moving == true)
        {
            rb.velocity = WaypointDirection.normalized * MoveSpeed * 100 * Time.deltaTime;
            if (WaypointDistance <= 0.1f)
            {
                ChangeTarget();
            }
        }
    }

    void ChangeTarget() {
        rb.velocity = Vector3.zero;
        Turning = true;
        Moving = false;
        CurrentWaypoint = Random.Range(0, PatrolRoute.Count);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "critter") {
            ChangeTarget();
            other.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }
        else if (other.tag == "bullchick") {
            ChangeTarget();
        }
    }
}
