using UnityEngine;
using System.Collections;

public class HearDen : MonoBehaviour {

	AudioSource hearDenHowlAudio;
	public Animator DenAnim;
	public WorldManager WorldManagerScript;

	// Use this for initialization
	void Start () {
		hearDenHowlAudio = GetComponent<AudioSource> ();
		//hearDenHowlAudio.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "HowlAttract"){
			StartCoroutine("DenHowlTimer");
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
