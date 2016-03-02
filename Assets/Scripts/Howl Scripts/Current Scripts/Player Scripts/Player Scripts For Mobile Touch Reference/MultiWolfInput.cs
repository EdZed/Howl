using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]

public class MultiWolfInput : MonoBehaviour 
{
	float speed;
	float moveSpeed = 6f;
	float runSpeed = 10f;
	public float tapMaxMovement = 50f;
	//float touchStartTime = 0;
	//float startHowlTime = 0;
	public float tapTimerMax = 3.25f;//0.75f;
	public float howlTimerMax = 8f;//0.75f;

	//float startRestartTimer = 0;
	public float restartTimerMax = 10;
	public float restartTimer = 10;

	//Vector3 targetPos = Vector3.zero;
	Vector3 targetPos;
	private Animator anim;
	public Rigidbody2D rb2DplayerWolf;
	public GameObject playerWolf;
	public GameObject HowlAttract;
	public GameObject MainCam;
	public CircleCollider2D HowlAttractCollider;
	
	public bool walking;
	public bool running;

	public AudioSource[] sources = new AudioSource[1];
	
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

		//#if UNITY_IOS
		targetPos = Vector3.zero;
		//#endif
	}
	
	// Update is called once per frame
	//Using fixed update instead for rigidbody use
	void FixedUpdate () 
	{
		//touchStartTime = Time.time + tapTimerMax;
		//startRestartTimer = Time.time + 0;
		restartTimer -= Time.deltaTime;

		if(restartTimer <= 0){
			Application.LoadLevel ("Howl Title Screen");
		}

		//#if UNITY_IOS
		Debug.Log("Unity iOS");
		//float speed;
		if (Input.touchCount > 0) 
		{
			Touch touch = Input.touches [0];
			
			switch(touch.phase)
			{
			case TouchPhase.Began:
				//startRestartTimer = Time.time + 0;
				restartTimer = restartTimerMax;
				targetPos = Camera.main.ScreenToWorldPoint (touch.position);
				//targetpos- transform.position make into vector2 and feed into input x and y and normalize it.
				//touchStartTime = Time.time + tapTimerMax;
				if(Input.GetTouch(0).tapCount == 2){
					running = true;
					walking = false;
					speed = runSpeed;
					Debug.Log("DOUBLE TAP");
//					if((doubleTapHand.GetComponent<Animator> ().enabled = true) && (doubleTapHand.GetComponent<SpriteRenderer> ().enabled = false)){
//						//doubleTapHand.GetComponent<Animator> ().enabled = false;
//						//doubleTapHand.GetComponent<SpriteRenderer> ().enabled = false;
//						//Destroy(doubleTapHand);
//					}

				}else{
					//touchStartTime = Time.time + tapTimerMax;
					speed = 0;
					running = false;
					walking = true;
					//targetPos = Camera.main.ScreenToWorldPoint (touch.position);
					//print ("wolf started!");
					
				}
				break;
			case TouchPhase.Stationary:
				//				if(running){
				//					speed = runSpeed;
				//				} else if(walking){
				//					speed = moveSpeed;
				//				}
				//
				//				break;
			case TouchPhase.Moved:
				restartTimer = restartTimerMax;
				//add dead zone down the line
				targetPos = Camera.main.ScreenToWorldPoint (touch.position);
				if(running){
					speed = runSpeed;
				} else if (walking){
					speed = moveSpeed;
					//targetPos = Camera.main.ScreenToWorldPoint (touch.position);
				}
				//print ("wolf moving!");
				break;
			default:
			case TouchPhase.Ended:
				//print ("wolf stop!");
				//anim.SetInteger ("AnimState", 0);
				//speed = 0;
				//speed here was preventing wolf from moving when tapping/pouncing
				StopSFX();
				running = false;
				walking = false;
				
				break;
				
			case TouchPhase.Canceled:
				
				//				speed = 0;
				//				anim.SetInteger ("AnimState", 0);
				//				
				break;
				
			}//end of switch touch.phase
		//#endif



			
			if(running){
				RunSFX();
				//sources[1].Play();
				//sources[0].Stop();
				//print ("Running!");
				if (targetPos.x > transform.position.x) 
				{
					//anim.SetTrigger("walk");
					anim.SetInteger ("AnimState", 7);
					//print ("running right!");
					rb2DplayerWolf.MovePosition (Vector2.MoveTowards (playerWolf.transform.position, targetPos, speed * Time.deltaTime));

//					#if UNITY_EDITOR || UNITY_WEBPLAYER
//					target = moveDirection * speed + currentPosition;
//					transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );
//					#endif

					if (playerWolf.transform.localScale.x < 0)
						playerWolf.transform.localScale = new Vector3 (1, 1, 1);

					
				} else if (targetPos.x < transform.position.x) {
					//anim.SetTrigger("walkLeft");
					anim.SetInteger ("AnimState", 7);
					//print ("running left!");
					rb2DplayerWolf.MovePosition (Vector2.MoveTowards (playerWolf.transform.position, targetPos, speed * Time.deltaTime));

//					#if UNITY_EDITOR || UNITY_WEBPLAYER
//					target = moveDirection * speed + currentPosition;
//					transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );
//					#endif
					
					if (playerWolf.transform.localScale.x > 0)
						playerWolf.transform.localScale = new Vector3 (-1, 1, 1);	
					
				} else if (Input.touchCount < 0) {
					anim.SetInteger ("AnimState", 0);
					//anim.SetTrigger("stand");
					//				anim.SetBool ("walk", false);
					/*anim.SetInteger ("AnimState", 0);
										print ("wolf stand!");
										didnt work because touchcount > 0 not only in this if statement but in the previous one
					 					*/
				}
			} else if(walking) {
				WalkSFX();
				//sources[1].Stop();
				//sources[0].Play();
				//print ("walking!");
				if (targetPos.x > transform.position.x) 
				{
					//anim.SetTrigger("walk");
					anim.SetInteger ("AnimState", 2);
					//print ("walking right!");
					rb2DplayerWolf.MovePosition (Vector2.MoveTowards (playerWolf.transform.position, targetPos, speed * Time.deltaTime));

//					#if UNITY_EDITOR || UNITY_WEBPLAYER
//					target = moveDirection * speed + currentPosition;
//					transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );
//					#endif


					if (playerWolf.transform.localScale.x < 0)
						playerWolf.transform.localScale = new Vector3 (1, 1, 1);
					
				} else if (targetPos.x < transform.position.x) {
					//anim.SetTrigger("walkLeft");
					anim.SetInteger ("AnimState", 1);
					//print ("walking left!");
					rb2DplayerWolf.MovePosition (Vector2.MoveTowards (playerWolf.transform.position, targetPos, speed * Time.deltaTime));
//					#if UNITY_EDITOR || UNITY_WEBPLAYER
//					target = moveDirection * speed + currentPosition;
//					transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );
//					#endif

					if (playerWolf.transform.localScale.x > 0)
						playerWolf.transform.localScale = new Vector3 (-1, 1, 1);	
					
				} else if (Input.touchCount < 0) {
					anim.SetInteger ("AnimState", 0);
					//anim.SetTrigger("stand");
					//				anim.SetBool ("walk", false);
					/*anim.SetInteger ("AnimState", 0);
										print ("wolf stand!");
										didnt work because touchcount > 0 not only in this if statement but in the previous one
					 					*/
				}
			} else{
				//doesn't work
				StopSFX();
				//sources[1].Stop();
				//sources[0].Stop();
			}
			
		}//end of touchcount
		else 
		{
			anim.SetInteger("AnimState", 0);
		}	

	//#endif
		
	}//end of update. Now fixedUpdate
	
	
	void WalkSFX(){
		if (!sources [0].isPlaying) {
			//audio.Play ();
			sources [1].Stop ();
			sources [0].Play ();
		}
	}
	
	void RunSFX(){
		if (!sources [1].isPlaying) {
			//audio.Play ();
			sources[0].Stop();
			sources[1].Play();
		}
		
		
	}
	
	void StopSFX(){
		//if (sources [1].isPlaying || sources [0].isPlaying) {
		//audio.Play ();
		sources[0].Stop();
		sources[1].Stop();
		//}
		
	}
	
} //end of whole class***






