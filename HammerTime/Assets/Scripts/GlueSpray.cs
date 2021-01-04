using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Clayxels;

public class GlueSpray : MonoBehaviour
{
    ClayContainer clay;
    public float fadeOffset = 0.1f;

    void Start()
    {
        clay = GetComponent<ClayContainer>();

    }

    void Update()
    {
        clay.forceUpdate = true;
    }

    public void EndAnimation()
    {
        gameObject.SetActive(false);
    }
}
