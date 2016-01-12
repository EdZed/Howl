using UnityEngine;
using System.Collections;

public class HearDen : MonoBehaviour {

	AudioSource hearDenHowlAudio;
	public Animator DenAnim;
	public WorldManager WorldManagerScript;
	public GameObject parentDen;
	public WolfParticleRadar WolfParticleRadarScript;
		//= GameObject.Find ("Particle Radar").GetComponent<WolfParticleRadar> ();

	// Use this for initialization
	void Start () {
		hearDenHowlAudio = GetComponent<AudioSource> ();
		parentDen = this.gameObject.transform.parent.gameObject;
		WolfParticleRadarScript = GameObject.Find ("Particle Radar").GetComponent<WolfParticleRadar> ();
		//hearDenHowlAudio.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "HowlAttract"){
			StartCoroutine("DenHowlTimer");

			//call method from particle radar that passes in this gameobject.
			//WolfParticleRadarScript.ParticleRadarLookAt(parentDen);
			WolfParticleRadarScript.StartCoroutine("ParticleRadarLookAt", parentDen);
			//DenHowlTimer();
			Debug.Log ("Den is howling");
		}


	}

	IEnumerator DenHowlTimer(){
		if (WorldManagerScript.isWorldTransitioning == false) {

			yield return new WaitForSeconds (2);

			hearDenHowlAudio.Play ();
			DenAnim.SetInteger ("DenAnimState", 1);
			Debug.Log ("audio enable switching");

			yield return new WaitForSeconds (5);

			//hearDenHowlAudio.enabled = false;
			hearDenHowlAudio.Stop ();
			DenAnim.SetInteger ("DenAnimState", 0);
		}
	}

}
