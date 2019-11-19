using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeBoundary : MonoBehaviour {
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerController>().Escape();
        }
        if (other.gameObject.CompareTag("critter")) {
            other.gameObject.GetComponent<Critter>().Escape();
        }
    }
}