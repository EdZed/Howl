﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

[RequireComponent(typeof(AudioSource))]

public class PCWolfInput : MonoBehaviour 
{
	//  Static keyword makes this variable a Member of the class, not of any particular instance.
	public static float speed = 0f;
	public static float idleSpeed = 0f;
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
	public bool damaged;
	public bool invincible;

	public bool runAtk;
	public bool affection;
	public GameObject ColliderRunAtkGO;
	public BoxCollider2D runAtkCollider;

	public GameObject affectionColliderGO;
	public BoxCollider2D affectionCollider;

	float currTime;
	public float atkBuildTime = 2f;
	public float atkDist = 5f;
	public float atkSpeed = 10f;
	int atkDmg = 0;
	Color startColor;
	public int playerHealth = 3;

	public GameObject playerWolfShadow;

	//***Warmth Meter***
	//float playerRunMeter;
	public int startingWarmthMeter = 30;
	public int currentWarmth;
	public Slider WarmthSlider;
	public Image coldImage;
	public float flashSpeed = 5f;
	//color(R,G,B, Alpha)
	public Color flashColor = new Color (1f, 0f, 0f, 0.1f);

	//works as isDead bool in tut
	bool WarmthMeterEmpty;
	//works as take damage in tut
	bool gettingCold;

	Vector3 currentPosition;

	public delegate void ClickLeft();
	//OnClickLeft is an event variable
	public static event ClickLeft OnClickLeft;

	public delegate void ClickRight();
	public static event ClickRight OnClickRight;

	//walk
	public delegate void WalkAnim();
	public static event WalkAnim OnWalkAnim;
	//run
	public delegate void RunAnim();
	public static event RunAnim OnRunAnim;
	//idle
	public delegate void IdleAnim();
	public static event IdleAnim OnIdleAnim;
	//howl
	public delegate void HowlAnim();
	public static event HowlAnim OnHowlAnim;

	//#if UNITY_EDITOR || UNITY_WEBPLAYER
	private Vector3 moveDirection;
	Vector3 targetPoint;
	//#endif

	//changed 1 to 2 in editor
	public AudioSource[] sources = new AudioSource[1];

	RaycastHit2D hit;
	bool howling = false;
	bool leftPressed = false;
	bool rightPressed = false;
	bool upPressed = false;
	bool downPressed = false;

	public string currLevel;

	//Damage flash and invincibilty frames. 
	Color dmgColor;
	public float dmgTimeStart;
	float timeRatio = 0.3f;
	float dmgTimeLength;
	float invincTimeLength; //MUST be larger than dmgTimeLength

	//turns back on and off when done healing
	public bool callOnce;
	public bool howlSpriteOnce = false;

	//public bool isWarmingUp;
	public float warmthTemp = 50f; public float maxWarmthTemp = 50f;
	float WarmingRate = 0.1f;
	public bool WarmingUp = false;
	public float WolfEnterTime; float WolfWarmingTime = 0.075f;
	public float coolingTime; float coolingRate = 0.1f;

	Vector2 movementDir;

	//public bool inSpiritWorld; bool worldSwitch = false; //False = Real World, True = Spirit world 
	
