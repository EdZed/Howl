using UnityEngine;
using System.Collections;

public class FallingRocksTrigger : MonoBehaviour {

	public GameObject[] rockSpawnPos = new GameObject[4];
	public GameObject[] fallingRocks = new GameObject[4];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "HowlAttract") {
			//spawn 1st rock at desired position
			fallingRocks [0] = Instantiate (Resources.Load ("Falling Rock Trigger")) as GameObject;
			//makes prefab a child of script's parent prefab
			fallingRocks [0].transform.parent = transform;
			fallingRocks [0].transform.position = rockSpawnPos [0].transform.position;
			
			fallingRocks [1] = Instantiate (Resources.Load ("Falling Rock Trigger")) as GameObject;
			fallingRocks [0].transform.parent = transform;
			fallingRocks [1].transform.position = rockSpawnPos [1].transform.position;
			
			fallingRocks [2] = Instantiate (Resources.Load ("Falling Rock Trigger")) as GameObject;
			fallingRocks [0].transform.parent = transform;
			fallingRocks [2].transform.position = rockSpawnPos [2].transform.position;
			
			fallingRocks [3] = Instantiate (Resources.Load ("Falling Rock Trigger")) as GameObject;
			fallingRocks [0].transform.parent = transform;
			fallingRocks [3].transform.position = rockSpawnPos [3].transform.position;
			
		} 
	}
}
