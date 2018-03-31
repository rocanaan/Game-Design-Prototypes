using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingSpeed : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		rb.velocity = speed * Vector3.up;

		transform.position  = new Vector3 (transform.position.x, Random.Range (-5, 5));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
