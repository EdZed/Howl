﻿using UnityEngine;
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
			if( Application.loadedLevelName == "Tutorial PS Demo"){
			Application.LoadLevel("Tutorial 2 PS Demo");
			}
			if(Application.loadedLevelName == "Tutorial 2 PS Demo"){
				Application.LoadLevel("Howl PS Demo");
			}
			
		} 
	}//end ontrigger
}
