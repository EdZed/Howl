using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {

	public RockPiece rockPiece;
	public int totalParts = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "RockDisappear"||
		    target.gameObject.tag == "Movable") {
			OnExplode();
		}
	}

	void OnExplode(){
		//Destroy (gameObject);

		var t = transform;

		for (int i = 0; i< totalParts; i++) {
			t.TransformPoint(0, -100,0);
			RockPiece clone = Instantiate(rockPiece, t.position, Quaternion.identity) as RockPiece;
			clone.GetComponent<Rigidbody2D>().AddForce(Vector3.right * Random.Range (-50, 50));
			clone.GetComponent<Rigidbody2D>().AddForce(Vector3.up * Random.Range (100, 400));
		}
	}
}
