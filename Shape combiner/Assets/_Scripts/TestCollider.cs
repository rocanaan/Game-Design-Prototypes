using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollider : MonoBehaviour {

	public Material modelMaterial;
	public Material playerMaterial;
	public Material bothMaterial;
	public Material noneMaterial;

	private bool active;

	private bool collidesWithPlayer;
	private bool collidesWithModel;


	// Use this for initialization
	void Start () {
		collidesWithPlayer = false;
		collidesWithModel = false;
		active = true;
	}

	void OnTriggerEnter(Collider col){
		print ("checking collision");
		if (col.tag == "PlayerShape") {
			collidesWithPlayer = true;
			print ("hi");
		}
		if (col.tag == "ModelShape") {
			collidesWithModel = true;
			print ("hey");
		}
	}

	// Update is called once per frame
	void Update () {
		if (collidesWithPlayer){
			GetComponent<Renderer>().material = playerMaterial;
		}
		if (collidesWithModel){
			GetComponent<Renderer>().material = modelMaterial;
		}
		if (collidesWithPlayer && collidesWithModel) {
			GetComponent<Renderer>().material = bothMaterial;
		}
		if (!collidesWithPlayer && !collidesWithModel) {
			GetComponent<Renderer>().material = noneMaterial;
		}
		
	}
}
