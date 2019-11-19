using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject critterPrefab;

    [SerializeField]
    private float spawnRate;

    [SerializeField]
    private int maxCritters;

    private int critterCount;

    // Start is called before the first frame update
    void Start() {
        critterCount = 0;
        InvokeRepeating("Spawn", 0f, spawnRate);
    }

    private void Spawn() {
        if (critterCount < maxCritters) {
            RandomizeRotation();
            GameObject critter = Instantiate(
                critterPrefab,
                transform.position,
                transform.rotation,
                transform) as GameObject;

            critter.GetComponent<Critter>().Spawner = this;
            critter.transform.SetParent(null);
            critterCount += 1;
        }
    }

    private void RandomizeRotation() {
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, Random.Range(0f, 360f), rot.z);
        transform.rotation = Quaternion.Euler(rot);
    }

    public void DecrementCritterCount() {
        critterCount -= 1;
    }
}