	// Use this for initialization
	void Start () 
	{
		dmgTimeLength = 5f * timeRatio;
		invincTimeLength = dmgTimeLength + (3f * timeRatio);

		currLevel = Application.loadedLevelName;
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		//Screen.orientation = ScreenOrientation.AutoRotation;
		WolfSprRend = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		playerWolf = GameObject.Find("playerWolf");
		MainCam = GameObject.Find("Main Camera");
		//anim.SetFloat ("PlayerAnimState", 0);
		anim.SetBool("walking", false);
		anim.SetBool("running", false);
		anim.SetFloat("Speed", 0f);
		//This bottom code chunk causes the player running anim to play in the beginning when the scene restarts, and makes player invisible
//		if (OnIdleAnim != null) {
//			OnIdleAnim ();
//		}
		rb2DplayerWolf = playerWolf.GetComponent<Rigidbody2D>();
		//HowlAttract = GameObject.Find("HowlAttract");
		HowlAttract = transform.FindChild ("HowlAttract").gameObject;
		HowlAttractCollider = HowlAttract.GetComponent <CircleCollider2D> ();


		HowlAttractCollider.radius = 0f;
		HowlSprite = HowlAttract.transform.FindChild ("HowlSprite").gameObject;
		HowlSprite.transform.localScale = Vector3.zero;
		HowlAttractCollider.enabled = false;
		startColor = playerWolf.GetComponent<SpriteRenderer> ().color;

		playerWolfShadow = GameObject.Find("playerWolfShadow");
		running = false;
		//playerRunMeter = 30f;
		//RunMeterEmpty = false;
		currentWarmth = startingWarmthMeter;
		WarmthMeterEmpty = false;

		ColliderRunAtkGO = transform.FindChild ("playerWolfCollRunAtk").gameObject;
		runAtkCollider = ColliderRunAtkGO.GetComponent <BoxCollider2D> ();
		runAtkCollider.enabled = false;

		runAtk = false;

		affectionColliderGO = transform.FindChild ("playerWolfCollAffection").gameObject;
		affectionCollider = affectionColliderGO.GetComponent <BoxCollider2D> ();
		//affectionCollider.enabled = false;

		affection = false;
		callOnce = false;
		//isWarmingUp = false;

		//inSpiritWorld = false;

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
		//****
		//Debug.Log (currLevel);


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
////				anim.SetFloat ("PlayerAnimState", 5);
////				//anim.SetFloat ("PlayerAnimState", 3);
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
//			//anim.SetFloat ("PlayerAnimState", 0);
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

//		if (playerHealth == 1) {
//			Color myHurtColor = playerWolf.GetComponent<SpriteRenderer> ().color;
//			//myHurtColor.r += 0.4f;
//			
//			myHurtColor = Color.Lerp (startColor, Color.white, 4);
//			playerWolf.GetComponent<SpriteRenderer> ().color = myHurtColor;
//		} else {
//			playerWolf.GetComponent<SpriteRenderer> ().color = startColor;
//		}

		//arrow keys controls


//		if (!callOnce) {
//			InvokeRepeating("OutInCold", 1,1);
//			callOnce = true;
//		}

//		if (gettingCold) {
//			coldImage.color = flashColor;
//		} else {
//			coldImage.color = Color.Lerp (coldImage.color, Color.clear, flashSpeed * Time.deltaTime);
//		}
//		gettingCold = false;


		float input_x = Input.GetAxisRaw("Horizontal");
		float input_y = Input.GetAxisRaw("Vertical");

		if(input_x != 0 || input_y !=0){
			movementDir = new Vector2(input_x, input_y);

//			if(running){
//				speed = runSpeed;
//			} else{
//				speed = moveSpeed;
//			}
			//speed = running ? runSpeed : moveSpeed;

			//might have to use if block for run and walk since gotta move the info from below into here
			if(running) {
				
				anim.SetBool("running", true);
				//running anim left/right
				RunSFX();
				if (OnRunAnim != null) {
					//sends message to ?
					OnRunAnim ();
				}
				//			if (playerRunMeter > 0){
				//				playerRunMeter--;
				//			} else {
				//				playerRunMeter = 0;
				//			}
				//
				//			if (playerRunMeter <= 0){
				//				//code next line out to deactivate meter
				//				RunMeterEmpty = true;
				//			}
				//if guiding Orange lost wolf, turn on break ability while running
				//turning on from HowlAttractPowers Script
				if(runAtk == true){
					runAtkCollider.enabled = true;
					Color myOrgColor = playerWolf.GetComponent<SpriteRenderer>().color;
					
					//Makes orange color to match wolf and destructible orange objects
					myOrgColor.r += 1.4f;
					myOrgColor.b -= 1.4f;
					
					playerWolf.GetComponent<SpriteRenderer>().color = myOrgColor;
					//print("runAttack Collider on");
					
				} else{
					//runAtkCollider.enabled = false;
					//playerWolf.GetComponent<SpriteRenderer>().color = startColor;
				}
			} else if (walking) {
				
				anim.SetBool("walking", true);
				WalkSFX();
				if (OnWalkAnim != null) {
					OnWalkAnim ();
				}
				if(runAtk == true){
					runAtkCollider.enabled = false;
					//player goes from orange to normal color when walking
					playerWolf.GetComponent<SpriteRenderer>().color = startColor;
					//print("runAttack Collider off");
				}
			} else {
				//doesn't work
				speed = idleSpeed;
				StopSFX();
				if(runAtk == true){
					runAtkCollider.enabled = false;
					//player goes from orange to normal color when standing
					playerWolf.GetComponent<SpriteRenderer>().color = startColor;
					//print("runAttack Collider off");
				}
				if (OnIdleAnim != null) {
					//sends message to Playerwolfshadow script
					OnIdleAnim ();
				}
			}

		} else{
			speed = idleSpeed;
		}

		//Bring input stuff first in update before the logic script
		anim.SetFloat("x", movementDir.x);
		anim.SetFloat("y", movementDir.y);

		transform.position += new Vector3(input_x, input_y, 0).normalized * speed * Time.deltaTime;
		anim.SetFloat("Speed", speed);



		if (Input.GetKey (KeyCode.G)) {
			Application.LoadLevel (currLevel);
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
		    Input.GetButtonUp("Gamepad_Mac_VerticalDown") ||
		    leftPressed == false && rightPressed == false &&
		    upPressed == false && downPressed == false && howling == false){
			StopSFX();
			anim.SetFloat("PlayerAnimState", 0);
			if (OnIdleAnim != null) {
				OnIdleAnim ();
			}
			//print ("Player is idle!");
			walking = false;
			running = false;
		}
		
		if(Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Gamepad_Mac_Howl") ) {
			howling = true;
			anim.SetBool("howling", true);
			//StartCoroutine("PlayerHowling");
			HowlAttractCollider.enabled = true;
		}
		
		//Affection
		//		if(Input.GetKey(KeyCode.V) ) {
		//			anim.SetFloat("PlayerAnimState", 1);
		//			Debug.Log ("affection");
		//			affection = true;
		//			//affectionCollider.enabled = true;
		//			//HowlAttractCollider.enabled = true;
		//		}
		//		if(Input.GetKeyUp(KeyCode.V) ) {
		//			//anim.SetFloat("PlayerAnimState", 1);
		//			//Debug.Log ("affection");
		//			AffectionFalse();
		//			//HowlAttractCollider.enabled = true;
		//		}
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
			anim.SetFloat("PlayerAnimState", 5);
			playerWolf.transform.position = Vector3.MoveTowards(playerWolf.transform.position, targetPos, atkSpeed * Time.deltaTime);
		}
		if (playerWolf.transform.position == targetPos) {
			attacking = false;
			targetPos = Vector3.zero;
			anim.SetFloat("PlayerAnimState", 0);
			//Debug.Log("Attack for " + atkDmg + " damage!");
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
			if (Input.GetKey (KeyCode.LeftArrow) || 
			    Input.GetKey (KeyCode.A) || 
			    Input.GetButton("Gamepad_Mac_HorizontalLeft") || 
			    Input.GetAxisRaw ("Gamepad_PC_Horizontal") < 0) {
				WolfMoveToLeft ();
				leftPressed = true;
				anim.SetFloat ("PlayerAnimState", 1);
				if (Input.GetKey (KeyCode.LeftShift)|| Input.GetButton("Gamepad_Mac_Run") ) {
					//anim.SetFloat ("PlayerAnimState", 6);
					//if (!RunMeterEmpty){
					if (WarmthMeterEmpty == false){
						running = true;
						walking = false;
						speed = runSpeed;
						//anim.SetFloat ("PlayerAnimState", 6);
					} else {
						running = false;
						walking = true;
						speed = moveSpeed;
						//anim.SetFloat ("PlayerAnimState", 1);
					}
					//transform.position += Vector3.left * speed * Time.deltaTime;
				} else {
					running = false;
					walking = true;
					speed = moveSpeed;
					//transform.position += Vector3.left * speed * Time.deltaTime;
				}
			} else{
				leftPressed = false;
			}
			if (Input.GetKey (KeyCode.RightArrow) || 
			    Input.GetKey (KeyCode.D) || 
			    Input.GetButton("Gamepad_Mac_HorizontalRight") ||
			    Input.GetAxisRaw ("Gamepad_PC_Horizontal") > 0) {
				WolfMoveToRight ();
				//anim.SetFloat ("PlayerAnimState", 1);
				rightPressed = true;
				if (Input.GetKey (KeyCode.LeftShift) || Input.GetButton("Gamepad_Mac_Run") ) {
					if (!WarmthMeterEmpty){
						running = true;
						walking = false;
						speed = runSpeed;
						//anim.SetFloat ("PlayerAnimState", 6);
					} else {
						running = false;
						walking = true;
						speed = moveSpeed;
						//anim.SetFloat ("PlayerAnimState", 1);
					}
					//transform.position += Vector3.right * speed * Time.deltaTime;
				} else {
					running = false;
					walking = true;
					speed = moveSpeed;
					//anim.SetFloat ("PlayerAnimState", 1);
					//transform.position += Vector3.right * speed * Time.deltaTime;
				}
			} else{
				rightPressed = false;
			}
			if (Input.GetKey (KeyCode.UpArrow) || 
			    Input.GetKey (KeyCode.W) || 
			    Input.GetButton("Gamepad_Mac_VerticalUp") ||
			    Input.GetAxisRaw ("Gamepad_PC_Vertical") > 0) {
				
				upPressed = true;
				//anim.SetFloat ("PlayerAnimState", 2);
				if (Input.GetKey (KeyCode.LeftShift) || Input.GetButton("Gamepad_Mac_Run") ) {
					if (!WarmthMeterEmpty){
						running = true;
						walking = false;
						speed = runSpeed;
						//anim.SetFloat ("PlayerAnimState", 7);
					} else {
						running = false;
						walking = true;
						speed = moveSpeed;
						//anim.SetFloat ("PlayerAnimState", 2);
					}
					//transform.position += Vector3.up.normalized * speed * Time.deltaTime;
				} else {
					running = false;
					walking = true;
					speed = moveSpeed;
					//anim.SetFloat ("PlayerAnimState", 2);
					//transform.position += Vector3.up.normalized * speed * Time.deltaTime;
				}
			} else{
				upPressed = false;
			}
			if (Input.GetKey (KeyCode.DownArrow) || 
			    Input.GetKey (KeyCode.S) || 
			    Input.GetButton("Gamepad_Mac_VerticalDown") ||
			    Input.GetAxisRaw ("Gamepad_PC_Vertical") < 0) {
				downPressed = true;
				//anim.SetFloat ("PlayerAnimState", 3);
				if (Input.GetKey (KeyCode.LeftShift) || Input.GetButton("Gamepad_Mac_Run") ) {
					if (!WarmthMeterEmpty){
						running = true;
						walking = false;
						speed = runSpeed;
						//anim.SetFloat ("PlayerAnimState", 8);
					} else {
						running = false;
						walking = true;
						speed = moveSpeed;
						//anim.SetFloat ("PlayerAnimState", 3);
					}
					//transform.position += Vector3.down.normalized * speed * Time.deltaTime;
				} else {
					running = false;
					walking = true;
					speed = moveSpeed;
					//anim.SetFloat ("PlayerAnimState", 3);
					//transform.position += Vector3.down.normalized * speed * Time.deltaTime;
				}
			} else{
				downPressed = false;
			}
		}
		
