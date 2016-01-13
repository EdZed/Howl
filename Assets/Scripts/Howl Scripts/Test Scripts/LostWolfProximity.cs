using UnityEngine;
using System.Collections;

public class LostWolfProximity : MonoBehaviour {

	public GameObject parentLostWolf;
	public AudioSource lostWolfHowl;

	public WolfParticleRadar WolfParticleRadarScript;
	public bool isLostHowling;

	// Use this for initialization
	void Start () {
		parentLostWolf = this.gameObject.transform.parent.gameObject;
		lostWolfHowl = parentLostWolf.GetComponent<AudioSource> ();
		WolfParticleRadarScript = GameObject.Find ("Particle Radar").GetComponent<WolfParticleRadar> ();
		isLostHowling = false;

		//GameObject.Find("playerWolf"); Find ("Lost Wolf Green").gameObject
		//anim = parentLostWolf.GetComponent<Animator> ();
		//anim = gameObject.GetComponentInParent<Animator> ();
		//anim.SetTrigger("stand");
		//anim.SetInteger ("AnimState", 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "HowlAttract") 
		{
			//have lost wolf howl
			//anim.SetInteger ("AnimState", 3);
			StartCoroutine("HiddenWolfHowl");
			Debug.Log ("howl coroutine called");

			WolfParticleRadarScript.StartCoroutine("ParticleRadarLookAt", parentLostWolf);


			//anim.SetInteger ("AnimState", 5);
			//if(attacking)
			//{
			//var explode = target.GetComponent<Explode>() as Explode;
			//explode.OnExplode();
			//Destroy(target.gameObject); 
			//Destroy(target.transform.parent.gameObject); 
			
			 
			
		} 	
	} // end ontriggerEnter

	IEnumerator HiddenWolfHowl(){

		Debug.Log ("HiddenWolfHowl");
		yield return new WaitForSeconds(1);

		//lostWolfHowl.enabled = true;
		if (!isLostHowling) {
			lostWolfHowl.Play ();
			Debug.Log ("howl should be heard");
			isLostHowling = true;
		}

		yield return new WaitForSeconds(3);
		isLostHowling = false;
		//lostWolfHowl.enabled = false;
	}


}
