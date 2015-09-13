using UnityEngine;
using System.Collections;

public class LostWolfProximity : MonoBehaviour {

	public GameObject playerWolf;
	private Animator anim;


	// Use this for initialization
	void Start () {
		playerWolf = GameObject.Find("playerWolf");
		anim = playerWolf.GetComponent<Animator> ();
		//anim = gameObject.GetComponentInParent<Animator> ();
		//anim.SetTrigger("stand");
		//anim.SetInteger ("AnimState", 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "Player") 
		{
			anim.SetInteger ("AnimState", 6);
			anim.SetInteger ("AnimState", 5);
			//if(attacking)
			//{
			//var explode = target.GetComponent<Explode>() as Explode;
			//explode.OnExplode();
			//Destroy(target.gameObject); 
			//Destroy(target.transform.parent.gameObject); 
			
			 
			
		} 	
	} // end ontriggerEnter


}
