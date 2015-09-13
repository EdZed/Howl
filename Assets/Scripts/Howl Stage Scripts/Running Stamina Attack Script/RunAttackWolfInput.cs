using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]

public class RunAttackWolfInput : MonoBehaviour 
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
	//public float restartTimerMax = 10;
	//public float restartTimer = 10;
	
	//Vector3 targetPos = Vector3.zero;
	Vector3 targetPos;
	private Animator anim;
	public Rigidbody2D rb2DplayerWolf;
	public GameObject playerWolf;
	public GameObject HowlAttract;
	public GameObject MainCam;
	public CircleCollider2D HowlAttractCollider;

	public BoxCollider2D attackCollider;

	public float staminaMax = 5f;
	public float stamina = 5f;

	
	public bool walking;
	public bool running;
	
	//#if UNITY_EDITOR || UNITY_WEBPLAYER
	private Vector3 moveDirection;
	Vector3 targetPoint;
	//#endif
	//public GameObject doubleTapHand;
	
	//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	//RaycastHit hit;
	//public var MousePos;
	//var MousePos : Vector3 = camera.main.ScreenToWorldPoint (Input.mousePosition);
	//Vector3 MousePos = Vector3.zero;
	//Vector3 MousePos = Input.mousePosition;
	//	pos.z = transform.position.z - Camera.main.transform.position.z;
	//	transform.position = Camera.main.ScreenToWorldPoint(pos);
	//	
	//changed 1 to 2 in editor
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
		attackCollider.enabled = false;
		
		//doubleTapHand.GetComponent<Animator> ().enabled = false;
		//doubleTapHand.GetComponent<SpriteRenderer> ().enabled = false;
		
		//restartTimer -= Time.deltaTime;
		
		//MainCamScript = MainCam.GetComponent<Camera2DFollow>();
		//MainCam.GetComponent<Camera2DFollow>().enabled = false;
		//(gameObject.GetComponent( "Script" ) as MonoBehaviour).enabled = true;
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
	void FixedUpdate () 
	{
		//touchStartTime = Time.time + tapTimerMax;
		//startRestartTimer = Time.time + 0;
		//restartTimer -= Time.deltaTime;
		
//		if(restartTimer <= 0){
//			Application.LoadLevel ("Howl Title Screen");
//		}
		
		//#if UNITY_EDITOR || UNITY_WEBPLAYER
		//Debug.Log("Unity Editor");
		
		Vector3 currentPosition = transform.position;

		//Debug.Log (stamina.ToString());
		
		if(Input.GetMouseButton(0)){

			
			Vector3 targetPos = Camera.main.ScreenToWorldPoint( Input.mousePosition );

			moveDirection = targetPos - currentPosition;
			moveDirection.z = 0; 
			moveDirection.Normalize();

			running = false;
			walking = true;
			speed = moveSpeed;
			
			//			if (Physics.Raycast(targetPos)){
			//				//Instantiate(particle, transform.position, transform.rotation);
			//			}
		}//ends getmousebuttondown
		//			else{
		//				//touchStartTime = Time.time + tapTimerMax;
		//				speed = 0;
		//				running = false;
		//				walking = true;
		//				//targetPos = Camera.main.ScreenToWorldPoint (touch.position);
		//				//print ("wolf started!");	
		//			}
		
		if(Input.GetMouseButton(1)){
			Vector3 targetPos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			
			moveDirection = targetPos - currentPosition;
			moveDirection.z = 0; 
			moveDirection.Normalize();

			
//			if(stamina < 5f | stamina<=0f){
//				//Application.LoadLevel ("Howl Title Screen");
//				running = false;
//				walking = true;
//				speed = moveSpeed;
//			}
			//if(stamina >= 5f)
			//{
				running = true;
				walking = false;
				speed = runSpeed;
				Debug.Log("DOUBLE TAP");

				//stamina-= Time.deltaTime * 1;
				//Debug.Log (stamina.ToString());
				//Debug.Log("WeaponNum = " + WeaponNum.ToStiring());
			//}
		}
		
		if(Input.GetMouseButtonUp(0)||Input.GetMouseButtonUp(1)){
			StopSFX();
			running = false;
			walking = false;
			
		}
		
		if(running){
			RunSFX();
			//sources[1].Play();
			//sources[0].Stop();
			//print ("Running!");
			//stamina -= Time.deltaTime*1;
			attackCollider.enabled = true;

			if (targetPoint.x > transform.position.x) 
			{
				//anim.SetTrigger("walk");
				anim.SetInteger ("AnimState", 7);
				//print ("running right!");
				//rb2DplayerWolf.MovePosition (Vector2.MoveTowards (playerWolf.transform.position, targetPos, speed * Time.deltaTime));
				
				//#if UNITY_EDITOR || UNITY_WEBPLAYER
				targetPoint = moveDirection * speed + currentPosition;
				transform.position = Vector3.Lerp( currentPosition, targetPoint, Time.deltaTime );
				//#endif
				
				if (playerWolf.transform.localScale.x < 0)
					playerWolf.transform.localScale = new Vector3 (1, 1, 1);
				
				
			} else if (targetPoint.x < transform.position.x) {
				//anim.SetTrigger("walkLeft");
				anim.SetInteger ("AnimState", 7);
				//print ("running left!");
				//rb2DplayerWolf.MovePosition (Vector2.MoveTowards (playerWolf.transform.position, targetPos, speed * Time.deltaTime));
				
				//#if UNITY_EDITOR || UNITY_WEBPLAYER
				targetPoint = moveDirection * speed + currentPosition;
				transform.position = Vector3.Lerp( currentPosition, targetPoint, Time.deltaTime );
				//#endif
				
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

			attackCollider.enabled = false;

			if (targetPoint.x > transform.position.x) 
			{
				//anim.SetTrigger("walk");
				anim.SetInteger ("AnimState", 2);
				//print ("walking right!");
				//rb2DplayerWolf.MovePosition (Vector2.MoveTowards (playerWolf.transform.position, targetPos, speed * Time.deltaTime));
				
				//#if UNITY_EDITOR || UNITY_WEBPLAYER
				targetPoint = moveDirection * speed + currentPosition;
				transform.position = Vector3.Lerp( currentPosition, targetPoint, Time.deltaTime );
				//#endif
				
				
				if (playerWolf.transform.localScale.x < 0)
					playerWolf.transform.localScale = new Vector3 (1, 1, 1);
				
			} else if (targetPoint.x < transform.position.x) {
				//anim.SetTrigger("walkLeft");
				anim.SetInteger ("AnimState", 1);
				//print ("walking left!");
				//rb2DplayerWolf.MovePosition (Vector2.MoveTowards (playerWolf.transform.position, targetPos, speed * Time.deltaTime));
				//#if UNITY_EDITOR || UNITY_WEBPLAYER
				targetPoint = moveDirection * speed + currentPosition;
				transform.position = Vector3.Lerp( currentPosition, targetPoint, Time.deltaTime );
				//#endif
				
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
			anim.SetInteger("AnimState", 0);

			attackCollider.enabled = false;
			//sources[1].Stop();
			//sources[0].Stop();
		}
		
		//end of touchcount
//		else 
//		{
//			
//		}	
		
	}//end of update. Now fixedUpdate
	//Vector3 target = moveDirection * speed + currentPosition;
	//transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );
	
	//#endif
	

//}//end of update. Now fixedUpdate


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

}//end of whole class***






