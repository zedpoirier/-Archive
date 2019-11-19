using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Egg : MonoBehaviour
{
    [SerializeField]
    private GameObject chickPrefab;

    [SerializeField]
    private float hatchTime = 2f;

    private Vector3 landingSpot;
    private bool falling;

    // Start is called before the first frame update
    void Start()
    {
        landingSpot = new Vector3(
            transform.position.x, 
            (transform.localScale.y / 2) + ((transform.localScale.y / 2) * .1f), 
            transform.position.z);

        falling = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (falling)
        {
            Fall();
        }
        else
        {
            Hatch();
        }
    }

    private int multiplier = 1;
    private void Fall()
    {
        transform.position = Vector3.Lerp(transform.position, landingSpot, Time.deltaTime * multiplier);
        multiplier++;

        if (Vector3.Distance(transform.position, landingSpot) <= 0.05f)
        {
            falling = false;
        }
    }

    private void Hatch()
    {
        hatchTime -= Time.deltaTime;
        if (hatchTime <= 0f)
        {
            GameObject chick = Instantiate(
                chickPrefab,
                transform.position,
                transform.rotation,
                transform.parent);

            chick.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            Destroy(gameObject);
        }
    }
}
