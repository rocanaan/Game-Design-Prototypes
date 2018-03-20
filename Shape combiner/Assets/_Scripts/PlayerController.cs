using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	private GameObject current_shape;
	public GameObject gameControllerObject;
	private GameController gc;
	private int state;
	private float last_click_time;
	public Vector3 start;
	public Material material;

	private List<GameObject> playerObjects;



	public void New_Shape (GameObject shape){
		if (state == 0 && !gc.isGameOver()) {
			current_shape = Instantiate (shape, start, shape.transform.rotation);
			current_shape.GetComponent<Renderer> ().material = material;
			playerObjects.Add (current_shape);
			state = 1;
			last_click_time = Time.time;
		}
	}

	// Use this for initialization
	void Start () {
		state = 0;

		playerObjects = new List<GameObject> ();

		gc = gameControllerObject.GetComponent<GameController> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (state == 1 && !gc.isGameOver()) {

			float scaleDirection =  Input.GetAxis ("Vertical");
			float rotationDirection = (-1) * Input.GetAxis ("Horizontal");

			if (rotationDirection == 0.0f) {
				rotationDirection = 3 * Input.GetAxis ("Mouse ScrollWheel");
			}

			current_shape.transform.localScale = current_shape.transform.localScale * (1.0f + 0.05f*scaleDirection) ;
			//current_shape.transform.Rotate (0, 0, 3 * rotationDirection);

			current_shape.transform.RotateAround (transform.position, Vector3.forward, 3 * rotationDirection);

			Vector3 mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);
			current_shape.transform.SetPositionAndRotation ( new Vector3(mousePosition.x,mousePosition.y,-1), current_shape.transform.rotation);

			if (Input.GetMouseButtonDown(0) && Time.time > last_click_time + 0.1f) {
				current_shape = null;
				state = 0;
				last_click_time = Time.time;
			}

			if (Input.GetButton ("Cancel") || Input.GetMouseButton(1)) {
				Destroy (current_shape);
				current_shape = null;
				state = 0;
			}



		}

		if (gc.isGameOver() && Input.GetButton ("Restart")) {
			foreach (GameObject obj in playerObjects) {
				Destroy (obj);
			}
			state = 0;
			playerObjects = new List<GameObject> ();
			gc.restart ();
		}
		
	}	
		
}
