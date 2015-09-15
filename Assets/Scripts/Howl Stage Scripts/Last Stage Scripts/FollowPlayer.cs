using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour 
{
	public BoxCollider2D LostWolfCollider;
	public GameObject LostWolfGO;
	public Rigidbody2D rb2DLostWolf;
	//player wolf
	public GameObject PlayerWolfGO;
	public BoxCollider2D PlayerWolfCollider;

	public GameObject followPlayerWolfGO;
	//public GameObject lostWolf2Pos;
	float speed;
	float moveSpeed = 7f;
	float runSpeed = 12f;
	float staySpeed = 0f;

	bool isFollowing;
	bool willFollow;
	public bool isTriggering;
	public bool isTriggeringDen;

	//Wolf Den Art
	public GameObject wolfDenArt;
	private Animator wolfDenAnim;
	private Animator LostWolfAnim;

	//PCWolfInput Script
//	private PCWolfInput PCWolfInputScript;
//	float playerSpeed;
//	float playerMoveSpeed;
//	float playerRunSpeed;

	// Use this for initialization
	void Start () 
	{
		LostWolfAnim = gameObject.GetComponent<Animator> ();
		LostWolfGO = this.gameObject;
		LostWolfCollider = GetComponent <BoxCollider2D> ();
		//LostWolfCollider.enabled = false;
		
		PlayerWolfGO = GameObject.Find("playerWolf");
		PlayerWolfCollider = PlayerWolfGO.GetComponent <BoxCollider2D> ();
		PlayerWolfCollider.enabled = true;

		followPlayerWolfGO = GameObject.Find("FollowPlayerWolf");
		
		speed = moveSpeed;
		rb2DLostWolf = GetComponent<Rigidbody2D> ();
		LostWolfAnim = GetComponent<Animator> ();
		LostWolfAnim.SetInteger ("LostWolfAnimState", 0);
		isTriggering = false;
		isTriggeringDen = false;

		//wolf Den
		wolfDenArt = GameObject.Find ("WolfDen");
		wolfDenAnim = wolfDenArt.GetComponent<Animator> ();
		wolfDenAnim.SetInteger ("DenAnimState", 0);

		// reading PCWolfInput Script
//		PCWolfInputScript = PlayerWolfGO.GetComponent <PCWolfInput> () as PCWolfInput; 
//		playerSpeed = PlayerWolfGO.GetComponent<PCWolfInput>().speed;
//		playerMoveSpeed = PlayerWolfGO.GetComponent<PCWolfInput>().moveSpeed;
//		playerRunSpeed = PlayerWolfGO.GetComponent<PCWolfInput>().runSpeed;

		//Vector3 randomPos = new Vector3(Random.Range(-10.0, 10.0), 0, Random.Range(-10.0, 10.0));
	}//end start
	
	// Update is called once per frame
	void Update () 
	{
		if (isTriggeringDen) 
		{
			speed = staySpeed;
			//WolfSpirit disappears
			LostWolfAnim.SetInteger ("LostWolfAnimState", 5);
			print ("Beam up Update!");
		}
		else if ((followPlayerWolfGO.transform.position.x > transform.position.x) && (!isTriggering)) 
		{
			LostWolfAnim.SetInteger ("LostWolfAnimState", 3);
			WolfSpiritFaceRight();
		} else if ((followPlayerWolfGO.transform.position.x < transform.position.x) && (!isTriggering)) 
		{
			LostWolfAnim.SetInteger ("LostWolfAnimState", 3);
			WolfSpiritFaceLeft();
		} 
		else if ((followPlayerWolfGO.transform.position.x > transform.position.x) && (isTriggering)) 
		{
			WolfSpiritFaceRight();
		} else if ((followPlayerWolfGO.transform.position.x < transform.position.x) && (isTriggering)) 
		{
			WolfSpiritFaceLeft();
		} else if ((followPlayerWolfGO.transform.position.x == transform.position.x) && (isTriggering)) 
		{
			LostWolfAnim.SetInteger ("LostWolfAnimState", 0);
		}

		if (isFollowing) {
			rb2DLostWolf.transform.position = Vector3.MoveTowards(rb2DLostWolf.transform.position, followPlayerWolfGO.transform.position, speed * Time.deltaTime);
			if(rb2DLostWolf.transform.position == followPlayerWolfGO.transform.position){
				rb2DLostWolf.transform.position = followPlayerWolfGO.transform.position;
			}
		}
	}//end update

	void OnEnable()
	{
		PCWolfInput.OnClickLeft += WolfSpiritWalking;
		PCWolfInput.OnClickRight += WolfSpiritRunning;
	}

	void OnDisable()
	{
		PCWolfInput.OnClickLeft -= WolfSpiritWalking;
		PCWolfInput.OnClickRight -= WolfSpiritRunning;

	}

	void WolfSpiritWalking()
	{
		if (isTriggering) 
		{
			LostWolfAnim.SetInteger ("LostWolfAnimState", 1);
			print ("spirit wolf walking");
		}
	}

	void WolfSpiritRunning()
	{
		if (isTriggering) 
		{
			LostWolfAnim.SetInteger ("LostWolfAnimState", 7);
			print ("spirit wolf running");
		}
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "HowlAttract") {
			isFollowing = true;
		}
	}

	/*
	void OnTriggerEnter2D(Collider2D target)
	{
		if(target.gameObject.tag == "HowlAttract" && target.gameObject.tag == "WolfDen")
		{
			rb2DLostWolf.MovePosition (Vector2.MoveTowards (LostWolfGO.transform.position, wolfDenArt.transform.position, speed * Time.deltaTime));
		} else if (target.gameObject.tag == "HowlAttract") 
		{
			isTriggering = true;
			speed = runSpeed;
			rb2DLostWolf.MovePosition (Vector2.MoveTowards (LostWolfGO.transform.position, followPlayerWolfGO.transform.position, speed * Time.deltaTime));
		} else if (target.gameObject.tag == "WolfDen")
		{
			isTriggering = true;
			isTriggeringDen = true;
			rb2DLostWolf.MovePosition (Vector2.MoveTowards (LostWolfGO.transform.position, wolfDenArt.transform.position, speed * Time.deltaTime));
		}
	}

	void OnTriggerStay2D(Collider2D target)
	{
		if (target.gameObject.tag == "HowlAttract") 
		{
			isTriggering = true;
			speed = runSpeed;
			rb2DLostWolf.MovePosition (Vector2.MoveTowards (LostWolfGO.transform.position, followPlayerWolfGO.transform.position, speed * Time.deltaTime));
		}//end if target howl attract
	}//end on trigger stay

	void OnTriggerExit2D(Collider2D target)
	{
		wolfDenAnim.SetInteger ("DenAnimState", 0);
		isTriggering = false;
		if ((followPlayerWolfGO.transform.position.x > transform.position.x) && (!isTriggering))
		{
			LostWolfAnim.SetInteger ("LostWolfAnimState", 3);
			WolfSpiritFaceRight();
		} else if ((followPlayerWolfGO.transform.position.x < transform.position.x) && (!isTriggering)) 
		{
			LostWolfAnim.SetInteger ("LostWolfAnimState", 3);
			WolfSpiritFaceLeft();
		} 
		else if (isTriggering == true) 
		{
			 if ((PlayerWolfGO.transform.position.x > transform.position.x) && (isTriggering))
			{
				WolfSpiritFaceRight();
			} else if ((PlayerWolfGO.transform.position.x < transform.position.x) && (isTriggering)) 
			{
				WolfSpiritFaceLeft();
			} 
		} //end is triggering true
	}//end onTriggerExit
	*/

	//IEnumerator WalkToPlayerWolf(){}

	void HowlEnd()
	{
		isTriggeringDen = false;
		wolfDenAnim.SetInteger ("DenAnimState", 0);
		Destroy(this.gameObject);
		PlayerWolfCollider.enabled = true;
	}

	void LastHowlEnd()
	{
		isTriggeringDen = false;
		wolfDenAnim.SetInteger ("DenAnimState", 0);
		//Destroy(this.gameObject);
		PlayerWolfCollider.enabled = true;
	}

	void GameEnd()
	{
		Application.LoadLevel ("Howl Title Screen");
	}

	void WolfSpiritFaceRight()
	{
		if (gameObject.transform.localScale.x < 0)
		{
			gameObject.transform.localScale = new Vector3 (1, 1, 1);
		}
	}

	void WolfSpiritFaceLeft()
	{
		if (gameObject.transform.localScale.x > 0)
		{
			gameObject.transform.localScale = new Vector3 (-1, 1, 1);	
		}	
	}

}//end whole class



