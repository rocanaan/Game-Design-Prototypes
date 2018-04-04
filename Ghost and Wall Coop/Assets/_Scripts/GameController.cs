﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// TODO: Make this class responsible for displaying the score and managing respanws;


public class GameController : MonoBehaviour {

	public enum GameMode{
		Survival,
		//Deathmatch,
		KOTH,
		Tutorial,
		Boss,
	};

	private int totalDeaths;

	public GameMode gameMode;

	private struct RespawnEvent
	{
		public GameObject player;
		public float time;
	}

//	public GameObject[] startingPositions;
//	private int countPlayersInTarget;

	public int respawnTime;

	public GameObject[] players;

	private int[] numPlayersAliveByTeam;

	public Text gameOverText;

	private static bool gameOver;

	private int currentKeyboardInput;

	public float max_x;
	public float max_y;
	public float respawnDistanceFromCenter;

	public int numTargets;
	private int targetsDestroyed;

	//private Queue<RespawnEvent> respawns;

	// Use this for initialization
	void Start () {
		gameOver = false;

		numPlayersAliveByTeam = new int[players.Length+1];

		for (int i=0; i< numPlayersAliveByTeam.Length; i++){
			numPlayersAliveByTeam[i] = 0;
		}

		foreach (GameObject player in players){
			PlayerController pc = player.GetComponent<PlayerController> ();
			numPlayersAliveByTeam [pc.teamID] += 1;
		}

		for (int i=0; i< numPlayersAliveByTeam.Length; i++) {
			print ("Team " + i + " has number of players " + numPlayersAliveByTeam [i]);
		}

		targetsDestroyed = 0;

		totalDeaths = 0;

		//respawns = new Queue<RespawnEvent> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (gameOver && Input.GetButton("Submit")){
			if (gameMode == GameMode.Tutorial) {
				SceneManager.LoadScene (1,LoadSceneMode.Single);
			} else {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}

		// If 1, 2, 3 or 4 is pressed, set currentKeyboardInput to that player ID
		setCurrentKeyboardInput ();

//		if (respawns.Count != 0) {
//			RespawnEvent nextRespawn = respawns.Peek ();
//			if (Time.time >= nextRespawn.time) {
//			}
//		}
	}

	public void notifyDeath(int playerID, int teamID){
		numPlayersAliveByTeam [teamID] -= 1;
		totalDeaths++;
		if (numPlayersAliveByTeam [teamID] <= 0 && gameMode == GameMode.Survival) {
			if (teamID == 1) {
				setGameOver (2);
			} else if (teamID == 2) {
				setGameOver (1);
			}

		}
	}

	public int getRespawnTime (){
		if (gameMode == GameMode.Survival) {
			return -1;
		} else {
			return respawnTime;
		}
	}

//	public void notifyTutorial(){
//		countPlayersInTarget--;
//		if (countPlayersInTarget == 0) {
//			print ("tutorial ended - game controller");
//			foreach (GameObject obj in startingPositions) {
//				obj.SendMessageUpwards ("endTutorial");
//				print ("Tutorial Ended - Sending message");
//			}
//		}
//	}

	public static bool isGameOver(){
		return gameOver;
	}

	public void hillCaptured(int teamID){
		if (gameMode == GameMode.KOTH){
			setGameOver (teamID);
		}
	}

	void setGameOver(int winner){
		gameOver = true;
		if (gameMode == GameMode.Tutorial) {
			gameOverText.text = "End of tutorial. Press SQUARE or ENTER to battle!!!";
		}
		else if (gameMode == GameMode.Boss){
			gameOverText.text = "Congratulations!!! Your team defeated the boss losing a total of " + totalDeaths + " lives!! Press SQUARE or ENTER to restart.";
		}
		else {
			if (winner == 1) {
				gameOverText.text = "Blue team wins!! Press SQUARE or ENTER to restart.";
			}
			if (winner == 2) {
				gameOverText.text = "Red team wins!! Press SQUARE or ENTER to restart.";
			}
		}
	}

	void setCurrentKeyboardInput (){
		for (int i = 1; i < players.Length +1; i++) {
			if (Input.GetButton ("Toggle" + i)) {
				currentKeyboardInput = i;
			}
		}
	}

	public int getCurrentKeyboardInput(){
		return currentKeyboardInput;
	}

	// Returns a valid respawn position. Right now is implemented as simply the x or y position being distant from the center
	public Vector2 getRespawnPosition(){
		while (true) {
			Vector2 randomVector = new Vector3 (Random.Range (-max_x, max_x), Random.Range (-max_y, max_y));
			if (Mathf.Abs(randomVector.x) >= respawnDistanceFromCenter || Mathf.Abs(randomVector.y) >= respawnDistanceFromCenter) {
				return randomVector;
			}
		}
	}

	public void bossDied()
	{
		if (gameMode == GameMode.Boss) {
			setGameOver (1);
		}
	}

	public void notifyTargetDestroyed (){
		if (gameMode == GameMode.Tutorial) {
			targetsDestroyed++;
			if (targetsDestroyed >= numTargets) {
				setGameOver (0);
			}
		}
	}
}
