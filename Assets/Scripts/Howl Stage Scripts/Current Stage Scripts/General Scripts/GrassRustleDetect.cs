using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]

public class GrassRustleDetect : MonoBehaviour {

	private Animator anim;
	public AudioSource[] sources = new AudioSource[1];

	// Use this for initialization
	void Start () {
		//anim = GetComponent<Animator> ();
		//anim.SetInteger ("AnimState", 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void GrassSFX(){
		if (!sources [0].isPlaying) {
			//audio.Play ();
			sources[0].Play();

		}
		
		
	}
	
	void StopGrassSFX(){
		//if (sources [1].isPlaying || sources [0].isPlaying) {
		//audio.Play ();
		sources[0].Stop();

		//}
		
	}

		void OnTriggerEnter2D(Collider2D target)
		{
			if (target.gameObject.tag == "Player") 
			{
			GrassSFX();
				//if(attacking)
				//{
				//var explode = target.GetComponent<Explode>() as Explode;
				//explode.OnExplode();
				//Destroy(target.gameObject); 
				//anim.SetInteger ("AnimState", 1);
			//print ("wolf touch grass!");

			} 
		}
	
		void OnTriggerStay2D(Collider2D target)
		{
	if (target.gameObject.tag == "Player") 
			{
			GrassSFX();
			//anim.SetInteger ("AnimState", 1);
			} 	
		}
	
		void OnTriggerExit2D(Collider2D target){
			
			//anim.SetInteger ("AnimState", 0);
		StopGrassSFX ();
		}

}
