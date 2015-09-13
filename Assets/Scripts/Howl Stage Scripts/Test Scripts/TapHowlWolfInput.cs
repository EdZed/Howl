using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class TapHowlWolfInput : MonoBehaviour 
{
	float speed;
	float moveSpeed = 8f;
	public float tapMaxMovement = 50f;
	bool isTap = false;
	float touchStartTime = 0;
	float startHowlTime = 0;
	public float tapTimerMax = 3.25f;//0.75f;
	public float howlTimerMax = 8f;//0.75f;
	Vector3 targetPos = Vector3.zero;
	bool howling = false;
	private Animator anim;
	public Rigidbody2D rb2DplayerWolf;
	//public Rigidbody2D rb2DplayerWolfgetHit;
	public BoxCollider2D colplayerWolfgetHit;
	//public GameObject playerAttackCollider;
	//public BoxCollider2D attackCollider;
	public GameObject playerWolf;
	public GameObject playerWolfgetHit;
	
	public GameObject HowlAttract;
	public CircleCollider2D HowlAttractCollider;
	RaycastHit2D hit;
	
	// Use this for initialization
	void Start () 
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		//Screen.orientation = ScreenOrientation.AutoRotation;
		anim = GetComponent<Animator> ();
		playerWolf = GameObject.Find("playerWolf");
		playerWolfgetHit = GameObject.Find("playerWolfgetHit");
		//anim.SetTrigger("stand");
		anim.SetInteger ("AnimState", 0);
		rb2DplayerWolf = playerWolf.GetComponent<Rigidbody2D>();
		colplayerWolfgetHit = playerWolfgetHit.GetComponent<BoxCollider2D>();
		HowlAttract = GameObject.Find("HowlAttract");
		HowlAttractCollider = HowlAttract.GetComponent <CircleCollider2D> ();
		
		//HowlAttractCollider.enabled = false;
		
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
				if (!howling){
					isTap = true;
					touchStartTime = Time.time + tapTimerMax;
					speed = 0;
					targetPos = Camera.main.ScreenToWorldPoint (touch.position);
				}
				//print ("wolf started!");
				break;
			case TouchPhase.Stationary:
				if (howling){
					speed = 0;
				}
				else if (Time.time > touchStartTime) 
				{
					isTap = false;
					howling = false;
					speed = moveSpeed;
				} 
				
				else{
					speed = 0;
				}
				
				break;
			case TouchPhase.Moved:
				//add dead zone down the line
				if (howling){
					speed = 0;
				} else if(!howling){
					isTap = false;
					howling = false;
					speed = moveSpeed;
					targetPos = Camera.main.ScreenToWorldPoint (touch.position);
				}
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
				hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint((Input.GetTouch (0).position)), Vector2.zero);
				
				if (isTap && hit.collider != null && hit.collider.gameObject.tag == "LostWolf") 
				{
					//attacking = true;
					howling = true;
					speed = 0;
					//hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint((Input.GetTouch (0).position)), Vector2.zero);
					targetPos = Camera.main.ScreenToWorldPoint (touch.position);
					//anim.SetInteger ("AnimState", 3);
					//print ("wolf attack animation!");
				} else {
					speed = 0;
				}
				anim.SetInteger ("AnimState", 0);
				
				break;
				
			}//end of switch touch.phase
			
			//			if (howling) 
			//			{
			//
			//				if(isTap == true){
			//
			//					//hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint((Input.GetTouch (0).position)), Vector2.zero);
			//					
			//					//if (hit.collider != null && hit.collider.gameObject.tag == "LostWolf")
			//					//{
			//						
			//						//HowlAttractCollider.enabled = true;
			//						print ("howl anim started!");
			//
			//					if (targetPos.x > transform.position.x && hit.collider != null && hit.collider.gameObject.tag == "LostWolf") {
			//							//anim.SetTrigger("walk");
			//							anim.SetInteger ("AnimState", 6);
			//							
			//							//print ("tapped right");
			//							//remove here and next one to see pounce w/o movement
			//							//rb2DplayerWolf.MovePosition (Vector2.MoveTowards (gameObject.transform.position, targetPos, speed * Time.deltaTime));
			//							if (transform.localScale.x < 0)
			//								transform.localScale = new Vector3 (1, 1, 1);
			//					} else if (targetPos.x < transform.position.x && hit.collider != null && hit.collider.gameObject.tag == "LostWolf") {
			//							//anim.SetTrigger("walkLeft");
			//							anim.SetInteger ("AnimState", 5);
			//							//print ("tapped left");
			//							//rb2DplayerWolf.MovePosition (Vector2.MoveTowards (gameObject.transform.position, targetPos, speed * Time.deltaTime));
			//							
			//							if (transform.localScale.x > 0)
			//								transform.localScale = new Vector3 (-1, 1, 1);	
			//					} else if (targetPos.x > transform.position.x && hit.collider != null && hit.collider.gameObject.tag != "LostWolf" || targetPos.x < transform.position.x && hit.collider != null && hit.collider.gameObject.tag != "LostWolf"){
			//						speed = 0;
			//					}
			//					else if (Input.touchCount < 0) {
			//						//speed = moveSpeed;
			//					} 
			//					//} //end hit.collider LostWolf
			//				} //end isTap = true
			//
			//			}//end if(howling)
			
			//remove "else" if run into issues
			if(isTap == false ) 
			{
				if (targetPos.x > transform.position.x) 
				{
					//anim.SetTrigger("walk");
					anim.SetInteger ("AnimState", 2);
					rb2DplayerWolf.MovePosition (Vector2.MoveTowards (playerWolf.transform.position, targetPos, speed * Time.deltaTime));
					//Wolf1.GetComponent<Rigidbody2D>().MovePosition (Vector2.MoveTowards (Wolf1.transform.position, FollowPlayer1.transform.position, speed * Time.deltaTime));
					//Wolf2.GetComponent<Rigidbody2D>().MovePosition (Vector2.MoveTowards (Wolf2.transform.position, FollowPlayer2.transform.position, speed * Time.deltaTime));
					//Wolf3.GetComponent<Rigidbody2D>().MovePosition (Vector2.MoveTowards (Wolf3.transform.position, FollowPlayer3.transform.position, speed * Time.deltaTime));
					if (playerWolf.transform.localScale.x < 0)
						playerWolf.transform.localScale = new Vector3 (1, 1, 1);
					
					//Wolf1.transform.localScale = new Vector3 (1, 1, 1);
					//Wolf2.transform.localScale = new Vector3 (1, 1, 1);
					//Wolf3.transform.localScale = new Vector3 (1, 1, 1);
					
					
				} else if (targetPos.x < transform.position.x) {
					//anim.SetTrigger("walkLeft");
					anim.SetInteger ("AnimState", 1);
					rb2DplayerWolf.MovePosition (Vector2.MoveTowards (playerWolf.transform.position, targetPos, speed * Time.deltaTime));
					//Wolf1.GetComponent<Rigidbody2D>().MovePosition (Vector2.MoveTowards (Wolf1.transform.position, FollowPlayer1.transform.position, speed * Time.deltaTime));
					//Wolf2.GetComponent<Rigidbody2D>().MovePosition (Vector2.MoveTowards (Wolf2.transform.position, FollowPlayer2.transform.position, speed * Time.deltaTime));
					//Wolf3.GetComponent<Rigidbody2D>().MovePosition (Vector2.MoveTowards (Wolf3.transform.position, FollowPlayer3.transform.position, speed * Time.deltaTime));
					if (playerWolf.transform.localScale.x > 0)
						playerWolf.transform.localScale = new Vector3 (-1, 1, 1);	
					
					//Wolf1.transform.localScale = new Vector3 (-1, 1, 1);
					//Wolf2.transform.localScale = new Vector3 (-1, 1, 1);
					//Wolf3.transform.localScale = new Vector3 (-1, 1, 1);
				} else if (Input.touchCount < 0) {
					anim.SetInteger ("AnimState", 0);
					//anim.SetTrigger("stand");
					//				anim.SetBool ("walk", false);
					/*anim.SetInteger ("AnimState", 0);
										print ("wolf stand!");
										didnt work because touchcount > 0 not only in this if statement but in the previous one
					 					*/
				}
				
				
			}// end of isTap ==false
			
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
	
	void Howl(){
		startHowlTime = Time.time + howlTimerMax;
		howling = true;
		HowlAttractCollider.enabled = true;
		
		if (howling) {
			speed = 0;
			GetComponent<AudioSource> ().Play ();
			print ("howl sound started!");
		}
		//		if (Time.time > startHowlTime) 
		//		{
		//			HowlAttractCollider.enabled = false;
		//		} 
		
		//		else{
		//			speed = 0;
		//		}
	}
	
	void HowlEnd(){
		howling = false;
		//isTap = false;
		
		if (!howling) {
			speed = moveSpeed;
			GetComponent<AudioSource> ().Stop ();
			print ("howl sound ended!");
		}
	}
	
	
	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "Proximity") 
		{
			//if(attacking)
			//{
			//var explode = target.GetComponent<Explode>() as Explode;
			//explode.OnExplode();
			//Destroy(target.gameObject); 
			//anim.SetInteger ("AnimState", 5);
		} else {
			//anim.SetInteger("AnimState", 0);
		}	
	}
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
	
	void OnTriggerExit2D(Collider2D target){
		//readyToAttack = false;
		//attacking = false;
		//anim.SetInteger ("AnimState", 0);
	}
	
} //end of whole class***






