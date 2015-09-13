using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class WolfInput : MonoBehaviour 
{

	//int damage = 1;
	float speed;
	float attackSpeed = 200f;
	float moveSpeed = 20f;
	public float tapMaxMovement = 50f;
	bool isTap = false;
	float touchStartTime = 0;
	public float tapTimerMax = 3.25f;//0.75f;
	Vector3 targetPos = Vector3.zero;
	bool attacking = false;
	private Animator anim;
	public Rigidbody2D rb2DplayerWolf;
	public GameObject playerAttackCollider;
	public BoxCollider2D attackCollider;
	public GameObject playerWolf;

	private GameObject WolfPack;
	private GameObject Wolf1;
	private GameObject Wolf2;
	private GameObject Wolf3;
	private GameObject FollowPlayer1;
	private GameObject FollowPlayer2;
	private GameObject FollowPlayer3;

	//public AudioSource[] WolfMusic;
	//public AudioSource WolfWalk;
	//public AudioSource WolfAttack;
	//public AudioClip WolfWalk;
	//public AudioClip WolfAttack;
	//public AudioClip WolfAttack;
	//public AudioSource WolfSource;
	public AudioSource au_WolfAttack;
	public AudioClip ac_WolfAttack;

	// Use this for initialization
	void Start () 
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		//Screen.orientation = ScreenOrientation.AutoRotation;
		anim = GetComponent<Animator> ();
		playerWolf = GameObject.Find("playerWolf");
		//anim.SetTrigger("stand");
		anim.SetInteger ("AnimState", 0);
		rb2DplayerWolf = playerWolf.GetComponent<Rigidbody2D>();
		playerAttackCollider = GameObject.Find("playerAttackCollider");
		attackCollider = playerAttackCollider.GetComponent <BoxCollider2D> ();
		//attackCollider.enabled = false;

//		WolfPack = GameObject.Find("WolfPack");
//		FollowPlayer1 = GameObject.Find("FollowPlayer1");
//		FollowPlayer2 = GameObject.Find("FollowPlayer2");
//		FollowPlayer3 = GameObject.Find("FollowPlayer3");
//		Wolf1 = GameObject.Find("/WolfPack/Wolf1");
//		Wolf2 = GameObject.Find("/WolfPack/Wolf2");
//		Wolf3 = GameObject.Find("/WolfPack/Wolf3");

		attackCollider.enabled = false;

		au_WolfAttack = playerWolf.GetComponent<AudioSource> ();
		ac_WolfAttack = playerWolf.GetComponent<AudioClip> ();

		//WolfWalk = GetComponents<AudioSource>();
		//WolfAttack = GetComponents<AudioSource>();
		//playerWolf.
		//WolfAttack = GetComponents<AudioSource> ();
		//WolfWalk = GetComponent<AudioSource> ("Wolf Walk");
		//WolfAttack = GetComponent<AudioSource> ("Wolf Attack");
		//WolfSource = playerWolf.GetComponent<AudioSource> ();
		//WolfSource.clip = WolfAttack;
	
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
				isTap = true;
				touchStartTime = Time.time + tapTimerMax;
				speed = 0;
				targetPos = Camera.main.ScreenToWorldPoint (touch.position);
				//print ("wolf started!");
				break;
			case TouchPhase.Stationary:
				if (Time.time > touchStartTime) 
				{
					isTap = false;
					speed = moveSpeed;
				} else {
					speed = 0;
				}
				break;
			case TouchPhase.Moved:
				//add dead zone down the line
				isTap = false;
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

			case TouchPhase.Canceled:
				if (isTap) 
				{
					attacking = true;
					speed = attackSpeed;
					targetPos = Camera.main.ScreenToWorldPoint (touch.position);
					//anim.SetInteger ("AnimState", 3);
					//print ("wolf attack animation!");
				} else {
					speed = 0;
				}
				anim.SetInteger ("AnimState", 0);

				break;

			}//end of switch touch.phase

			if (attacking) 
			{
				
				//if(readyToAttack == true)
				if(isTap == true){

					//Audio.Pla
					//WolfSource.Play();
					//print ("wolf music Attack!");
					if (targetPos.x > transform.position.x) {
						//anim.SetTrigger("walk");
						anim.SetInteger ("AnimState", 4);

						//print ("tapped right");
						//remove here and next one to see pounce w/o movement
						//rb2DplayerWolf.MovePosition (Vector2.MoveTowards (gameObject.transform.position, targetPos, speed * Time.deltaTime));
						if (transform.localScale.x < 0)
							transform.localScale = new Vector3 (1, 1, 1);
					} else if (targetPos.x < transform.position.x) {
						//anim.SetTrigger("walkLeft");
						anim.SetInteger ("AnimState", 3);
						//print ("tapped left");
						//rb2DplayerWolf.MovePosition (Vector2.MoveTowards (gameObject.transform.position, targetPos, speed * Time.deltaTime));
						
						if (transform.localScale.x > 0)
							transform.localScale = new Vector3 (-1, 1, 1);	
					} else if (Input.touchCount < 0) {
						//anim.SetTrigger("stand");
						//				anim.SetBool ("walk", false);
						/*anim.SetInteger ("AnimState", 0);
										print ("wolf stand!");
										didnt works because touchcount > 0 not only in this if statement but in the previous one
					 					*/
					}

				}
				
				attacking = false;
				//print ("attacking false!");
				//speed = attackSpeed;
				//anim.SetInteger ("AnimState", 3);
				//targetPos = transform.position;
				//print ("wolf attack!");
			}//end if(attacking)

			//remove "else" if run into issues
			else if(isTap == false) 
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
			
		}//end of touchcount
		else 
		{
			anim.SetInteger("AnimState", 0);
		}	
	
	}//end of update. Now fixedUpdate

	void Attack(){
		//readyToAttack = true;
		attacking = true;
		//print ("attack true again!");
		if (attacking) {
			//audio.PlayOneShot(au_WolfAttack, 0.7F);
			//au_WolfAttack.PlayOneShot(ac_WolfAttack, 0.74F);
			//au_WolfAttack.Play();
			//print ("wolf  music attack!");
			speed = attackSpeed;
			rb2DplayerWolf.MovePosition (Vector2.MoveTowards (playerWolf.transform.position, targetPos, speed * Time.deltaTime));
			//print ("wolf attack!");

			//Wolf1.GetComponent<Rigidbody2D>().MovePosition (Vector2.MoveTowards (Wolf1.transform.position, FollowPlayer1.transform.position, speed * Time.deltaTime));
			//Wolf2.GetComponent<Rigidbody2D>().MovePosition (Vector2.MoveTowards (Wolf2.transform.position, FollowPlayer2.transform.position, speed * Time.deltaTime));
			//Wolf3.GetComponent<Rigidbody2D>().MovePosition (Vector2.MoveTowards (Wolf3.transform.position, FollowPlayer3.transform.position, speed * Time.deltaTime));

			attacking = false;
			//print ("attack false again!");
		} 
	}

	void AttackTrigger()
	{
		attackCollider.enabled = true;
		print ("attack collider on!");

	}

	void AttackTriggerOff()
	{
		attackCollider.enabled = false;
		print ("attack collider off!");
		
	}

} //end of whole class***






