using UnityEngine;
using System.Collections;

public class WarmthObject : MonoBehaviour {

	[Range (0,2)]
	public int WorldType;
	/*
	  0 = Real World 
	  1 = Spirit World
	  2 = Both Worlds
	*/

	public float warmthBoundaries = 5.0f;
	public CircleCollider2D warmthCollider;

	//TimePlayerEnteredWarmth, TimeInverval

	// Use this for initialization
	void Start () {
		warmthCollider = this.gameObject.AddComponent<CircleCollider2D> ();
		warmthCollider.radius = warmthBoundaries;
		warmthCollider.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			coll.gameObject.GetComponent<PCWolfInput>().WarmingUp = true;
			coll.gameObject.GetComponent<PCWolfInput>().WolfEnterTime = Time.time;
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			coll.gameObject.GetComponent<PCWolfInput>().WarmingUp = false;
		}
	}
}
