﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]

public class PCWolfInput : MonoBehaviour 
{
	//  Static keyword makes this variable a Member of the class, not of any particular instance.
	public static float speed = 0f;
	public static float moveSpeed = 6f;
	public static float runSpeed = 12f;
	public float tapMaxMovement = 50f;
	//float touchStartTime = 0;
	//float startHowlTime = 0;
	public float tapTimerMax = 3.25f;//0.75f;
	public float howlTimerMax = 8f;//0.75f;

	//Vector3 targetPos = Vector3.zero;
	Vector3 targetPos;
	private Animator anim;
	public Rigidbody2D rb2DplayerWolf;
	public GameObject playerWolf;
	public GameObject HowlAttract;
	public GameObject MainCam;
	public CircleCollider2D HowlAttractCollider;
	public float HowlRadiusMax;
	public float HowlRadiusRate;
	
	public bool walking;
	public bool running;

	Vector3 currentPosition;

	public delegate void ClickLeft();
	//OnClickLeft is an event variable
	public static event ClickLeft OnClickLeft;

	public delegate void ClickRight();
	public static event ClickRight OnClickRight;

	//#if UNITY_EDITOR || UNITY_WEBPLAYER
	private Vector3 moveDirection;
	Vector3 targetPoint;
	//#endif

	//changed 1 to 2 in editor
	public AudioSource[] sources = new AudioSource[1];

	RaycastHit2D hit;
	bool howling = false;
	
	// Use this for initialization
	void Start () 
	{

		Screen.orientation = ScreenOrientation.LandscapeLeft;
		//Screen.orientation = ScreenOrientation.AutoRotation;
		anim = GetComponent<Animator> ();
		playerWolf = GameObject.Find("playerWolf");
		MainCam = GameObject.Find("Main Camera");
		anim.SetInteger ("AnimState", 0);
		rb2DplayerWolf = playerWolf.GetComponent<Rigidbody2D>();
		HowlAttract = GameObject.Find("HowlAttract");
		HowlAttractCollider = HowlAttract.GetComponent <CircleCollider2D> ();

		HowlAttractCollider.radius = 0f;
		
		//restartTimer -= Time.deltaTime;

		//#if UNITY_EDITOR || UNITY_WEBPLAYER
		//targetPos.z = transform.position.z - Camera.main.transform.position.z;
		//transform.position = Camera.main.ScreenToWorldPoint(targetPos);
		//targetPos = transform.position;
		//#endif
//		#if UNITY_IOS
//		targetPos = Vector3.zero;
//		#endif
	}
	
