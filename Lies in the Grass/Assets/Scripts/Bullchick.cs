using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bullchick : MonoBehaviour {
    [SerializeField]
    private float graveY = -2f;

    [SerializeField]
    private float decayTime = 10f;

    private NavController navController;
    private Animator anim;
    private bool isDead;
    private Vector3 gravePosition;
    private float decayCounter;

    private void Start() {
        navController = GetComponent<NavController>();
        anim = GetComponent<Animator>();
        graveY = -2f;
        isDead = false;
        decayCounter = 0f;
    }

    private void Update() {
        if (isDead) {
            Decay();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("bullchick")) {
            navController.SelectPatrolTarget();
        }
        if (other.gameObject.CompareTag("critter")) {
            anim.SetTrigger("attack");
            other.gameObject.GetComponent<Critter>().Invoke("Die", 0.7f);
        }
        if (other.gameObject.CompareTag("Player")) {
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();
            if (!pc.isHidden) {
                anim.SetTrigger("attack");
                pc.Invoke("Die", 0.7f);
                Invoke("Die", 1.5f);
                navController.enabled = false;
            }
        }
    }

    private void Die() {
        GameObject.FindGameObjectWithTag("BigMomma").GetComponent<BigMomma>().DecrementChickCount();
        anim.SetTrigger("die");
        gravePosition = new Vector3(transform.position.x, graveY, transform.position.z);
        isDead = true;
    }

    private void Decay() {
        decayCounter += Time.deltaTime;

        if (decayCounter <= decayTime) {
            transform.position = Vector3.Lerp(transform.position, gravePosition, (Mathf.Abs(graveY) / decayTime) * Time.deltaTime);
        } else {
            Destroy(gameObject);
        }
    }
}
