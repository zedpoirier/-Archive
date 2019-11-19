using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    enum State { Start, Game, End };

    [SerializeField]
    private State state = State.Start;

    [SerializeField]
    private Vector3 startingPos = new Vector3(0f, 0.15f, 0f);

    public float moveSpeed = 10f;
    public float turnSpeed = 50f;
    public float burrowDepth = 1f;
    public float burrowSpeed = 1f;
    public bool isHidden = false; // This bool activates whenever you use the Hide ability
    public float animOffset = 5.58f;
    public float spawnTime = 1.475f;
    public bool spawned = false;
    public ParticleSystem dust;
    public ParticleSystem toxic;

    private Rigidbody rb;
    private bool isBurrowing;
    private Animator anim;
    private bool paused = false;

    private SFXManager sfx;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        sfx = GameObject.FindGameObjectWithTag("sfx").GetComponent<SFXManager>();
    }

    void Update() {
        if (state == State.Game) {
            CheckSpawn();
        }

        if (state == State.End) {
            toxic.gameObject.SetActive(true);
        }

        if (!paused) {
            CheckMoving();

            if (state == State.Game) {
                if (!isHidden && !isBurrowing) {
                    Turn();
                }

                Burrow();
            }

            if (state == State.End) {
                if (!rb.useGravity) {
                    rb.useGravity = true;
                }
            }

            Slow();
        }
        
    }

    private void FixedUpdate() {
        if (!isHidden && !isBurrowing && !paused) {
            Move();
        }
    }

    private void Burrow() {
        if (Input.GetKeyDown(KeyCode.Space) && !isBurrowing) {
            isBurrowing = true;
            anim.SetTrigger("burrow");
            sfx.PlayClip(0);
            dust.Play();
        }

        if (isBurrowing && !isHidden) {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(
                    transform.position.x,
                    startingPos.y - burrowDepth,
                    transform.position.z),
                Time.deltaTime * burrowSpeed);

            if (transform.position.y.Equals(startingPos.y - burrowDepth)) {
                isBurrowing = false;
                isHidden = true;
                dust.Stop();
            }
        } else if (isBurrowing && isHidden) {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(
                    transform.position.x,
                    startingPos.y,
                    transform.position.z),
                Time.deltaTime * burrowSpeed);

            if (transform.position.y.Equals(startingPos.y)) {
                isBurrowing = false;
                isHidden = false;
                dust.Stop();
            }
        }
    }

    private void Turn() {
        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime);
    }

    private void Slow() {
        float input = Input.GetAxis(state == State.End ? "Horizontal" : "Vertical");
        if (Mathf.Abs(input) > 0f) {
            rb.velocity = Vector3.zero;
        }
    }

    // handles basic player movement on a flat surface, does not have jump option, nor is it necessary due to our choice of mechanics.
    private void Move() {
        if (state == State.Start) {
            rb.AddForce(transform.forward * Input.GetAxis("Vertical") * moveSpeed * 50);
        } else if (state == State.Game) {
            rb.AddForce(transform.forward * Input.GetAxis("Vertical") * moveSpeed * 100);
        } else if (state == State.End) {
            rb.AddForce(transform.forward * Input.GetAxis("Horizontal") * moveSpeed * 100);
        }
    }

    private void CheckMoving() {
        float vert = Input.GetAxis("Vertical");
        float hori = Input.GetAxis("Horizontal");
        if (vert + hori != 0f) {
            anim.SetBool("moving", true);
        }
        else {
            anim.SetBool("moving", false);
        }
    }

    private void CheckSpawn() {
        if (spawnTime <= 0f && !spawned) {
            transform.Translate(Vector3.forward * animOffset);
            spawned = true;
            paused = false;
        }
        spawnTime -= Time.deltaTime;
    }

    private void ResetPosition() {
        rb.position = startingPos;
        spawned = false;
        spawnTime = 1.475f;
        anim.SetTrigger("emerge");
    }

    public void Die() {
        sfx.PlayClip(1);
        ResetPosition();
        UIManager.Instance.IncrementDeaths();
    }

    public void Escape() {
        ResetPosition();
        UIManager.Instance.IncrementEscapes();
    }
}
