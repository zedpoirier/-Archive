using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Maze : MonoBehaviour {

	public int size = 7;
	public Player playerPrefab;
	public Mask maskPrefab;
	public GameObject[] mazeComponents;

	public bool spinning = false;

	private Player playerInstance;
	private float rows;
	private float columns;
	private float lanes;
	private float posX;
	private float posY;
	private float posZ;
	private float offsetX;
	private float offsetY;
	private float offsetZ;
	private Quaternion targetRotation;
	private float rotationAmount = 90.0f;
	private List <Vector3> gridPositions = new List<Vector3>();


	void Start(){
		rows = size;
		columns = size;
		lanes = size;
		offsetX = 0.5f * size - 0.5f;
		offsetY = 0.5f * size - 0.5f;
		offsetZ = 0.5f * size - 0.5f;
		targetRotation = transform.rotation;

		InitialiseList ();
		MazeSetup ();
	}


	void Update(){
		//Quaternion executions ORDER MATTERS rot * Quat = local, Quat * rot = World!!!
		if (Input.GetKeyDown (KeyCode.RightArrow) && !spinning && !playerInstance.moving) {
			targetRotation = Quaternion.AngleAxis(rotationAmount, Vector3.down) * targetRotation;
			spinning = true;
		}
		else if (Input.GetKeyDown (KeyCode.LeftArrow) && !spinning && !playerInstance.moving) {
			targetRotation = Quaternion.AngleAxis(rotationAmount, Vector3.up) * targetRotation;
			spinning = true;
		}
		else if (Input.GetKeyDown (KeyCode.DownArrow) && !spinning && !playerInstance.moving) {
			targetRotation = Quaternion.AngleAxis(rotationAmount, Vector3.left) * targetRotation;
			spinning = true;
		}
		else if (Input.GetKeyDown (KeyCode.UpArrow) && !spinning && !playerInstance.moving) {
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
		

	void InitialiseList(){
		gridPositions.Clear ();
		for (int x = 0; x < rows -1; x++) 
		{
			for (int y = 0; y < columns -1; y++) 
			{
				for (int z = 0; z < lanes -1; z++) 
				{
					gridPositions.Add (new Vector3 (x, y, z));
				}
			}
		}
	}


	public void MazeSetup (){
		//EmptyMaze ();
		for (int x = 0; x < rows; x++) {
			for (int y = 0; y < columns; y++) {
				for (int z = 0; z < lanes; z++) {
					GameObject toInstantiate = mazeComponents [Random.Range (3, mazeComponents.Length)];
					posX = x;
					posY = y;
					posZ = z;
					//Setup Invisible Walls x,y,z - 1 and rows,columns,lanes
					if (x == 1 && y == 1 && z == 1) {
						toInstantiate = mazeComponents [1]; //Entrance
						// Also spawn player
						playerInstance = Instantiate (playerPrefab, new Vector3 (posX - offsetX, posY - offsetY, posZ - offsetZ), Quaternion.identity) as Player;
						playerInstance.transform.SetParent (this.transform);
					}
					else if (x == 0 || y == 0 || z == 0 || x == rows - 1 || y == columns - 1 || z == lanes - 1) {
						toInstantiate = mazeComponents [0]; //Invisible Wall
					}
					else if (x == rows - 2 && y == columns - 2 && z == lanes - 2) {
						toInstantiate = mazeComponents [2]; //Goal
					}

					GameObject instance = Instantiate (toInstantiate, new Vector3 (posX - offsetX, posY - offsetY, posZ - offsetZ), Quaternion.identity) as GameObject;
					instance.transform.SetParent(this.transform);
				}
			}
		}
	}


	void EmptyMaze(){
		while (transform.childCount > 0) {
			Transform c = transform.GetChild (0);
			c.SetParent (null); // become Batman
			Destroy (c.gameObject); // become The Joker
		}
	}



}
