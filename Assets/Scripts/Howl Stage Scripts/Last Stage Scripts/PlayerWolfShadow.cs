using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]

public class PlayerWolfShadow : MonoBehaviour 
{
	float speed;
	float moveSpeed = 6f;
	float runSpeed = 12f;

	Vector3 targetPos = Vector3.zero;
	private Animator anim;
	public Rigidbody2D rb2DplayerWolf;
	public GameObject playerWolfShadow;
	public GameObject playerWolf;
	
	public bool walking;
	public bool running;
	#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE
	private Vector3 moveDirection;
	Vector3 targetPoint;
	#endif
	
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
		playerWolfShadow = GameObject.Find("playerWolfShadow");
		playerWolf = GameObject.Find("playerWolf");
		anim.SetInteger ("AnimState", 0);
		rb2DplayerWolf = playerWolfShadow.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	//Using fixed update instead for rigidbody use
	void Update () 
	{
		#if UNITY_IOS

		//float speed;
		if (Input.touchCount > 0) 
		{
			Touch touch = Input.touches [0];
			
			switch(touch.phase)
			{
			case TouchPhase.Began:
				targetPos = Camera.main.ScreenToWorldPoint (touch.position);
				if(Input.GetTouch(0).tapCount == 2){
					running = true;
					walking = false;
					speed = runSpeed;
					Debug.Log("DOUBLE TAP");
				}else{
					speed = 0;
					running = false;
					walking = true;
					//print ("wolf started!");
					
				}
				break;
			case TouchPhase.Stationary:
				if(running){
					speed = runSpeed;
				} else{
					speed = moveSpeed;
				}
				
				break;
			case TouchPhase.Moved:
				//add dead zone down the line
				targetPos = Camera.main.ScreenToWorldPoint (touch.position);
				if(running){
					speed = runSpeed;
				} else{
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
				
				//break;
				
			case TouchPhase.Canceled:
				
				speed = 0;
				anim.SetInteger ("AnimState", 0);
				
				break;
				
			}//end of switch touch.phase
			if(running){
				if (targetPos.x > transform.position.x) 
				{
					//anim.SetTrigger("walk");
					//run right
					anim.SetInteger ("AnimState", 7);
					//rb2DplayerWolf.MovePosition (Vector2.MoveTowards (playerWolfShadow.transform.position, playerWolf.transform.position, speed * Time.deltaTime));

					//playerWolfShadow.transform.localPosition

					if (playerWolfShadow.transform.localScale.x < 0)
						playerWolfShadow.transform.localScale = new Vector3 (-1, 1, 1);
					playerWolfShadow.transform.localRotation = Quaternion.Euler(58, 328, 0);
					playerWolfShadow.transform.localPosition = new Vector3 (-0.43f, -0.52f, 1);
					//playerWolfShadow.transform.localRotation = Quaternion.Euler(58, 328, 0);
					//Debug.Log("run right");
					
				} else if (targetPos.x < transform.position.x) {
					//anim.SetTrigger("walkLeft");
					//run left
					anim.SetInteger ("AnimState", 7);
					//rb2DplayerWolf.MovePosition (Vector2.MoveTowards (playerWolfShadow.transform.position, playerWolf.transform.position, speed * Time.deltaTime));
					
					if (playerWolfShadow.transform.localScale.x > 0){
						playerWolfShadow.transform.localScale = new Vector3 (1, 1, 1);	
						//playerWolfShadow.transform.localRotation = Quaternion.Euler(58, 328, 3);
						playerWolfShadow.transform.localPosition = new Vector3 (-0.15f, -0.52f, 1);
						playerWolfShadow.transform.localRotation = Quaternion.Euler(59.7f, 342, 354.65f);

					}
				} else if (Input.touchCount < 0) {
					anim.SetInteger ("AnimState", 0);
					//anim.SetTrigger("stand");
					//				anim.SetBool ("walk", false);
					/*anim.SetInteger ("AnimState", 0);
										print ("wolf stand!");
										didnt work because touchcount > 0 not only in this if statement but in the previous one
					 					*/
				}
			} else {
				if (targetPos.x > transform.position.x) 
				{
					//anim.SetTrigger("walk");
					//walk right
					anim.SetInteger ("AnimState", 2);
					//rb2DplayerWolf.MovePosition (Vector2.MoveTowards (playerWolfShadow.transform.position, playerWolf.transform.position, speed * Time.deltaTime));
					
					if (playerWolfShadow.transform.localScale.x < 0)
						playerWolfShadow.transform.localScale = new Vector3 (-1, 1, 1);
					playerWolfShadow.transform.localPosition = new Vector3 (-0.43f, -0.52f, 1);
					playerWolfShadow.transform.localRotation = Quaternion.Euler(58, 328, 0);

					
				} else if (targetPos.x < transform.position.x) {
					//anim.SetTrigger("walkLeft");
					//walk left
					anim.SetInteger ("AnimState", 1);
					//rb2DplayerWolf.MovePosition (Vector2.MoveTowards (playerWolfShadow.transform.position, playerWolf.transform.position, speed * Time.deltaTime));
					
					if (playerWolfShadow.transform.localScale.x > 0)
						playerWolfShadow.transform.localScale = new Vector3 (1, 1, 1);	
					playerWolfShadow.transform.localPosition = new Vector3 (-0.15f, -0.52f, 1);
					playerWolfShadow.transform.localRotation = Quaternion.Euler(59.7f, 342, 354.65f);
					
				} else if (Input.touchCount < 0) {
					anim.SetInteger ("AnimState", 0);
					//anim.SetTrigger("stand");
					//				anim.SetBool ("walk", false);
					/*anim.SetInteger ("AnimState", 0);
										print ("wolf stand!");
										didnt work because touchcount > 0 not only in this if statement but in the previous one
					 					*/
				}
			}
			
		}//end of touchcount
		else 
		{
			anim.SetInteger("AnimState", 0);
		}	
		#endif

		#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE
		Vector3 currentPosition = transform.position;

		if (playerWolf.GetComponent<PCWolfInput>().walking){
			walking = true;
			running = false;
		} else if (playerWolf.GetComponent<PCWolfInput>().running){
			running = true;
			walking = false;
		} else {
			running = false;
			walking = false;
		}

		if(running){
			anim.SetInteger ("AnimState", 7);
			if (targetPoint.x > transform.position.x) {
				if (playerWolfShadow.transform.localScale.x < 0){
					playerWolfShadow.transform.localScale = new Vector3 (-1, 1, 1);
				}
				playerWolfShadow.transform.localRotation = Quaternion.Euler(58, 328, 0);
				playerWolfShadow.transform.localPosition = new Vector3 (-0.43f, -0.52f, 1);
			} else if (targetPoint.x < transform.position.x) {
				if (playerWolfShadow.transform.localScale.x > 0){
					playerWolfShadow.transform.localScale = new Vector3 (1, 1, 1);	
					playerWolfShadow.transform.localPosition = new Vector3 (-0.15f, -0.52f, 1);
					playerWolfShadow.transform.localRotation = Quaternion.Euler(59.7f, 342, 354.65f);
				}
			}
		} else if (walking) {
			anim.SetInteger ("AnimState", 2);
			if (targetPoint.x > transform.position.x) {
				if (playerWolfShadow.transform.localScale.x < 0){
					playerWolfShadow.transform.localScale = new Vector3 (-1, 1, 1);
				}
				playerWolfShadow.transform.localPosition = new Vector3 (-0.43f, -0.52f, 1);
				playerWolfShadow.transform.localRotation = Quaternion.Euler(58, 328, 0);
			} else if (targetPoint.x < transform.position.x) {
				if (playerWolfShadow.transform.localScale.x > 0){
					playerWolfShadow.transform.localScale = new Vector3 (1, 1, 1);	
				}
				playerWolfShadow.transform.localPosition = new Vector3 (-0.15f, -0.52f, 1);
				playerWolfShadow.transform.localRotation = Quaternion.Euler(59.7f, 342, 354.65f);
			}
		} else {
			anim.SetInteger("AnimState", 0);
		}

		#endif
	}//end of update. Now fixedUpdate
	
} //end of whole class***






