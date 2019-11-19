using UnityEngine;
using System.Collections;

public class Guide : MonoBehaviour {

	public bool spinning = false;
	public Maze maze;

	private Quaternion targetRotation;
	private float rotationAmount = 90.0f;


	private void Start(){
		targetRotation = transform.rotation;
		maze = GameObject.FindGameObjectWithTag ("Maze").GetComponent<Maze>();
	}

	
	void Update(){
		
		spinning = maze.spinning;

		//Quaternion executions ORDER MATTERS rot * Quat = local, Quat * rot = World!!!
		if (Input.GetKeyDown (KeyCode.RightArrow) && !spinning) {
			targetRotation = Quaternion.AngleAxis(rotationAmount, Vector3.down) * targetRotation;
			spinning = true;
		}
		else if (Input.GetKeyDown (KeyCode.LeftArrow) && !spinning) {
			targetRotation = Quaternion.AngleAxis(rotationAmount, Vector3.up) * targetRotation;
			spinning = true;
		}
		else if (Input.GetKeyDown (KeyCode.DownArrow) && !spinning) {
			targetRotation = Quaternion.AngleAxis(rotationAmount, Vector3.left) * targetRotation;
			spinning = true;
		}
		else if (Input.GetKeyDown (KeyCode.UpArrow) && !spinning) {
			targetRotation = Quaternion.AngleAxis(rotationAmount, Vector3.right) * targetRotation;
			spinning = true;
		}
		//If not spinning then start spinning!
		if (spinning) {
			transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, 8 * Time.deltaTime);
			if (transform.rotation == targetRotation) {
				spinning = false;
			}
		}
	}
}
