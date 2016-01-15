using UnityEngine;
using System.Collections;

public class TutorialTransitionToScene : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Player") {
			if( target.gameObject.GetComponent<PCWolfInput>().currLevel == "Howl Stage 1"){
				Application.LoadLevel("Howl Stage 2");
			}
			if( target.gameObject.GetComponent<PCWolfInput>().currLevel == "Howl Stage 2"){
				Application.LoadLevel("Howl Stage 3");
			}
			
		} 
	}//end ontrigger
}
