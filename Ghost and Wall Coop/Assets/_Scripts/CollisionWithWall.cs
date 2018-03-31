using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithWall : MonoBehaviour {

	public bool ghostTrespassable;
	
	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Shot") {
			// TO DO: really figure out what is the best way to structure the rigidbody and collider of the shot, wither in the parent shot or in the child bullet,
			// especially if I decide to work with reflections
			Destroy (col.gameObject);
		}
		if (col.tag == "Ghost" && !ghostTrespassable) {
			GhostController ghostController = col.GetComponent<GhostController> ();
			ghostController.collidesWithBoundary ();
		}
	}
}
