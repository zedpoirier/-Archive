using UnityEngine;
using System.Collections;

public class Mask : MonoBehaviour {

	private Player player;


	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () {
		if (player == null) {
			player = GameObject.FindWithTag ("Player").GetComponent<Player>();
		}
		//Follow player's depth as per the camera
		Vector3 position = transform.position;
		position.z = player.transform.position.z + 0.49f;
		transform.position = position;
	}
}
