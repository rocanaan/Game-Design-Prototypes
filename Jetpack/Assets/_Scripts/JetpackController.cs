using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackController : MonoBehaviour {

	public GameObject currentFuelObject;
	public GameObject ejectedFuelObject;
	private Transform fuelObjectTransform;

	public float ejectSpeed;

	public int maxFuel;
	private int currentFuel;

	// Use this for initialization
	void Start () {
		fuelObjectTransform = currentFuelObject.transform;
		currentFuel = maxFuel;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public int getCurrentFuel(){
		return currentFuel;
	}

	public void FireJetpack (){
		currentFuel--;


		RescaleFuel ();

		EjectFuel ();
	}

	public void EjectFuel (){
		Vector3 ejectPosition = transform.TransformPoint (Vector3.down);
		GameObject ejectedFuel = Instantiate (ejectedFuelObject, ejectPosition, Quaternion.identity);
		ejectedFuel.GetComponent<Rigidbody2D> ().velocity = Vector3.down * ejectSpeed;

	}

	private void RescaleFuel(){
		float fuelRatio = (float)currentFuel / (float)maxFuel;

		Vector3 scale = fuelObjectTransform.localScale;
		scale.y = fuelRatio;
		fuelObjectTransform.localScale = scale;

		Vector3 position = fuelObjectTransform.localPosition;
		position.y = (fuelRatio - 1) / 2.0f;
		fuelObjectTransform.localPosition = position;
	}

	public void Refuel(){
		currentFuel = maxFuel;
		RescaleFuel ();
	}
}
