using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClick : MonoBehaviour {

	public GameObject playerControllerObject;
	private PlayerController playerController;
	public GameObject shape;

	void Start (){
		playerController = playerControllerObject.GetComponent<PlayerController>();
	}

	void OnMouseDown(){
		playerController.New_Shape (shape);
	}



}
