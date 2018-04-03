using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

	public int maxHealth;
	private int currentHealth;

	private int difficulty;

	public int teamID;

	public GameObject healthBar;

	public Material difficulty1Material;
	public Material difficulty2Material;

	BlinkDamageAnimation coreBlinkAnimation;

	// Use this for initialization
	void Start () {
		coreBlinkAnimation = GetComponentInChildren<BlinkDamageAnimation> ();
		currentHealth = maxHealth;
		difficulty = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Shot") {
			ShotAttributes shot = col.GetComponent<ShotAttributes> ();
			if (shot.getTeamID() != teamID) {
				takeDamage (shot.damage);
				Destroy (col.gameObject);
			}
		}
	}

	void takeDamage (int damage){
		currentHealth -= damage;
		print ("Boss Health is" + currentHealth);
		//print (" Player " + playerID + " current Health is " + currentHealth);
		coreBlinkAnimation.startAnimation ();
		//healthTracker.setHealth (currentHealth);


		float healthRatio = (float)currentHealth / (float)maxHealth;

		RescaleHealthBar (healthRatio);

		if (difficulty == 0 && healthRatio < 2.0/3.0) {
			difficulty = 1;
			healthBar.GetComponent<Renderer> ().material = difficulty1Material;
			GetComponentsInChildren<Renderer> () [1].material = difficulty1Material;

			GetComponentInChildren<ShieldSpawner> ().setStatus (true);
		}

		if (difficulty == 1 && healthRatio < 1.0 / 3.0) {
			difficulty = 2;
			healthBar.GetComponent<Renderer> ().material = difficulty2Material;
			GetComponentsInChildren<Renderer> () [1].material = difficulty2Material;

			GetComponent<FollowBehavior> ().setStatus(true);
			GetComponent<WanderBehavior> ().setActive (false);

			GetComponentInChildren<ShieldSpawner> ().setStatus (false);
		}
			
		if (currentHealth <= 0) {
			//playerDeath ();
		}
	}

	private void RescaleHealthBar(float healthRatio){
		//float healthRatio = (float)currentHealth / (float)maxHealth;

		Vector3 scale = healthBar.transform.localScale;
		print (scale);
		scale.x = healthRatio;
		healthBar.transform.localScale = scale;

		Vector3 position = healthBar.transform.localPosition;
		position.x = (healthRatio - 1) / 2.0f;
		healthBar.transform.localPosition = position;
	}
}
