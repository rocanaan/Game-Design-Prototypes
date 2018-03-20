using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] pickupCollection;
	public int currentScene;
	public int nextScene;
	public int numberOfScenes;
	private int currentPriority;

	// Use this for initialization
	void Start () {
		print ("Initializing game controller");
		for (int i = 0; i < pickupCollection.Length; i++) {
			PickupController pc = pickupCollection [i].GetComponent<PickupController> ();
			pc.setPriority (i);
			if (i == 0) {
				pc.setActive ();
				print ("Setting active");
			} else {
				pc.setInactive ();
				print ("Setting inactive");
			}
		}

		currentPriority = 0;
		
	}

	public void incrementPriority(int p){
		if (p != currentPriority) {
			print ("Error! Current priority does not match the destroyed pickup");
		} else {
			currentPriority++;
			if (currentPriority == pickupCollection.Length){
//				SceneManager.LoadScene (SceneManager.GetSceneByBuildIndex( (currentScene+1)%numberOfScenes).name );
				SceneManager.LoadScene (nextScene,LoadSceneMode.Single);
			}
			else{
				PickupController pc = pickupCollection [currentPriority].GetComponent<PickupController> ();
				pc.setActive();
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
