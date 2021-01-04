using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nail : MonoBehaviour
{
    public GameNails gameNails;
    public Vector3 startPos;
    public Vector3 endPos;
    public float endOffset = 0.1f;
    public int hitCount;
    public int hitsNeeded = 3;
    public bool hammeredFully;

    public GameObject colliders;

    void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(startPos.x, startPos.y - endOffset, startPos.z);
    }

    private void Update()
    {
        if (MiniGameManager.instance.gameActive == false) gameObject.SetActive(false);
    }

    void HammerDown()
    {
        Vector3 partialMove = Vector3.Lerp(startPos, endPos, (float)hitCount / (float)hitsNeeded);
        transform.position = partialMove;
        if (hitCount >= hitsNeeded)
        {
            hammeredFully = true;
            gameNails.UpdateScore();
            colliders.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Hammer" && !hammeredFully)
        {
            hitCount++;
            HammerDown();
        }
    }
}
