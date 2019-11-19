// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Destructable will destroy this object if the token collides with it.
/// </summary>
public class Destructable : MonoBehaviour {

    [Header("Destructable Parameters")]
    [Tooltip("Number of hits before destruction")]  public int hitPoints = 1;
    [Tooltip("Reference to the Puffs")]             public GameObject puff;
	
    
    void PuffDestroy() {
        hitPoints--;
        if (hitPoints <= 0) {
            GameObject puffy = Instantiate(puff);
            puffy.transform.position = transform.position;
            puffy.transform.rotation = transform.rotation;
            gameObject.SetActive(false);
        }
    }
}
