using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]

public class NewWolfInput : MonoBehaviour 
{
	float speed;
	float moveSpeed = 6f;
	float runSpeed = 10f;
	public float tapMaxMovement = 50f;
	//float touchStartTime = 0;
	//float startHowlTime = 0;
	public float tapTimerMax = 3.25f;//0.75f;
	public float howlTimerMax = 8f;//0.75f;
	Vector3 targetPos = Vector3.zero;
	private Animator anim;
	public Rigidbody2D rb2DplayerWolf;
	public GameObject playerWolf;
	public GameObject HowlAttract;
	public GameObject MainCam;
	public CircleCollider2D HowlAttractCollider;

	public bool walking;
	public bool running;

	//changed 1 to 2 in editor
	public AudioSource[] sources = new AudioSource[1];

	// Use this for initialization
	void Start () 
	{
		//Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.orientation = ScreenOrientation.AutoRotation;
		anim = GetComponent<Animator> ();
		playerWolf = GameObject.Find("playerWolf");
		MainCam = GameObject.Find("Main Camera");
		anim.SetInteger ("AnimState", 0);
		rb2DplayerWolf = playerWolf.GetComponent<Rigidbody2D>();
		HowlAttract = GameObject.Find("HowlAttract");
		HowlAttractCollider = HowlAttract.GetComponent <CircleCollider2D> ();
		//MainCamScript = MainCam.GetComponent<Camera2DFollow>();
		//MainCam.GetComponent<Camera2DFollow>().enabled = false;
		//(gameObject.GetComponent( "Script" ) as MonoBehaviour).enabled = true;

	}
	
	// Update is called once per frame
	//Using fixed update instead for rigidbody use
	void FixedUpdate () 
	{
		
		//float speed;
		if (Input.touchCount > 0) 
		{
			Touch touch = Input.touches [0];
			
			switch(touch.phase)
			{
			case TouchPhase.Began:
				targetPos = Camera.main.ScreenToWorldPoint (touch.position);
				//touchStartTime = Time.time + tapTimerMax;
				if(Input.GetTouch(0).tapCount == 2){
					running = true;
					walking = false;
					speed = runSpeed;
					Debug.Log("DOUBLE TAP");
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
					
					if (playerWolf.transform.localScale.x < 0)
						playerWolf.transform.localScale = new Vector3 (1, 1, 1);
					
				} else if (targetPos.x < transform.position.x) {
					//anim.SetTrigger("walkLeft");
					anim.SetInteger ("AnimState", 7);
					//print ("running left!");
					rb2DplayerWolf.MovePosition (Vector2.MoveTowards (playerWolf.transform.position, targetPos, speed * Time.deltaTime));
					
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
					
					if (playerWolf.transform.localScale.x < 0)
						playerWolf.transform.localScale = new Vector3 (1, 1, 1);
					
				} else if (targetPos.x < transform.position.x) {
					//anim.SetTrigger("walkLeft");
					anim.SetInteger ("AnimState", 1);
					//print ("walking left!");
					rb2DplayerWolf.MovePosition (Vector2.MoveTowards (playerWolf.transform.position, targetPos, speed * Time.deltaTime));
					
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

//	void Howl(){
//		startHowlTime = Time.time + howlTimerMax;
//		howling = true;
//		HowlAttractCollider.enabled = true;
//
//		if (howling) {
//			speed = 0;
//			GetComponent<AudioSource> ().Play ();
//			print ("howl sound started!");
//		}
////		if (Time.time > startHowlTime) 
////		{
////			HowlAttractCollider.enabled = false;
////		} 
//		
////		else{
////			speed = 0;
////		}
//	}

//	void HowlEnd(){
//		howling = false;
//		//isTap = false;
//		
//		if (!howling) {
//			speed = moveSpeed;
//			GetComponent<AudioSource> ().Stop ();
//			print ("howl sound ended!");
//		}
//	}


//	void OnTriggerEnter2D(Collider2D target)
//	{
//		if (target.gameObject.tag == "Proximity") 
//		{
//			//if(attacking)
//			//{
//			//var explode = target.GetComponent<Explode>() as Explode;
//			//explode.OnExplode();
//			//Destroy(target.gameObject); 
//			//anim.SetInteger ("AnimState", 5);
//		} else {
//			//anim.SetInteger("AnimState", 0);
//		}	
//	}
	//}

//	void OnTriggerStay2D(Collider2D target)
//	{
//		if (target.gameObject.tag == "Proximity") 
//		{
//			//if(attacking)
//			//{
//			//var explode = target.GetComponent<Explode>() as Explode;
//			//explode.OnExplode();
//			//Destroy(target.gameObject); 
//			//rb2DLostWolf.MovePosition (Vector2.MoveTowards (LostWolfGO.transform.position, PlayerWolfGO.transform.position, speed * Time.deltaTime));
//			
//		} else {
//			anim.SetInteger("AnimState", 0);
//		}	
//	}
	
//	void OnTriggerExit2D(Collider2D target){
//		//readyToAttack = false;
//		//attacking = false;
//		//anim.SetInteger ("AnimState", 0);
//	}
		
} //end of whole class***