		if (damaged) {
			//Debug.Log ("player health:" + playerHealth);
			invincible = true;
			if (Time.time <= dmgTimeStart + dmgTimeLength) {
				StartCoroutine(PlayerHurtFlash());
				//PlayerHurtFlash ();
			} else {
				damaged = false;
			}
		}
		
		if (playerHealth == 0) {
			Application.LoadLevel (currLevel);
		}



		if (!running) {
			anim.SetBool("running", false);
//			if (playerRunMeter < 30f){
//				if (RunMeterEmpty){
//					playerRunMeter +=0.2f;
//					//playerRunMeter +=0.8f;
//				} else if (!RunMeterEmpty){
//					playerRunMeter +=0.5f;
//					//playerRunMeter +=1.0f;
//				}
//			}
//			if (playerRunMeter >= 30f){
//				playerRunMeter = 30f;
//				RunMeterEmpty = false;
//			}

		}
		if(!walking){
			anim.SetBool("walking", false);
		}

		//if ((howling && !running) || (howling && !walking)) {
		if (howling) {
			//howl sfx starts
			HowlSFX();
			canMove = false;

			if(running){
				if (OnHowlAnim != null) {
					OnHowlAnim ();
				}
			} else if (walking) {
				if (OnHowlAnim != null) {
					OnHowlAnim ();
				}
			} else {
				if (OnHowlAnim != null) {
					OnHowlAnim ();
				}
			}
			if(howlSpriteOnce == false){
				if (HowlAttractCollider.radius < HowlRadiusMax){
					HowlAttractCollider.radius += HowlRadiusRate;
					HowlSprite.transform.localScale += HowlSpriteRate;
				}
			}
			
			if(HowlAttractCollider.radius >= HowlRadiusMax){
				HowlAttractCollider.radius = 0f;
				HowlSprite.transform.localScale = Vector3.zero;
//				howling = false;
//				canMove = true;
				HowlAttractCollider.enabled = false;
				howlSpriteOnce = true;
				//HowlFalse();
				//howling sfx stops
				//StopHowlSFX();
			}
		}

