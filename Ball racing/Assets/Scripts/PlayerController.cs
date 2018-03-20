using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float acceleration;
	public float speed;
	public bool useForces;
//	public Text countText;
//	public Text timer;
//	public Text winText;

	private Rigidbody2D rb;
//	private int count;
//	private float minutes;
//	private float seconds;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
//		count = 0;
//		SetCountText();
//		winText.text = "";
	}

	void Update ()
	{
		//Esta parte deveria estar em uma função própria
//		string minutes = Mathf.Floor(Time.time / 60).ToString("00");
//		string seconds = (Time.time % 60).ToString("00"); //Provavelmente deveria usar um único Time.time para evitar inconsistência
//		timer.text = minutes + ":" + seconds;
	}


	void FixedUpdate()
	{

		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		if (useForces) {
			rb.AddForce (movement * acceleration);
		} else {
			rb.velocity = movement * speed;
		}

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Enemy")) {
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
	}


	void SetCountText()
	{
//		countText.text = "Count = " + count.ToString ();
//		if (count >= 12)
//		{
//			winText.text = "You Win!!!\n" +
//				"Time = " + timer.text + "\n" +
//				"You collected "  + count.ToString() + " Objects!";
//		}
	}

}
