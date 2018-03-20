using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

	public bool isActive;
	public Material materialActive;
	public Material materialInactive;
	private GameController gameController;
	private int priority;

	// Use this for initialization
	void Start () {
		// Get the game controller
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController> ();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}

		// Set the initial material
		if (isActive) {
			GetComponent<Renderer> ().material = materialActive;
		}
		else{
			GetComponent<Renderer> ().material = materialInactive;
		}
		
	}

	public void setActive(){
		isActive = true;
		GetComponent<Renderer> ().material = materialActive;
	}

	public void setInactive(){
		isActive = false;
		GetComponent<Renderer> ().material = materialInactive;
	}

	public void setPriority(int p){
		priority = p;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player") && isActive) {
			gameController.incrementPriority (priority);
			Destroy (gameObject);
		}

//		if (other.gameObject.CompareTag ("Player") && !isActive) {
//			setActive ();
//		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
