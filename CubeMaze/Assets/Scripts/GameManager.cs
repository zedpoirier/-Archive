using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazePrefab;
	public Player playerPrefab;
	public Mask maskPrefab;
	public bool begun = false;


	private Maze mazeInstance;
	private Player playerInstance;
	private Mask maskInstance;
	private Eye eyeInstance;
	private GameObject winText;
	private GameObject controlText;
	private GameObject levelImage;
	private GameObject titleText;
	private GameObject spaceText;


	// Use this for initialization
	private void Start () {
		controlText = GameObject.Find ("Controls");
		levelImage = GameObject.Find ("Image");
		titleText = GameObject.Find ("Title");
		spaceText = GameObject.Find ("Space");
		winText = GameObject.Find ("Win");
		winText.SetActive (false);
	}


	private void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && !begun) {
			BeginGame ();
			levelImage.SetActive (false);
			controlText.SetActive (false);
			titleText.SetActive (false);
			spaceText.SetActive (false);
			begun = true;
		} 
		else if (Input.GetKeyDown (KeyCode.Space) && begun) {
			ReStartGame ();
			levelImage.SetActive (false);
			controlText.SetActive (false);
			titleText.SetActive (false);
			spaceText.SetActive (false);
			if (winText.activeInHierarchy) {
				winText.SetActive (false);
			}
			eyeInstance = GameObject.FindGameObjectWithTag ("Eye").GetComponent<Eye> ();
			eyeInstance.EyeEnder ();
		}
	}
	

	private void BeginGame(){
		mazeInstance = Instantiate (mazePrefab) as Maze;
		maskInstance = Instantiate(maskPrefab) as Mask;
	}


	private void ReStartGame(){
		Destroy (mazeInstance.gameObject);
		Destroy (maskInstance.gameObject);
		BeginGame ();
	}


	public void Win(){
		levelImage.SetActive (true);
		titleText.SetActive (true);
		spaceText.SetActive (true);
		winText.SetActive (true);
		winText.transform.localPosition = new Vector3(0, 0, 0);

	}
}
