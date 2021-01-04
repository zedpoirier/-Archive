using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [Header("Sway Properties")]
    [Range(-1f, 1f)]
    public float sway;
    public bool steppingRight;
    public float swaySpeed;
    public float minSwapDist = 0.1f;
    public float rotationMaximum = 45f;
    public float[] swayMilestones;
    public bool stepping;
    bool firstStep;
    float vertical;
    float horizontal;
    float currentMaxValue;

    [Header("Movement Properties")]
    public float forwardMoveSpeed;
    public float currentStepDistance;
    public float stepLeanAmount;
    public Vector3 lastPosition;
    public Vector3 nextPosition;

    [Header("References")]
    public Transform meshRoot;
    public MeterController meter;

    private void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        // get raw input
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");

        Sway();
        Move();
    }

    void Sway()
    {
        // check for first step
        if (!firstStep && horizontal != 0f)
        {
            steppingRight = horizontal > 0f ? true : false;
            firstStep = true;
            meter.ActivateZones();
        }

        // calc and clamp sway : -1 to 1
        sway = Mathf.Clamp(sway + (horizontal * swaySpeed * Time.deltaTime), -1f, 1f);

        // add "gravity" to sway with no input
        sway += sway * Time.deltaTime;

        // check for input direction switch and swap
        if (steppingRight && sway > 0f)
        {
            stepping = true;
        }
        else if (!steppingRight && sway < 0f)
        {
            stepping = true;
        }

        if (Mathf.Abs(sway) > currentMaxValue && stepping)
        {
            currentMaxValue = Mathf.Abs(sway);
        }
        else
        {
            if (currentMaxValue - Mathf.Abs(sway) > minSwapDist)
            {
                // register step
                meter.SwapZones();
                currentMaxValue = 0f;
                stepping = false;
                currentStepDistance = Mathf.Abs(sway);
                lastPosition = transform.position;
                steppingRight = !steppingRight;
            }
        }
    }

    void Move()
    {
        // rotate monster root
        float rotation = -sway * rotationMaximum;
        meshRoot.localRotation = Quaternion.Euler(0, 0, rotation);

        // TODO: lean monster by factor of sway
        

        // move monster forward by factor of sway and moveSpeed
        if (Mathf.Abs(sway) > 0f && stepping)
        {
            float forwardPos = transform.position.z + (currentStepDistance * forwardMoveSpeed);
            nextPosition = new Vector3(transform.position.x, 0, forwardPos);
            transform.position = Vector3.Lerp(lastPosition, nextPosition, Mathf.Abs(sway));
        }

        // TODO: set bump back in other direction on step

        
    }

    void Bump()
    {

    }
}
