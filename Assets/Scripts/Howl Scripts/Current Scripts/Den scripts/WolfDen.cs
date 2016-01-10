using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WolfDen : MonoBehaviour {
	
	//Wolf Den Art
	public GameObject wolfDenArt;
	private Animator wolfDenAnim;	
	public bool isTriggering;
	public GameObject[] spiritAnim = new GameObject[4];	
	public GameObject PlayerWolfGO;

	//public CircleCollider2D DenCollider;
	//public ParticleSystemRenderer[] particlefx = new ParticleSystemRenderer[1];
	Color startColor;
	public GameObject wolfDen;

	//event**
	public delegate void Rescues();
	public static event Rescues WolfRescued;

	public WorldManager WorldManagerScript;

	public bool PackExist;

	//public delegate void ParticlesOff();
	//public static event ParticlesOff DenParticlesOff;
	
	// Use this for initialization
	void Start () {
//		spiritAnim [0].GetComponent<Animator> ().enabled = false;
//		spiritAnim [1].GetComponent<Animator> ().enabled = false;
//		spiritAnim [2].GetComponent<Animator> ().enabled = false;
//		spiritAnim [3].GetComponent<Animator> ().enabled = false;
		
//		spiritAnim [0].GetComponent<SpriteRenderer> ().enabled = false;
//		spiritAnim [1].GetComponent<SpriteRenderer> ().enabled = false;
//		spiritAnim [2].GetComponent<SpriteRenderer> ().enabled = false;
//		spiritAnim [3].GetComponent<SpriteRenderer> ().enabled = false;
		
		//wolf Den
		wolfDenArt = GameObject.Find ("WolfDen");
		wolfDenAnim = wolfDenArt.GetComponent<Animator> ();

		wolfDenAnim.SetInteger ("DenAnimState", 0);
		isTriggering = false;

		PlayerWolfGO = GameObject.Find("playerWolf");

		//DenCollider = this.gameObject.GetComponent<CircleCollider2D> ();
		//wolfDen = GameObject.Find("Wolf Den");
		//startColor = wolfDenArt.GetComponent<SpriteRenderer> ().color;
	}//end start
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D target)
	{
		isTriggering = true;
		
		//send message to WolfDeManager
		if (target.gameObject.tag == "LostWolf" 
		    || target.gameObject.tag == "LostWolfOrange"
		    || target.gameObject.tag == "LostWolfGreen") 
		{
//			Color DenOff = this.gameObject.GetComponent<SpriteRenderer> ().color;
//			DenOff.r -= 50f;
//			DenOff.g -= 50f;
//			DenOff.b -= 50f;
//			//DenOff = Color.Lerp (startColor, Color.white, 4);
//			this.gameObject.GetComponent<SpriteRenderer> ().color = DenOff;
//
//			DenCollider.enabled = false;
//			if(DenParticlesOff != null){
//				DenParticlesOff();
//			}
			//sends message to WolfDenManager Script
			if(WolfRescued != null){
				WolfRescued();
			}
		}//end target tag LostWolf
		if (PackExist == false) {
			//if no pack, can switch worlds
			if (target.gameObject.tag == "HowlAttract") {
				//send an event that changes the color of all prefabs
				//PCwolf or warmthobj int changed to spirit world
				//WorldManagerScript.WorldTypeSwitch();
				WorldManagerScript.StartCoroutine ("WorldTypeSwitch");
			
			}
		}


	}//end on trigger enter

	void OnEnable()
	{
		PackFormationPos.OnPackNotExist += NoPack;
		FollowPlayer.OnPackNotExist += NoPack;
		PackFormationPos.OnPackDoesExist += YesPack;
	}
	
	
	void OnDisable()
	{
		PackFormationPos.OnPackNotExist -= NoPack;
		FollowPlayer.OnPackNotExist -= NoPack;
		PackFormationPos.OnPackDoesExist -= YesPack;
	}
	
	void NoPack(){
		PackExist = false;
	}

	void YesPack(){
		PackExist = true;
	}

}//end whole class










