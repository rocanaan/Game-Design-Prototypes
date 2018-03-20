using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	private GameObject nextGroup;
	private int nextIndex;
	private float lastChange;
	private float changeInterval = 0.3f;

	public GameObject[] groups;

	// Use this for initialization
	void Start () {
		lastChange = 0.0f;
		nextIndex = Random.Range (0, groups.Length);
		SpawnNext ();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Fire3") && Time.time > lastChange + changeInterval){
			generateNextBlock();
			lastChange = Time.time;
		}
	}

	public void SpawnNext(){
		Instantiate (groups [nextIndex], transform.position, Quaternion.identity);

		generateNextBlock();

	}

	void generateNextBlock(){
		nextIndex = Random.Range (0, groups.Length);
		Destroy (nextGroup);
		nextGroup = Instantiate (groups [nextIndex], new Vector3(-6.0f,7.5f,0.0f), Quaternion.identity);
		nextGroup.GetComponent<Group> ().enabled = false;
	}
}
