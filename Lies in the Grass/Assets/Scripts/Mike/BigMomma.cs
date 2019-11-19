using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMomma : MonoBehaviour
{
    [SerializeField]
    private GameObject eggPrefab;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private float spawnRate = 5f;

    [SerializeField]
    private float maxChicks = 25;

    private GameObject chicks;
    private int chickCount;


    // Start is called before the first frame update
    void Start()
    {
        chicks = GameObject.Find("Chicks");
        chickCount = chicks.transform.childCount;
        InvokeRepeating("Spawn", 0f, spawnRate);
    }

    private void Spawn()
    {
        if (chickCount < maxChicks)
        {
            GameObject egg = Instantiate(
                eggPrefab,
                spawnPoint.position,
                transform.rotation,
                chicks.transform) as GameObject;

            chickCount += 1;
        }
    }

    public void DecrementChickCount()
    {
        chickCount -= 1;
    }
}
