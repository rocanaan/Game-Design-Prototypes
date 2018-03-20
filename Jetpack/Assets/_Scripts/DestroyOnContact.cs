using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Fuel") {
			Destroy (other.gameObject);
		}
	}
}
