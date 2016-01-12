using UnityEngine;
using System.Collections;

public class WolfParticleRadar : MonoBehaviour {

	public GameObject den;
	public GameObject particleRadarGO;
	public ParticleSystem radarParticleSystem;

	// Use this for initialization
	void Start () {
		particleRadarGO = this.gameObject;
		den = GameObject.Find("Wolf Den");
		radarParticleSystem = this.gameObject.GetComponent<ParticleSystem> ();
		radarParticleSystem.enableEmission = false;
	}

	
	// Update is called once per frame
	void Update () {
		//transform.LookAt (den.transform);

	}

	public IEnumerator ParticleRadarLookAt(GameObject target){
		//turn on spirit radar
		radarParticleSystem.enableEmission = true;

		//Spirit points at target
		transform.LookAt (target.transform);
		yield return new WaitForSeconds(4);
		//turn off spirit radar
		radarParticleSystem.enableEmission = false;
	}
}
