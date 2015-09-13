using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]

public class NewWolfInputBackup : MonoBehaviour 
{
	float speed;
	float moveSpeed = 6f;
	public float tapMaxMovement = 50f;
	//float touchStartTime = 0;
	float startHowlTime = 0;
	public float tapTimerMax = 3.25f;//0.75f;
	public float howlTimerMax = 8f;//0.75f;
	Vector3 targetPos = Vector3.zero;
	private Animator anim;
	public Rigidbody2D rb2DplayerWolf;
	public GameObject playerWolf;
	public GameObject HowlAttract;
	public GameObject MainCam;
	public CircleCollider2D HowlAttractCollider;
	
	
	//Lost wolves
	//	public GameObject lostWolf1;
	//	public GameObject lostWolf2;
	//	public GameObject lostWolf3;
	//	public GameObject lostWolf4;
	//	public GameObject lostWolf5;
	//
	//	public BoxCollider2D lostWolf1Collider;
	//	public BoxCollider2D lostWolf2Collider;
	//	public BoxCollider2D lostWolf3Collider;
	//	public BoxCollider2D lostWolf4Collider;
	//	public BoxCollider2D lostWolf5Collider;
	
	
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
		
		//lost wolves find gameobjects
		//		lostWolf1 = GameObject.Find ("Lost Wolf 1");
		//		lostWolf2 = GameObject.Find ("Lost Wolf 2");
		//		lostWolf3 = GameObject.Find ("Lost Wolf 3");
		//		lostWolf4 = GameObject.Find ("Lost Wolf 4");
		//		lostWolf5 = GameObject.Find ("Lost Wolf 5");
		//
		//		//Find lost wolf colliders
		//		lostWolf1Collider = lostWolf1.GetComponent <BoxCollider2D> ();
		//		lostWolf2Collider = lostWolf2.GetComponent <BoxCollider2D> ();
		//		lostWolf3Collider = lostWolf3.GetComponent <BoxCollider2D> ();
		//		lostWolf4Collider = lostWolf4.GetComponent <BoxCollider2D> ();
		//		lostWolf5Collider = lostWolf5.GetComponent <BoxCollider2D> ();
		//		
		//		//Disable Lost wolf renderers
		//		lostWolf1.GetComponent<Renderer> ().enabled = false;
		//		lostWolf2.GetComponent<Renderer> ().enabled = false;
		//		lostWolf3.GetComponent<Renderer> ().enabled = false;
		//		lostWolf4.GetComponent<Renderer> ().enabled = false;
		//		lostWolf5.GetComponent<Renderer> ().enabled = false;
		//
		//		//Disable Lost wolf audio sources
		//		lostWolf1.GetComponent<AudioSource> ().enabled = false;
		//		lostWolf2.GetComponent<AudioSource> ().enabled = false;
		//		lostWolf3.GetComponent<AudioSource> ().enabled = false;
		//		lostWolf4.GetComponent<AudioSource> ().enabled = false;
		//		lostWolf5.GetComponent<AudioSource> ().enabled = false;
		//
		//		//Disable Lost wolf colliders
		//		lostWolf1Collider.enabled = false;
		//		lostWolf2Collider.enabled = false;
		//		lostWolf3Collider.enabled = false;
		//		lostWolf4Collider.enabled = false;
		//		lostWolf5Collider.enabled = false;
		
		
		//		lostWolf1.SetActive (false);
		//		lostWolf2.SetActive (false);
		//		lostWolf3.SetActive (false);
		//		lostWolf4.SetActive (false);
		//		lostWolf5.SetActive (false);
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
				
				//touchStartTime = Time.time + tapTimerMax;
				speed = 0;
				targetPos = Camera.main.ScreenToWorldPoint (touch.position);
				//print ("wolf started!");
				
				
				break;
			case TouchPhase.Stationary:
				
				speed = moveSpeed;
				
				break;
			case TouchPhase.Moved:
				//add dead zone down the line
				
				speed = moveSpeed;
				targetPos = Camera.main.ScreenToWorldPoint (touch.position);
				//print ("wolf moving!");
				break;
			default:
			case TouchPhase.Ended:
				//print ("wolf stop!");
				//anim.SetInteger ("AnimState", 0);
				//speed = 0;
				//speed here was preventing wolf from moving when tapping/pouncing
				
				//break;
				
			case TouchPhase.Canceled:
				
				speed = 0;
				anim.SetInteger ("AnimState", 0);
				
				break;
				
			}//end of switch touch.phase
			
			if (targetPos.x > transform.position.x) 
			{
				//anim.SetTrigger("walk");
				anim.SetInteger ("AnimState", 2);
				rb2DplayerWolf.MovePosition (Vector2.MoveTowards (playerWolf.transform.position, targetPos, speed * Time.deltaTime));
				
				if (playerWolf.transform.localScale.x < 0)
					playerWolf.transform.localScale = new Vector3 (1, 1, 1);
				
			} else if (targetPos.x < transform.position.x) {
				//anim.SetTrigger("walkLeft");
				anim.SetInteger ("AnimState", 1);
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
			
			
			if (Time.time > startHowlTime) 
			{
				//HowlAttractCollider.enabled = false;
			} 
			
			
			
		}//end of touchcount
		else 
		{
			anim.SetInteger("AnimState", 0);
		}	
		
	}//end of update. Now fixedUpdate
	
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






