using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public MonsterController monster;
    public Vector3 offset;
    public float lerpSpeed;

    void Start()
    {
        offset = transform.position - monster.transform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position, 
            monster.transform.position + offset, 
            lerpSpeed);
    }
}