		WolfWarmthSystem ();

		//Debug.Log ("player hit equals:" + damaged);
		//Debug.Log (playerRunMeter);

	}//end of update. Now fixedUpdate
	//Vector3 target = moveDirection * speed + currentPosition;
	//transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );
	
	//#endif

//	IEnumerator OutinCold(){
//		
//		yield return new WaitForSeconds(2);
//	}
	//*** METHODS ****

//	IEnumerator PlayerHowling(){
//		//sfx here
//		yield return new WaitForSeconds(2);
//		howling = false;
//
//	}

	void OutInCold(){
		
		//InvokeRepeating("TakeDamage", 1,1);
		TakeCold (1);
	}

	public void TakeCold (int amount){

		currentWarmth -= amount;
		WarmthSlider.value = currentWarmth;

		gettingCold = true;


		if(currentWarmth <= 0 && !WarmthMeterEmpty){
			WarmthMeterEmpty = true;
		}
	}

	public void TakeWarm (int amount){
		
		//gettingCold = true;
		currentWarmth += amount;
		WarmthSlider.value = currentWarmth;

		if (currentWarmth >= 30){
			currentWarmth = 30;
			WarmthSlider.value = currentWarmth;
			WarmthMeterEmpty = false;
			
		}
	}

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

	void HowlFalse() {
		howling = false;
		anim.SetBool("howling", false);
		canMove = true;
		howlSpriteOnce = false;
		//HowlAttractCollider.enabled = false;
		HowlSprite.transform.localScale = Vector3.zero;
		//print ("howl is false now");
	}

	void AffectionFalse() {
		affection = false;
		canMove = true;
		//affectionCollider.enabled = false;
		//print ("howl is false now");
	}

	IEnumerator PlayerHurtFlash(){
		Color myOrgColor = this.GetComponent<SpriteRenderer> ().color;
		float colorConstrain = Mathf.Sin((Time.time - dmgTimeStart / dmgTimeStart + dmgTimeLength - dmgTimeStart) / 2 - 0.5f) * 2;
		float colorSpd = 1 - myOrgColor.r;

		myOrgColor.r += colorSpd * colorConstrain;
		this.GetComponent<SpriteRenderer> ().color = myOrgColor;
		//Debug.Log(this.GetComponent<SpriteRenderer>().color);
		yield return new WaitForSeconds(2);
		if (playerHealth == 1) {
			Color myHurtColor = playerWolf.GetComponent<SpriteRenderer> ().color;
			//myHurtColor.r += 0.4f;
			
			myHurtColor = Color.Lerp (startColor, Color.white, 4);
			playerWolf.GetComponent<SpriteRenderer> ().color = myHurtColor;
		} else {
			playerWolf.GetComponent<SpriteRenderer> ().color = startColor;
		}
		invincible = false;
		damaged = false;
		//Debug.Log ("Player invincible:" + invincible);
	}

	void OnEnable()
	{
		WolfDenManager.OnHeal += PlayerHeal;
		AffectionTrigger.OnPlayerClose += PlayerWarmUp;
		//AffectionTrigger.OnPlayerAway += PlayerStopWarm;
	}
	
	
	void OnDisable()
	{
		WolfDenManager.OnHeal -= PlayerHeal;
		AffectionTrigger.OnPlayerClose -= PlayerWarmUp;
		//AffectionTrigger.OnPlayerAway -= PlayerStopWarm;
	}

	void PlayerWarmUp()
	{
		//isWarmingUp = true;
		TakeWarm (1);
		//cancelinvoke??
	}

//	void PlayerStopWarm()
//	{
//		//isWarmingUp = false;
//		CancelInvoke
//	}
	
	void PlayerHeal()
	{
		//If health more than 1, color back to normal (blue)
		playerWolf.GetComponent<SpriteRenderer> ().color = startColor;
	}

	void WolfWarmthSystem(){
		float tempVignette;

		if (WarmingUp) {
			if (Time.time >= WolfEnterTime + WolfWarmingTime) {
				if (warmthTemp < maxWarmthTemp) {
					warmthTemp += WarmingRate;
					WolfEnterTime = Time.time;
				} else if (warmthTemp == maxWarmthTemp) {
					warmthTemp = maxWarmthTemp;
				}
			}
		} else if (!WarmingUp) {
			if (Time.time >= coolingTime + coolingRate){
				warmthTemp -= WarmingRate;
				coolingTime = Time.time;
			}
		}
		tempVignette = (maxWarmthTemp - warmthTemp) + 1.0f;
		if (tempVignette <= 1f) {
			tempVignette = 1f;
		}
	}

	
}//end of whole class***






