using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private JetpackController jetpack;

	private float carriedCoinWeight;
	private int carriedCoinValue;
	private int currentScore;

	public float bodyWeight;
	public float maxFuelWeight;

	public float upwardForce;

	private float currentTotalWeight;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		jetpack = GetComponentsInChildren<JetpackController> () [0];

		carriedCoinWeight = 0.0f;
		carriedCoinValue = 0;

		SetWeight ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Jump") && jetpack.getCurrentFuel() > 0){
			rb.AddForce (new Vector2 (0, upwardForce));
			jetpack.FireJetpack ();
		}

		Stabilize ();

		SetWeight ();
	}

	void Stabilize(){
		transform.rotation = Quaternion.identity;
	}

	public void Refuel(){
		jetpack.Refuel();
	}

	public void PickupCoin(CoinAttributes coin){
		carriedCoinWeight += coin.weight;
		carriedCoinValue += coin.value;
	}

	void SetWeight(){
		currentTotalWeight = bodyWeight + carriedCoinWeight;
		rb.mass = currentTotalWeight;
	}
}
