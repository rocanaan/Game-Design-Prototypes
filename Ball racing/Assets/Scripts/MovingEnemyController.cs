using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyController : MonoBehaviour {

	public float speed;
	private float speedModifier;
	private Vector2 direction;
	private Rigidbody2D rb;
	private MovingEnemyParameters parameters;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();

		GameObject collection = GameObject.FindWithTag ("MovingEnemyCollection");
		if (collection != null)
		{
			parameters = collection.GetComponent <MovingEnemyParameters> ();
		}

		speedModifier = 1;
		if (parameters) {
			speedModifier = parameters.speedModifier;
		}
				
		Vector2 start2dDirection = Random.insideUnitCircle;
		rb.velocity = start2dDirection * speed * speedModifier;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
