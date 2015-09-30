using UnityEngine;
using System.Collections;

public class WolfParticleToDen : MonoBehaviour {

	GameObject den;
	GameObject particleToDen;

	// Use this for initialization
	void Start () {
		particleToDen = GameObject.Find("Particle To Den");
		den = GameObject.Find("WolfDen");
	}

	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Euler( Mathf.Atan2(den.transform.position.y - transform.position.y, den.transform.position.x - transform.position.x) * Mathf.Rad2Deg,270, 270);
	}
}
