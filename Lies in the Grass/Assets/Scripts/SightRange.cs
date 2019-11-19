using UnityEngine;

public class SightRange : MonoBehaviour {

    public NavController nav;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("critter") || 
            other.gameObject.CompareTag("Player")) 
        {
            nav.SelectPatrolTarget(other.gameObject.transform.position);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag("critter") ||
            other.gameObject.CompareTag("Player")) 
        {
            nav.SelectPatrolTarget();
        }
    }

}
