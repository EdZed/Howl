using UnityEngine;
using System.Collections;

public class LostWolfProximity : MonoBehaviour {

	public GameObject parentLostWolf;
	public AudioSource lostWolfHowl;
	//private Animator anim;


	// Use this for initialization
	void Start () {
		parentLostWolf = this.gameObject.transform.parent.gameObject;
		lostWolfHowl = parentLostWolf.GetComponent<AudioSource> ();
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
		yield return new WaitForSeconds(2);

		//lostWolfHowl.enabled = true;
		lostWolfHowl.Play ();
		Debug.Log ("howl should be heard");

		//yield return new WaitForSeconds(4);

		//lostWolfHowl.enabled = false;
	}


}
