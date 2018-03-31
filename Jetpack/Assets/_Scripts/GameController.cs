using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour {


	public GameObject playerObject;
	public Camera gameCamera;
	public float startingSpeed;

	private bool gameOver;

	public Text gameOverText;


	// Use this for initialization
	void Start () {
		gameOver = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameOver) {
			playerObject.GetComponent<Rigidbody2D> ().velocity = new Vector3 (
				getCurrentSpeed (),
				playerObject.GetComponent<Rigidbody2D> ().velocity.y
			);
			gameCamera.transform.position = new Vector3 (
				playerObject.transform.position.x + 4,
				gameCamera.transform.position.y,
				gameCamera.transform.position.z
			);
		}

		if (gameOver) {
			if (Input.GetButton ("Submit")) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}
	}

	private float getCurrentSpeed(){
		return startingSpeed;
	}

	public void setGameover (){
		gameOver = true;
		gameOverText.text = "Game Over! Press ENTER to restart";
	}

	public bool isGameOver(){
		return gameOver;
	}
}
