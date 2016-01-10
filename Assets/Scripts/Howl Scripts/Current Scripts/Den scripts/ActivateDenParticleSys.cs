using UnityEngine;
using System.Collections;

public class ActivateDenParticleSys : MonoBehaviour {
	public CircleCollider2D activateDenPartSysCol;

	public GameObject DenParticleSysGO;
	public ParticleSystem DenParticleSys;
	public ParticleSystemRenderer DenParticleSysRend;

	// Use this for initialization
	void Start () {

		activateDenPartSysCol = this.gameObject.GetComponent<CircleCollider2D> ();

		DenParticleSysGO = this.gameObject.transform.parent.gameObject;
			//Find ("WolfDen Particle Sys").gameObject;
		DenParticleSys = DenParticleSysGO.GetComponent<ParticleSystem> ();
		DenParticleSysRend = DenParticleSysGO.GetComponent<ParticleSystemRenderer> ();
		DenParticleSys.enableEmission = false;
		DenParticleSysRend.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "HowlAttract") {
			//turn on particle system
			DenParticleSys.enableEmission= true;
			DenParticleSysRend.enabled = true;
			//turn off collider
			activateDenPartSysCol.enabled = false;
		}

	}

}
