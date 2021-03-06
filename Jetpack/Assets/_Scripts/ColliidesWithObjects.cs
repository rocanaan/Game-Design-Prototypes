﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliidesWithObjects : MonoBehaviour {

	private PlayerController playerController;
	GameController gc;

	void Start(){
		playerController = GetComponent<PlayerController> ();
		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "FuelContainer") {
			playerController.Refuel ();
			Destroy (other.gameObject);
		}

		if (other.tag == "Coin") {
			playerController.PickupCoin (other.GetComponent<CoinAttributes>());
			Destroy (other.gameObject);
		}

		if (other.tag == "Threat") {
			Destroy (gameObject);
			gc.setGameover ();
		}
	}
}
