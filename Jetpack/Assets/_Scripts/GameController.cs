using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {


	public GameObject playerObject;
	public Camera gameCamera;
	public float startingSpeed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		playerObject.GetComponent<Rigidbody2D> ().velocity = new Vector3 (
			getCurrentSpeed(),
			playerObject.GetComponent<Rigidbody2D>().velocity.y
		);
		gameCamera.transform.position = new Vector3 (
			playerObject.transform.position.x + 4,
			gameCamera.transform.position.y,
			gameCamera.transform.position.z
		);
	}

	private float getCurrentSpeed(){
		return startingSpeed;
	}
}