	// Update is called once per frame
	//Using fixed update instead for rigidbody use
	void Update () 
	{
		//#if UNITY_EDITOR || UNITY_WEBPLAYER
		//Debug.Log("Unity Editor");
		
		//currentPosition = transform.position;

		//Hold left-click to walk
//		if(Input.GetMouseButton(0))
//		{
//
//			//hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint((Input.GetTouch (0).position)), Vector2.zero);
////			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
////			
////			if (hit.collider != null && hit.collider.gameObject.tag == "Player") 
////			{
////				//attacking = true;
////				howling = true;
////				speed = 0;
////				//hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint((Input.GetTouch (0).position)), Vector2.zero);
////				targetPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
////				anim.SetInteger ("AnimState", 5);
////				//anim.SetInteger ("AnimState", 3);
////				print ("player is howling!");
////			} else 
////			{
//				Vector3 targetPos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
//				
//				moveDirection = targetPos - currentPosition;
//				moveDirection.z = 0; 
//				moveDirection.Normalize();
//				
//				running = false;
//				walking = true;
//				speed = moveSpeed;
//				
//				//Makes sure even doesn't equal null in order to prevent error if invoking an event with no subscribers
//				if(OnClickLeft != null)
//				{
//					//event variable used as a call to a function. This invokes the event
//					OnClickLeft();
//				}
//			//}
//			//anim.SetInteger ("AnimState", 0);
//		}
//
//		//Hold right-click to Run
//		if(Input.GetMouseButton(1))
//		{
//			Vector3 targetPos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
//			
//			moveDirection = targetPos - currentPosition;
//			moveDirection.z = 0; 
//			moveDirection.Normalize();
//
//			running = true;
//			walking = false;
//			speed = runSpeed;
//			Debug.Log("DOUBLE TAP");
//
//			if(OnClickRight !=null)
//			{
//			OnClickRight();
//			}
//		}
//
//		//Both left/right click are no longer pressed
//		if(Input.GetMouseButtonUp(0)||Input.GetMouseButtonUp(1))
//		{
//			StopSFX();
//			running = false;
//			walking = false;
//		}

		//arrow keys controls
		if (running) {
			anim.SetInteger ("AnimState", 7);
			RunSFX();
		} else if (walking) {
			anim.SetInteger ("AnimState", 2);
			WalkSFX();
		} else {
			//doesn't work
			anim.SetInteger ("AnimState", 0);
			StopSFX();
		}

		if ((howling && !running) || (howling && !walking)) {
			anim.SetInteger("AnimState", 6);
			if (HowlAttractCollider.radius < HowlRadiusMax){
				HowlAttractCollider.radius += HowlRadiusRate;
			}
			
			if(HowlAttractCollider.radius >= HowlRadiusMax){
				HowlAttractCollider.radius = 0f;
				howling = false;
			}
		}
		if (Input.GetKeyUp (KeyCode.UpArrow)||
		    Input.GetKeyUp(KeyCode.DownArrow)||
		    Input.GetKeyUp(KeyCode.LeftArrow)|| 
		    Input.GetKeyUp(KeyCode.RightArrow)||
		    Input.GetKeyUp(KeyCode.W)||
		    Input.GetKeyUp(KeyCode.A)||
		    Input.GetKeyUp(KeyCode.S)|| 
		    Input.GetKeyUp(KeyCode.D) ) {
			StopSFX();
			anim.SetInteger("AnimState", 0);
			//print ("Player is idle!");
			walking = false;
			running = false;
		}

		if(Input.GetKeyUp(KeyCode.Space)) {
			howling = true;
		}

		if (Input.GetKey (KeyCode.LeftArrow)|| Input.GetKey(KeyCode.A)) {
			WolfMoveToLeft();
			if (Input.GetKey (KeyCode.LeftShift)) {
				running = true;
				walking = false;
				speed = runSpeed;
				transform.position += Vector3.left * speed * Time.deltaTime;
			} else {
				running = false;
				walking = true;
				speed = moveSpeed;
				transform.position += Vector3.left * speed * Time.deltaTime;
			}
		} 
		if (Input.GetKey (KeyCode.RightArrow)|| Input.GetKey(KeyCode.D)) {
			WolfMoveToRight();
			if (Input.GetKey (KeyCode.LeftShift)) {
				running = true;
				walking = false;
				speed = runSpeed;
				transform.position += Vector3.right * speed * Time.deltaTime;
			} else {
				running = false;
				walking = true;
				speed = moveSpeed;
				transform.position += Vector3.right * speed * Time.deltaTime;
			}
		} 
		if (Input.GetKey(KeyCode.UpArrow)|| Input.GetKey(KeyCode.W)) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				running = true;
				walking = false;
				speed = runSpeed;
				transform.position += Vector3.up * speed * Time.deltaTime;
			} else {
				running = false;
				walking = true;
				speed = moveSpeed;
				transform.position += Vector3.up * speed * Time.deltaTime;
			}
		} 
		if (Input.GetKey(KeyCode.DownArrow)|| Input.GetKey(KeyCode.S)) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				running = true;
				walking = false;
				speed = runSpeed;
				transform.position += Vector3.down * speed * Time.deltaTime;
			} else {
				running = false;
				walking = true;
				speed = moveSpeed;
				transform.position += Vector3.down * speed * Time.deltaTime;
			}
		} 
	}//end of update. Now fixedUpdate
	//Vector3 target = moveDirection * speed + currentPosition;
	//transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );
	
	//#endif

//}//end of update. Now fixedUpdate
	
	void WalkSFX() {
		if (!sources [0].isPlaying) 
		{
			//audio.Play ();
			sources [1].Stop ();
			sources [0].Play ();
		}
	}

	void RunSFX() {
		if (!sources [1].isPlaying) 
		{
			//audio.Play ();
			sources[0].Stop();
			sources[1].Play();
		}
	}

	void StopSFX() {
		//if (sources [1].isPlaying || sources [0].isPlaying) {
		//audio.Play ();
		sources[0].Stop();
		sources[1].Stop();
		//}
		
	}

	void WolfMoveToRight() {
		if (playerWolf.transform.localScale.x < 0) {
			playerWolf.transform.localScale = new Vector3 (1, 1, 1);
		}
	}

	void WolfMoveToLeft() {
		if (playerWolf.transform.localScale.x > 0) {
			playerWolf.transform.localScale = new Vector3 (-1, 1, 1);	
		}
	}
//	void WolfRun()
//	{
//		if (Input.GetKey (KeyCode.LeftShift))
//		{
//			anim.SetInteger("AnimState", 7);
//			speed = runSpeed;
//			running = true;
//			walking = false;	
//
//			if (Input.GetKeyUp (KeyCode.LeftShift))
//			{
//				speed = moveSpeed;
//				running = false;
//				walking = true;
//				anim.SetInteger("AnimState", 7);
//			}
//		}
//	}
	
}//end of whole class***





