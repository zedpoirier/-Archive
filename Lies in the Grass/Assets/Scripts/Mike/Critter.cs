using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Critter : MonoBehaviour {
    public GameObject guts;
    public float animOffset = 8f;
    public float spawnTime = 2f;
    public Animator anim;
    private SFXManager sfx;

    public NavController navController;
    public NavMeshAgent agent;
    public Collider capsule;

    private float deathAnimTime = 2f;
    private float timer = 0f;
    private bool moved = false;
    private CritterSpawner spawner;


    private void Start() {
        Vector3 pos = transform.position;
        pos = new Vector3(pos.x, pos.y + 0.35f, pos.z);
        transform.position = pos;
        sfx = GameObject.FindGameObjectWithTag("sfx").GetComponent<SFXManager>();
    }

    private void Update() {
        
        if(spawnTime <= 0f && !moved){
            transform.Translate(Vector3.forward * animOffset);
            moved = true;
            agent.enabled = true;
            capsule.enabled = true;
            navController.enabled = true;
        }
        spawnTime -= Time.deltaTime;
    }


    public CritterSpawner Spawner {
        get { return spawner; }
        set { spawner = value; }
    }

    public void Die() {
        spawner.DecrementCritterCount();
        UIManager.Instance.IncrementDeaths();
        GameObject temp = Instantiate(guts, transform.position, transform.rotation);
        Destroy(temp, 5f);
        sfx.PlayClip(1);
        Destroy(gameObject, deathAnimTime);
    }

    public void Escape() {
        spawner.DecrementCritterCount();
        UIManager.Instance.IncrementEscapes();
        Destroy(gameObject, deathAnimTime);
    }
}
