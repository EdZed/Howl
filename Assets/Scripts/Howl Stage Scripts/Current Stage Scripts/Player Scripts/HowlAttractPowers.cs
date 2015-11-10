using UnityEngine;
using System.Collections;

public class HowlAttractPowers : MonoBehaviour {
	GameObject PlayerWolfGO;

	// Use this for initialization
	void Start () {
	
		PlayerWolfGO = GameObject.Find ("playerWolf");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "LostWolfOrange") {
			PlayerWolfGO.GetComponent<PCWolfInput>().runAtk = true;

		} 
	}//end ontrigger
}
