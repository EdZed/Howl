 using UnityEngine;
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
	CircleCollider2D HowlAttractCollider;
	public float HowlRadiusMax;
	public float HowlRadiusRate;
	GameObject HowlSprite;
	Vector3 HowlSpriteRate = Vector3.one * 0.066f;
	SpriteRenderer WolfSprRend;
	
	public bool walking;
	public bool running;
	public bool objMoving;
	public bool canMove = true;
	bool prepAttack = false;
	public bool attacking;

	float currTime;
	public float atkBuildTime = 2f;
	public float atkDist = 5f;
	public float atkSpeed = 10f;
	int atkDmg = 0;
	Color startColor;

	public int playerHealth = 3;


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
		//HowlAttract = GameObject.Find("HowlAttract");
		HowlAttract = transform.FindChild ("HowlAttract").gameObject;
		HowlAttractCollider = HowlAttract.GetComponent <CircleCollider2D> ();
		WolfSprRend = GetComponent<SpriteRenderer> ();

		HowlAttractCollider.radius = 0f;
		HowlSprite = HowlAttract.transform.FindChild ("HowlSprite").gameObject;
		HowlSprite.transform.localScale = Vector3.zero;
		startColor = playerWolf.GetComponent<SpriteRenderer> ().color;

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

		//if ((howling && !running) || (howling && !walking)) {
		if (howling) {
			//howl sfx starts
			HowlSFX();
			canMove = false;

			if(running){
				anim.SetInteger("AnimState", 6);
				//anim.SetInteger("AnimState", 7);
			} else if (walking) {
				anim.SetInteger("AnimState", 6);
				//anim.SetInteger("AnimState", 2);
			} else {
				anim.SetInteger("AnimState", 6);
			}

			if (HowlAttractCollider.radius < HowlRadiusMax){
				HowlAttractCollider.radius += HowlRadiusRate;
				HowlSprite.transform.localScale += HowlSpriteRate;
			}
			
			if(HowlAttractCollider.radius >= HowlRadiusMax){
				HowlAttractCollider.radius = 0f;
				HowlSprite.transform.localScale = Vector3.zero;
				howling = false;
				canMove = true;
				//howling sfx stops
				//StopHowlSFX();
			}
		}
		if (Input.GetKeyUp (KeyCode.UpArrow)||
		    Input.GetKeyUp(KeyCode.DownArrow)||
		    Input.GetKeyUp(KeyCode.LeftArrow)|| 
		    Input.GetKeyUp(KeyCode.RightArrow)||
		    Input.GetKeyUp(KeyCode.W)||
		    Input.GetKeyUp(KeyCode.A)||
		    Input.GetKeyUp(KeyCode.S)|| 
		    Input.GetKeyUp(KeyCode.D)||
		    Input.GetButtonUp("Gamepad_Mac_HorizontalLeft")||
		    Input.GetButtonUp("Gamepad_Mac_HorizontalRight")||
		    Input.GetButtonUp("Gamepad_Mac_VerticalUp")||
		    Input.GetButtonUp("Gamepad_Mac_VerticalDown") ){
			StopSFX();
			anim.SetInteger("AnimState", 0);
			//print ("Player is idle!");
			walking = false;
			running = false;
		}

		if(Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Gamepad_Mac_Howl") ) {
			howling = true;

		}
		if (Input.GetKeyUp (KeyCode.LeftAlt)) {
			if (prepAttack){
				attacking = true;
			} else {
				currTime = Time.time;
				prepAttack = true;
				targetPos = playerWolf.transform.position;
				if (playerWolf.transform.localScale.x < 0) {
					targetPos.x -= atkDist;
				} else if (playerWolf.transform.localScale.x > 0) {
					targetPos.x += atkDist;
				}
			}
		}

		if (attacking) {
			anim.SetInteger("AnimState", 5);
			playerWolf.transform.position = Vector3.MoveTowards(playerWolf.transform.position, targetPos, atkSpeed * Time.deltaTime);
		}
		if (playerWolf.transform.position == targetPos) {
			attacking = false;
			targetPos = Vector3.zero;
			anim.SetInteger("AnimState", 0);
			Debug.Log("Attack for " + atkDmg + " damage!");
			atkDmg = 0;
			playerWolf.GetComponent<SpriteRenderer>().color = startColor;
			canMove = true;
			prepAttack = false;
		}
		
		if (prepAttack) {
			canMove = false;
			if(Time.time >= currTime + atkBuildTime){
				if (atkDmg >=5){
					atkDmg = 5;
				} else {
					atkDmg++;
				}
				Color myOrgColor = playerWolf.GetComponent<SpriteRenderer>().color;
				myOrgColor.r += 0.4f;
				playerWolf.GetComponent<SpriteRenderer>().color = myOrgColor;
				currTime = Time.time;
			}
		}

		if (canMove) {
//			if (howling == true){
//				canMove = false;
//			} else if (howling == false){
//				canMove = true;
//			}
			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A) || Input.GetButton("Gamepad_Mac_HorizontalLeft")) {
				WolfMoveToLeft ();

				if (Input.GetKey (KeyCode.LeftShift)|| Input.GetButton("Gamepad_Mac_Run") ) {
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
			if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D) || Input.GetButton("Gamepad_Mac_HorizontalRight")) {
				WolfMoveToRight ();
				if (Input.GetKey (KeyCode.LeftShift) || Input.GetButton("Gamepad_Mac_Run") ) {
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
			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W) || Input.GetButton("Gamepad_Mac_VerticalUp")) {
				if (Input.GetKey (KeyCode.LeftShift) || Input.GetButton("Gamepad_Mac_Run") ) {
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
			if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S) || Input.GetButton("Gamepad_Mac_VerticalDown")) {
				if (Input.GetKey (KeyCode.LeftShift) || Input.GetButton("Gamepad_Mac_Run") ) {
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
		}
		if (playerHealth == 1) {
			Color myHurtColor = playerWolf.GetComponent<SpriteRenderer> ().color;
			//myHurtColor.r += 0.4f;

			myHurtColor = Color.Lerp (startColor, Color.white, 4);
			playerWolf.GetComponent<SpriteRenderer> ().color = myHurtColor;
		} else {
			playerWolf.GetComponent<SpriteRenderer> ().color = startColor;

		}

		if (playerHealth == 0) {
			Application.LoadLevel ("Howl PS Demo");
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

	void HowlSFX() {
		if (!sources [2].isPlaying) 
		{
			//audio.Play ();
			sources [2].Play ();
		}
	}

	void StopHowlSFX() {
			//audio.Play ();
			sources [2].Stop ();
		
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






