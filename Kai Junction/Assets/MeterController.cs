using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterController : MonoBehaviour
{
    public MonsterController monster;
    public CameraController cam;

    [Header("Graphic Objects")]
    public Transform pointer;
    public Transform baseBar;
    public Transform goodZone;
    public Transform goldZone;

    Vector3 offsetToCamera;

    private void Start()
    {
        offsetToCamera = cam.transform.position - transform.position;
    }

    private void Update()
    {
        // sway meter position and rotation handling
        transform.position = cam.transform.position - offsetToCamera;
        transform.localRotation = Quaternion.LookRotation(transform.position - cam.transform.position);

        // moving the pointer
        pointer.localPosition = new Vector3(monster.sway * 0.5f, 0, 0);

    }

    public void ActivateZones()
    {
        goodZone.gameObject.SetActive(true);
        goldZone.gameObject.SetActive(true);
        if (!monster.steppingRight) SwapZones();
    }

    public void SwapZones()
    {
        goodZone.position = new Vector3(-goodZone.position.x, goodZone.position.y, goodZone.position.z);
        goldZone.position = new Vector3(-goldZone.position.x, goldZone.position.y, goldZone.position.z);
    }
}
