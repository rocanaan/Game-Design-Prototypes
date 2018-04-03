using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehavior : BossBehavior {

	private Vector3 target;
	private Rigidbody2D rb;

	public float x_range;
	public float y_range;
	public float speed;

	public float offsetTolerance;



	void Start(){
		statusActive = true;
		rb = GetComponent<Rigidbody2D> ();

		getNewTarget ();
	}
	
	// Update is called once per frame
	void Update () {
		if (statusActive) {
			Vector3 offset = (target - transform.position);
			while (offset.magnitude < offsetTolerance) {
				getNewTarget ();
				offset = (target - transform.position);
			}
			Vector3 direction = offset.normalized;
			GetComponent<Rigidbody2D> ().velocity = direction * speed;
		}

	}

	private void getNewTarget(){
		target = new Vector3 (Random.Range (-x_range, x_range), Random.Range (-y_range, y_range), transform.position.z);
	}
}
