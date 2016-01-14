using UnityEngine;
using System.Collections;

public class WarmthObject : MonoBehaviour {

//	[Range (0,2)]
//	public int WorldType;
	/*
	  0 = Real World 
	  1 = Spirit World
	  2 = Both Worlds
	*/

	//public float warmthBoundaries = 5.0f;
	public CircleCollider2D warmthCollider;
	public playerWarmth playerWarmthScript;
	public WorldManager worldManagerScript;
	public DynamicLight dynamicLightScript;

	public MeshRenderer warmthRend;

	//TimePlayerEnteredWarmth, TimeInverval

	// Use this for initialization
	void Start () {
		warmthCollider = this.gameObject.AddComponent<CircleCollider2D> ();
		//warmthCollider.radius = warmthBoundaries;
		dynamicLightScript = this.gameObject.GetComponent<DynamicLight> ();
		warmthCollider.radius = dynamicLightScript.lightRadius;
		warmthCollider.isTrigger = true;
		playerWarmthScript = GameObject.Find ("playerWolf").transform.Find ("playerWarmth").GetComponent<playerWarmth> ();
		worldManagerScript = GameObject.Find ("WorldManager").GetComponent<WorldManager> ();

		if (worldManagerScript.WorldType == 0) {
			//do what here?
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	//called by DynamicLight script
	public void WarmthRendOff(){
		
		warmthRend = GetComponent<MeshRenderer> ();
		warmthRend.enabled = false;
		//warmthCollider.tag = 
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
//			coll.gameObject.GetComponent<PCWolfInput>().WarmingUp = true;
//			coll.gameObject.GetComponent<PCWolfInput>().WolfEnterTime = Time.time;
			playerWarmthScript.InvokeRepeating("GetWarm",2,2);
			playerWarmthScript.CancelInvoke("GetCold");
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			//coll.gameObject.GetComponent<PCWolfInput>().WarmingUp = false;
		}
	}
}
