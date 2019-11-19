using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public LayerMask blockingLayer;
	public Camera camera1;
	public Maze maze;
	public Eye eye;
	public float lerpTime = 0.4f;
	public bool moving = false;
	public Vector3 moveUp;
	public Vector3 moveDown;
	public Vector3 moveRight;
	public Vector3 moveLeft;
	public Vector3 startingPosition;
	public Vector3 destination;

	private Quaternion targetRotation;
	private float currentLerpTime;
	private GameManager gamemanager;


	private void Start(){
		camera1= GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera> ();
		maze = GameObject.FindGameObjectWithTag ("Maze").GetComponent<Maze>();
		gamemanager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		eye = GameObject.FindGameObjectWithTag ("Eye").GetComponent<Eye> ();
	}

	private void Update(){
		//Set forward (blue axis) equal to camera's in all frames.
		transform.forward = camera1.transform.forward;

		if (Input.GetKey (KeyCode.W) && !moving && !maze.spinning) {
			destination = transform.position + moveUp;
			CheckSpot ();
		}
		else if (Input.GetKey (KeyCode.A) && !moving && !maze.spinning) {
			destination = transform.position + moveLeft;
			CheckSpot ();
		}
		else if (Input.GetKey (KeyCode.S) && !moving && !maze.spinning) {
			destination = transform.position + moveDown;
			CheckSpot ();
		}
		else if (Input.GetKey (KeyCode.D) && !moving && !maze.spinning) {
			destination = transform.position + moveRight;
			CheckSpot ();
		}

		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > lerpTime) {
			currentLerpTime = lerpTime;
		}

		//Move the Player
		if (moving) {
			float perc = currentLerpTime / lerpTime;
			transform.position = Vector3.Lerp (startingPosition, destination, perc);
			if (transform.position == destination) {
				moving = false;
			}
		}
	}


	private void CheckSpot(){
		//Repeated code for each direction
		moving = true;
		currentLerpTime = 0;
		startingPosition = transform.position;
		//RayCast 
		bool hit = Physics.Linecast (startingPosition, destination, blockingLayer);
		//True is hitting a collider, false otherwise.
		if (hit) {
			moving = false;
			return;
		} 
		else {
			moving = true;
		}
	}

	private void OnTriggerEnter() {
		gamemanager.Win ();
	}
}
