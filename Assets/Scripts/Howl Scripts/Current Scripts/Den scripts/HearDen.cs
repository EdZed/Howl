using UnityEngine;
using System.Collections;

public class HearDen : MonoBehaviour {

	AudioSource hearDenHowl;
	public Animator DenAnim;

	// Use this for initialization
	void Start () {
		hearDenHowl = GetComponent<AudioSource> ();
		hearDenHowl.enabled = false;
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
		yield return new WaitForSeconds(3);
		hearDenHowl.enabled = true;
		DenAnim.SetInteger ("DenAnimState", 1);
		Debug.Log ("audio enable switching");
		yield return new WaitForSeconds(5);
		hearDenHowl.enabled = false;
		DenAnim.SetInteger ("DenAnimState", 0);
	}

}
