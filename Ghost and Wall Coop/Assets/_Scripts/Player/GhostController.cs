﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GhostController : MonoBehaviour {


	public float angularSpeed;
	public float moveSpeed;
	public GameObject body;

	public float cooldown;
	private float nextActivation;

	private PlayerController pc;
	private CircleCollider2D col;
	private MeshRenderer meshRenderer;



	// TO DO: See if a better implementation is to use an enum or #define
	private int state;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		//rb.angularVelocity = angularSpeed;
		col = GetComponent<CircleCollider2D>();
		meshRenderer = GetComponent<MeshRenderer> ();

		pc = body.GetComponent<PlayerController> ();

	
		state = 0;
		col.enabled = false;

		nextActivation = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		if (state == 0 || state == 2) {
			transform.position = new Vector3 (body.transform.position.x, body.transform.position.y, transform.position.z);
		}
		if (state == 2 && Time.time >= nextActivation) {
			stateTransition (0);
		}
	}

	public void ghostAction ()
	{
		if (state == 0){
			Vector3 direction = pc.getFireDirection ();
			rb.velocity = direction * moveSpeed;
			stateTransition (1);
		}
		else if (state == 1) {
			rb.velocity = Vector3.zero;
			body.transform.position = new Vector3 (transform.position.x, transform.position.y, body.transform.position.z);
			stateTransition (2);

		}
	}

	public void collidesWithBoundary(){
		if (state == 1) {
			rb.velocity = Vector3.zero;
			stateTransition (2);
		}
			

	}

	void stateTransition(int nextState){
		if (nextState == 0) {
			state = 0;
			col.enabled = false;
			meshRenderer.enabled = true;
		}
		if (nextState == 1) {
			state = 1;
			col.enabled = true;
			meshRenderer.enabled = true;
		}
		if (nextState == 2) {
			if (cooldown > 0.0f) {
				state = 2;
				col.enabled = false;
				meshRenderer.enabled = false;
				nextActivation = Time.time + cooldown;
			} else {
				stateTransition (0);
			}
		}
	}

}
