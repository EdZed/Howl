using UnityEngine;
using System.Collections;

public class StageExit : MonoBehaviour {

	public AudioSource sfx;
	public bool currPlaying;
	public ParticleSystem exitParticleSys;

	// Use this for initialization
	void Start () {
		sfx = this.gameObject.GetComponent<AudioSource> ();
		currPlaying = false;
		exitParticleSys = this.gameObject.transform.Find ("ExitParticle").gameObject.GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			if(currPlaying == false){
			StartCoroutine ("ExitCollide");
			Debug.Log ("Player colliding w Exit");
			}
		}
		
	}


	IEnumerator ExitCollide(){
		currPlaying = true;
		exitParticleSys.startSize = 15;
		sfx.Play ();
		Debug.Log ("Exit sfx should be playing");
		yield return new WaitForSeconds (2);
		exitParticleSys.startSize = 8.2f;
		Debug.Log ("Exit particle should be normal again");
		currPlaying = false;
		sfx.Stop ();
	}

	IEnumerator ExitOpenSfx(){
		sfx.Play ();
		//Debug.Log ("Exit sfx should be playing");
		yield return new WaitForSeconds (1);
		sfx.volume -= .025f;
		yield return new WaitForSeconds (.8f);
		sfx.volume -= .025f;
		yield return new WaitForSeconds (.8f);
		sfx.volume -= .025f;
		yield return new WaitForSeconds (.8f);
		sfx.Stop ();
	}
}
