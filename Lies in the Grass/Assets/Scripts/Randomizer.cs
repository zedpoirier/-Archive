using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour {

    public bool fullRandom = false;
    public Vector2 minMaxScale;

    void Start() {
        float xAngle = Random.Range(0f, 360f);
        float yAngle = Random.Range(0f, 360f);
        float zAngle = Random.Range(0f, 360f);
        
        if (fullRandom) {
            transform.Rotate(xAngle, yAngle, zAngle);
        }
        else {
            transform.Rotate(0f, yAngle, 0f);
        }
        

        float scale = Random.Range(minMaxScale.x, minMaxScale.y);
        float x = transform.localScale.x;
        float y = transform.localScale.y;
        float z = transform.localScale.z;
        transform.localScale = new Vector3(x, y * scale, z);
    }
}
