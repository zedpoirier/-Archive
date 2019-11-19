using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Hiding");
            // Player automatically hides whenever they are inside the rock's aoe (the second collider).
            // In grass, it will be so long as the player is inside the grass.
            // Any enemy in pursuit will stop.
        }
    }
}
