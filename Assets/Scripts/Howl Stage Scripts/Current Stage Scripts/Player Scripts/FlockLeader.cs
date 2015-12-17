using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlockLeader : MonoBehaviour {

	List<FlockMember> flockScripts;



	// Use this for initialization
	void Start () {
		FlockMember[] flockScriptsArray = FindObjectsOfType (typeof(FlockMember)) as FlockMember[];
		flockScripts = new List<FlockMember> ();
		flockScripts.AddRange (flockScriptsArray);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
