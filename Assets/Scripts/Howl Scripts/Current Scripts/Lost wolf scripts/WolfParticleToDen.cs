using UnityEngine;
using System.Collections;

public class WolfParticleToDen : MonoBehaviour {

	GameObject den;
	GameObject particleToDen;

	// Use this for initialization
	void Start () {
		particleToDen = GameObject.Find("Particle To Den");
		den = GameObject.Find("Wolf Den");
	}

	
	// Update is called once per frame
	void Update () {
		transform.LookAt (den.transform);

	}
}
