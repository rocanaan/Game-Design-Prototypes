using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelInitializer : MonoBehaviour {
	public GameObject[] model_shapes;
	public Material model_material;
	public int min_objects;
	public int max_objects;
	private int num_objects;
	public float relative_offset;
	//private List<GameObject> model_objects;
	private List<GameObject> modelObjects;

	public float minScale;
	public float maxScale;

	public int maxConnections;
	private List<int> connections;

	// Use this for initialization
	void Start () {

		if (maxConnections >0){
			connections = new List<int>();
		}


		modelObjects = new List<GameObject>();
		num_objects = Random.Range (min_objects, max_objects);

		Vector3 next_center = new Vector3 (2.0f, 0.0f, 0.0f);

		int next_object_index = Random.Range(0, model_shapes.Length);

		if (maxConnections > 0) {
			connections.Add (0);
		}

			


		GameObject new_object = Instantiate (model_shapes [next_object_index], next_center, model_shapes [next_object_index].transform.rotation);

		new_object.transform.localScale = new_object.transform.localScale * Random.Range (minScale, maxScale);
		//new_object.transform.Rotate(0,0,Random.Range(0.0f,360.0f));

		new_object.transform.RotateAround (new_object.transform.position, Vector3.forward, Random.Range (0.0f, 360.0f));

		modelObjects.Add (new_object);

		for (int i = 0; i < num_objects-1; i++) {
			next_object_index = Random.Range(0, model_shapes.Length);

			int next_pivot = Random.Range (0, modelObjects.Count);
			while (maxConnections >0 && connections [next_pivot] > maxConnections) {
				next_pivot = Random.Range (0, modelObjects.Count);
			}

			next_center = modelObjects [next_pivot].transform.position;

			if (maxConnections > 0) {
				connections [next_pivot] += 1;
				connections.Add (1);
			}

			//next_center = object_positions [Random.Range (0, object_positions.Count-1)];

			float scale = Random.Range (minScale, maxScale);

			float theta = Random.Range (0, 2 * Mathf.PI);
			float delta_x = scale*relative_offset*Mathf.Cos (theta);
			float delta_y = scale*relative_offset*Mathf.Sin (theta);

			//Vector2 offset = Random.insideUnitCircle;


			next_center = next_center + new Vector3 (delta_x,delta_y);

			new_object = Instantiate (model_shapes [next_object_index], next_center,  model_shapes [next_object_index].transform.rotation);
			new_object.transform.localScale = new_object.transform.localScale * scale;
			//new_object.transform.Rotate(0,0,Random.Range(0.0f,360.0f));
			new_object.transform.RotateAround (new_object.transform.position, Vector3.forward, Random.Range (0.0f, 360.0f));
			modelObjects.Add (new_object);
		}


	}

	
	// Update is called once per frame
	void Update () {
		
	}

	public void modelReset(){
		print ("Trying to reset model");
		foreach (GameObject obj in modelObjects){
			Destroy (obj);
		}
		Start ();
		print ("Reinitializing Model");
	}
}
