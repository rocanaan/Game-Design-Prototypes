using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateColliders : MonoBehaviour {

	public Vector3 startingPoint;
	public float scale;
	public float gridWidth;
	public int gridSize;

	private float nextKeyPress;
	private float keyDelay;

	private int state;
	// 0 corresponds to not calculated
	// 1 is active
	// 2 is inactive

	public GameObject testCollider;

	private List<GameObject> colliderGrid;

	// Use this for initialization
	void Start () {
		state = 0;
		colliderGrid = new List<GameObject> ();
		keyDelay = 0.2f;
		nextKeyPress = keyDelay;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Test") && Time.time > nextKeyPress) {	
			if (state == 0) {
				for (int i = 0; i < gridSize; i++) {
					for (int j = 0; j < gridSize; j++) {
						Vector3 offset = new Vector3 (i * gridWidth, j * gridWidth, 0);
						Vector3 pos = startingPoint + offset;
						GameObject newCollider = Instantiate (testCollider, pos, Quaternion.identity);
						newCollider.transform.localScale = new Vector3 (scale, scale, 10);
						colliderGrid.Add (newCollider);
					}
				}
				state = 1;
			} else if (state == 1) {
				foreach (GameObject obj in colliderGrid) {
					obj.SetActive (false);
				}
				state = 2;
			} else if (state == 2) {
				foreach (GameObject obj in colliderGrid) {
					obj.SetActive (true);
				}
				state = 1;
			}
			nextKeyPress = Time.time + keyDelay;
		}
	}

	public void resetTestColliders(){
		foreach (GameObject obj in colliderGrid) {
			Destroy (obj);
		}
		state = 0;
		colliderGrid = new List<GameObject> ();
	}
}
