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

	bool isFollowing = false;
	bool isInDen = false;
	public bool isTriggeringDen;
	//public bool redWolf;

	//Wolf Den Art
	public GameObject wolfDenArt;
	private Animator wolfDenAnim;
	private Animator LostWolfAnim;

	// Use this for initialization
	void Start () 
	{
		//redWolf = false;
		LostWolfAnim = gameObject.GetComponent<Animator> ();
		LostWolfGO = this.gameObject;
		LostWolfCollider = GetComponent <BoxCollider2D> ();

		PlayerWolfGO = GameObject.Find("playerWolf");
		PlayerWolfCollider = PlayerWolfGO.GetComponent <BoxCollider2D> ();
		PlayerWolfCollider.enabled = true;

		followPlayerWolfGO = GameObject.Find("FollowPlayerWolf");
		
		speed = moveSpeed;
		rb2DLostWolf = GetComponent<Rigidbody2D> ();
		LostWolfAnim = GetComponent<Animator> ();
		LostWolfAnim.SetInteger ("LostWolfAnimState", 2);
		isTriggeringDen = false;

		//wolf Den
		wolfDenArt = GameObject.Find ("WolfDen");
		wolfDenAnim = wolfDenArt.GetComponent<Animator> ();
		wolfDenAnim.SetInteger ("DenAnimState", 0);
//		if(LostWolfGO == GameObject.Find ("Lost Wolf Red"))
//		{
//			redWolf = true;
//			print ("Red wolf detected!");
//		}

		//Vector3 randomPos = new Vector3(Random.Range(-10.0, 10.0), 0, Random.Range(-10.0, 10.0));
	}//end start
	
	// Update is called once per frame
	void Update () {
		if (isTriggeringDen) {
//			if(redWolf)
//			{
//				LostWolfAnim.SetInteger ("LostWolfAnimState", 6);
//				//GameEnd();
//				print ("red wolf disappear");
//			}else
//			{
			speed = staySpeed;
			//WolfSpirit disappears
			LostWolfAnim.SetInteger ("LostWolfAnimState", 5);
			//print ("Beam up Update!");
			//}
		}
	
		if (isFollowing) {
			rb2DLostWolf.transform.position = Vector3.MoveTowards(rb2DLostWolf.transform.position, followPlayerWolfGO.transform.position, speed * Time.deltaTime);
			if(rb2DLostWolf.transform.position == followPlayerWolfGO.transform.position){
				rb2DLostWolf.transform.position = followPlayerWolfGO.transform.position;
			}

			float LoneWolfDist = Vector3.Distance(rb2DLostWolf.transform.position, followPlayerWolfGO.transform.position);

			if (PlayerWolfGO.transform.position.x > rb2DLostWolf.transform.position.x){
				WolfSpiritFaceRight();
			} else if (PlayerWolfGO.transform.position.x < rb2DLostWolf.transform.position.x){
				WolfSpiritFaceLeft();
			}

			if (PlayerWolfGO.GetComponent<PCWolfInput>().walking || (LoneWolfDist > 0f && LoneWolfDist < 9f)){
				LostWolfAnim.SetInteger ("LostWolfAnimState", 1);
			} else if (PlayerWolfGO.GetComponent<PCWolfInput>().running || LoneWolfDist > 9f){
				LostWolfAnim.SetInteger ("LostWolfAnimState", 7);
			} else if (!PlayerWolfGO.GetComponent<PCWolfInput>().walking || !PlayerWolfGO.GetComponent<PCWolfInput>().running){
				LostWolfAnim.SetInteger ("LostWolfAnimState", 0);
			}
		}

		if (isInDen) {
			rb2DLostWolf.transform.position = Vector3.MoveTowards(rb2DLostWolf.transform.position, wolfDenArt.transform.position, speed * Time.deltaTime);
			if(rb2DLostWolf.transform.position == wolfDenArt.transform.position){
				rb2DLostWolf.transform.position = wolfDenArt.transform.position;
			}
		}
	}//end update

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "HowlAttract") {
			isFollowing = true;
		} else if (target.gameObject.tag == "WolfDen") {
			isFollowing = false;
			isInDen = true;
			isTriggeringDen = true;
		}
	}

	void HowlEnd(){
		isTriggeringDen = false;
		wolfDenAnim.SetInteger ("DenAnimState", 0);
		Destroy(this.gameObject);
		PlayerWolfCollider.enabled = true;
	}

	void LastHowlEnd(){
		isTriggeringDen = false;
		wolfDenAnim.SetInteger ("DenAnimState", 0);
		//Destroy(this.gameObject);
		PlayerWolfCollider.enabled = true;
	}

	IEnumerator GameEnd(){
		Debug.Log("Before Waiting 7 seconds");
		yield return new WaitForSeconds(7);
		Debug.Log("After Waiting 7 Seconds");
		Application.LoadLevel ("Howl Title Screen");
		Destroy(this.gameObject);
	}

	void WolfSpiritFaceRight(){
		if (gameObject.transform.localScale.x < 0){
			gameObject.transform.localScale = new Vector3 (1, 1, 1);
		}
	}

	void WolfSpiritFaceLeft(){
		if (gameObject.transform.localScale.x > 0){
			gameObject.transform.localScale = new Vector3 (-1, 1, 1);	
		}	
	}
	void OnEnable()
	{
		WolfDenSpiritMusic.RedWolfCollected += GameEnd;
	}
	
	
	void OnDisable()
	{
		WolfDenSpiritMusic.RedWolfCollected -= GameEnd;
	}


}//end whole class