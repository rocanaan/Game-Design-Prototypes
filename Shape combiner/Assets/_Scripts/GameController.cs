using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject modelSpawner;
	private ModelInitializer modelInitializer;

	public GameObject playerControllerObject;
	private GenerateColliders gc;

	public int gameLengthIntervals;
	public GameObject timeCounter;
	public int intervalLength;

	private int nextInterval;
	private bool gameOver;
	private float startOfCurrentGame;

	private List<GameObject> timerCounters;


	// Use this for initialization
	void Start () {
		startOfCurrentGame = 0.0f;
		startTimer ();
		nextInterval = 1;
		gameOver = false;

		modelInitializer = modelSpawner.GetComponent<ModelInitializer> ();
		gc = playerControllerObject.GetComponent<GenerateColliders> ();
	}
	
	// Update is called once per frame
	void Update () {
		if ( (Time.time - startOfCurrentGame) > (nextInterval * intervalLength) && !gameOver ) {
			GameObject counter = timerCounters [gameLengthIntervals - nextInterval];
			Destroy (counter);

			if (nextInterval >= gameLengthIntervals){
				setGameOver();
			}

			nextInterval++;;


		}
	}

	void startTimer(){
		timerCounters = new List<GameObject> ();
		float screenOffset = 1.0f;

		for (int i = 0; i < gameLengthIntervals; i++) {
			Vector3 timerPosition = new Vector3 (-2.5f + (screenOffset * (i%10)), -3.8f -(0.8f)*(i/10) , 0.0f);
			GameObject counter = Instantiate<GameObject>(timeCounter, timerPosition, Quaternion.identity);
			timerCounters.Add (counter);
			print (i);
		}
			
	}

	public void setGameOver (){
		gameOver = true;
	}

	public bool isGameOver(){
		return gameOver;
	}

	public void restart(){
		print ("Restarting");
		gameOver = false;

		modelInitializer.modelReset ();
		gc.resetTestColliders ();

		startTimer ();
		print ("restarted timer");
		nextInterval = 1;
		startOfCurrentGame = Time.time;
	}

}
