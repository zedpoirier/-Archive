using UnityEngine;
using System.Collections;

public class Blocks : MonoBehaviour {

	private Player player;
	private Renderer blockColor;

	// Use this for initialization
	void Start () {
		blockColor = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) {
			player = GameObject.FindWithTag ("Player").GetComponent<Player>();
		}
		HideBlocks ();
	}


	public void HideBlocks(){
		if (transform.position.z <= player.transform.position.z - 0.2) {
			//blockColor.material.color = new Color (0, 1, 0, 0);
			Color color = blockColor.material.color;
			color = new Color (0, 1, 0, 0);
			blockColor.material.color = color;
		} 
		else {
			blockColor.material.color = new Color (0, 0, 1, 1);
		}

	}
}
