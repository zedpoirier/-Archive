using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Eye : MonoBehaviour {

	public Player player;
	public float waitTime = 1.0f;

	//List used for open directions
	public List<Quaternion> openPaths = new List<Quaternion>();

	//SixPaths = Up, Down, Right, Left, In, Out : IN ORDER EVERYTIME
	private List<bool> sixPaths = new List<bool>();
	private bool up = false;
	private bool down = false;
	private bool right = false;
	private bool left = false;
	private bool inward = false;
	private bool outward = false;

	//List of directions to use later
	private List<Vector3> directions = new List<Vector3>();
	private Vector3 posY = new Vector3 (0, 1, 0);
	private Vector3 negY = new Vector3 (0, -1, 0);
	private Vector3 posX = new Vector3 (1, 0, 0);
	private Vector3 negX = new Vector3 (-1, 0, 0);
	private Vector3 posZ = new Vector3 (0, 0, 1);
	private Vector3 negZ = new Vector3 (0, 0, -1);

	//List of rotations
	private List<Quaternion> rotations = new List<Quaternion>();
	private Quaternion upY = Quaternion.Euler (0, 0, 90);
	private Quaternion downY = Quaternion.Euler (0, 0, 270);
	private Quaternion rightX = Quaternion.Euler (0, 0, 0);
	private Quaternion leftX = Quaternion.Euler (0, 0, 180);
	private Quaternion inZ = Quaternion.Euler (0, 270, 0);
	private Quaternion outZ = Quaternion.Euler (0, 90, 0);

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		sixPaths.Add (up);
		sixPaths.Add (down);
		sixPaths.Add (right);
		sixPaths.Add (left);
		sixPaths.Add (inward);
		sixPaths.Add (outward);
		directions.Add (posY);
		directions.Add (negY);
		directions.Add (posX);
		directions.Add (negX);
		directions.Add (posZ);
		directions.Add (negZ);
		rotations.Add (upY);
		rotations.Add (downY);
		rotations.Add (rightX);
		rotations.Add (leftX);
		rotations.Add (inZ);
		rotations.Add (outZ);
		StartCoroutine(IdleLooking ());
	}
	
	// Update is called once per frame
	void Update () {
		CheckSixPaths ();
		SetLookDirections ();
		if (Input.GetKeyDown (KeyCode.W)) {
			transform.localRotation = Quaternion.Euler (0, 0, 90);
		} 
		else if (Input.GetKey (KeyCode.A)) {
			transform.localRotation = Quaternion.Euler (0, 0, 180);
		} 
		else if (Input.GetKey (KeyCode.S)) {
			transform.localRotation = Quaternion.Euler (0, 0, 270);
		} 
		else if (Input.GetKey (KeyCode.D)) {
			transform.localRotation = Quaternion.Euler (0, 0, 0);
		}
	}


	public void CheckSixPaths(){
		for (int i = 0; i < directions.Count; i++) {
			Vector3 destination = transform.position + directions[i];
			bool hit = Physics.Linecast (transform.position, destination, player.blockingLayer);
			if (hit) {
				sixPaths [i] = true;
			} 
			else if (!hit) {
				sixPaths [i] = false;
			}
		}
	}


	public void SetLookDirections(){
		openPaths.Clear ();
		for (int i = 0; i < sixPaths.Count; i++) {
			if (!sixPaths [i]) {
				openPaths.Add (rotations[i]);
			}
		}
	}


	private IEnumerator IdleLooking(){
		while (true){
			yield return new WaitForSeconds (waitTime);
			//eye movement stuff

			transform.rotation = openPaths[Random.Range(0, openPaths.Count)];
		}
	}


	public void EyeEnder(){
		StopAllCoroutines ();
	}
}
