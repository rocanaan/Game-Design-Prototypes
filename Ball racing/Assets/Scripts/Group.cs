using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour {

	private float lastMove;
	private float lastFlip;
	private float lastTick;
	private float moveInterval = 0.3f;
	private float flipInterval = 0.3f;
	private float tickInterval = 1.0f;



	// Use this for initialization
	void Start () {
		lastMove = 0.0f;
		lastFlip = 0.0f;
		lastTick = 0.0f;

		// Default position not valid? Then it's game over
		if (!isValidGridPos()) {
			Debug.Log("GAME OVER");
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Move Left

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
	 
		if (moveHorizontal < 0 && Time.time > lastMove + moveInterval ) {
			// Modify position
			transform.position += new Vector3(-1, 0, 0);

			// See if valid
			if (isValidGridPos()){
				// Its valid. Update grid.
				updateGrid();
				lastMove = Time.time;
			}
			else
				// Its not valid. revert.
				transform.position += new Vector3(1, 0, 0);
		}

		// Move Right
		else if (moveHorizontal > 0 && Time.time > lastMove + moveInterval) {
			// Modify position
			transform.position += new Vector3(1, 0, 0);

			// See if valid
			if (isValidGridPos ()) {
				// It's valid. Update grid.
				updateGrid ();
				lastMove = Time.time;
			}
			else
				// It's not valid. revert.
				transform.position += new Vector3(-1, 0, 0);
		}

		// Rotate
		else if (Input.GetButton("Fire1") && Time.time > lastFlip + flipInterval ) {
			transform.Rotate(0, 0, -90);

			// See if valid
			if (isValidGridPos ()) {
				// It's valid. Update grid.
				updateGrid ();
				lastFlip = Time.time;
			}
			else
				// It's not valid. revert.
				transform.Rotate(0, 0, 90);
		}

		else if (Input.GetButton("Fire2") && Time.time > lastFlip + flipInterval ) {
			transform.Rotate(0, 0, 90);

			// See if valid
			if (isValidGridPos ()) {
				// It's valid. Update grid.
				updateGrid ();
				lastFlip = Time.time;
			}
			else
				// It's not valid. revert.
				transform.Rotate(0, 0, -90);
		}

		// Fall by pressing down
		else if (moveVertical < 0 && Time.time > lastMove + moveInterval ) {
				groupFall ();
				lastMove = Time.time;
				lastTick = Time.time;
		}


		else if ( Time.time > lastTick + tickInterval ) {
			groupFall ();
			lastTick = Time.time;
		}

		else if (moveVertical > 0 && Time.time > lastMove + moveInterval ) {
			bool cont = true;
			while (cont) {
				cont = groupFall ();
				Debug.Log ("Falling");
			}
			Debug.Log("On the ground");
			lastMove = Time.time;
			lastTick = Time.time;
		}
}
		



	bool groupFall(){
		// Modify position
		transform.position += new Vector3(0, -1, 0);

		// See if valid
		if (isValidGridPos ()) {
			// It's valid. Update grid.
			updateGrid ();
			return true;
		} else {
			// It's not valid. revert.
			transform.position += new Vector3 (0, 1, 0);

			// Clear filled horizontal lines
			Grid.deleteFullRows ();

			// Spawn next Group
			FindObjectOfType<Spawner> ().SpawnNext ();

			//FindGameObjectWithTag ("Spawner").SpawnNext ();

			// Disable script
			enabled = false;
			return false;
		}
	}
		

	bool isValidGridPos() {        
		foreach (Transform child in transform) {
			Vector2 v = Grid.roundVec2(child.position);

			// Not inside Border?
			if (!Grid.insideBorder(v))
				return false;

			// Block in grid cell (and not part of same group)?
			if (Grid.grid[(int)v.x, (int)v.y] != null &&
				Grid.grid[(int)v.x, (int)v.y].parent != transform)
				return false;
		}
		return true;
	}

	void updateGrid() {
		// Remove old children from grid
		for (int y = 0; y < Grid.h; ++y)
			for (int x = 0; x < Grid.w; ++x)
				if (Grid.grid[x, y] != null)
				if (Grid.grid[x, y].parent == transform)
					Grid.grid[x, y] = null;

		// Add new children to grid
		foreach (Transform child in transform) {
			Vector2 v = Grid.roundVec2(child.position);
			Grid.grid[(int)v.x, (int)v.y] = child;
		}        
	}
}
