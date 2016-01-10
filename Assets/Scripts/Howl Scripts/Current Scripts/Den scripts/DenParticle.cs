using UnityEngine;
using System.Collections;

public class DenParticle : MonoBehaviour {

	//GameObject den;
	GameObject DenParticleGO;
	public ParticleSystem DenParticleSystem;

	int particleRotSpeed;

	// Use this for initialization
	void Start () {
		//particleToDen = GameObject.Find("Particle To Den");
		//den = GameObject.Find("Wolf Den");
		DenParticleGO = this.gameObject;
		DenParticleSystem = this.gameObject.GetComponent<ParticleSystem> ();
		particleRotSpeed = 1;


	}

	
	// Update is called once per frame
	void Update () {
		//I want to rotate z axis for spinning effect**
		//transform.LookAt (den.transform);

		//DenParticleGO.transform.Rotate (0, 0, 1, Time.deltaTime);

		//DenParticleGO.transform.rotation.z += Vector3.left * Time.deltaTime;
			//transform.position += Vector3.left * speed * Time.deltaTime;
		//transform.Rotate (Vector3.up Time.deltaTime 100, Space.World);

	}
}
