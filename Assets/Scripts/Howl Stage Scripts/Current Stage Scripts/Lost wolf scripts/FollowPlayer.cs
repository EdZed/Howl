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
	float moveSpeed = 9f;
	float runSpeed = 14f;
	float staySpeed = 0f;

	bool isFollowing = false;
	bool isInDen = false;
	public bool isTriggeringDen;
	//public bool redWolf;

	//Wolf Den Art
	public GameObject wolfDenArt;
	//private Animator wolfDenAnim;
	private Animator LostWolfAnim;
	//string OrangeWolfString ;

	public bool runAtkPower;
	public bool howlFreezePower;
	public bool lostWolfAffection;

	// Use this for initialization
	void Start () 
	{
		//Debug.Log(gameObject);
		runAtkPower = false;
		howlFreezePower = false;
		//OrangeWolfString = "Lost Wolf Orange";
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
		//Wolf Idle
		LostWolfAnim.SetInteger ("LostWolfAnimState", 0);
		isTriggeringDen = false;

		//wolf Den
		wolfDenArt = GameObject.Find ("WolfDen");
		//wolfDenAnim = wolfDenArt.GetComponent<Animator> ();
		//Wolf idle
		//wolfDenAnim.SetInteger ("DenAnimState", 0);

		if(this.gameObject.tag == "LostWolfOrange"){
			runAtkPower = true;
		}
		if(this.gameObject.tag == "LostWolfGreen"){
			howlFreezePower = true;
		}
		lostWolfAffection = false;

//		if (GameObject.Find ("Lost Wolf Orange") == this.gameObject) {
//			runAtkPower = true;
//		}
//		if (GameObject.Find ("Lost Wolf Green") == this.gameObject) {
//			howlFreezePower = true;
//		}
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
			speed = staySpeed;
			//WolfSpirit disappears
			LostWolfAnim.SetInteger ("LostWolfAnimState", 5);
			//print ("Beam up Update!");
			//}
		}

		if (isInDen) {
			rb2DLostWolf.transform.position = Vector3.MoveTowards(rb2DLostWolf.transform.position, wolfDenArt.transform.position, speed * Time.deltaTime);
			if(rb2DLostWolf.transform.position == wolfDenArt.transform.position){
				rb2DLostWolf.transform.position = wolfDenArt.transform.position;
			}
		}
	
		if (isFollowing) {
			rb2DLostWolf.transform.position = Vector3.MoveTowards (rb2DLostWolf.transform.position, followPlayerWolfGO.transform.position, speed * Time.deltaTime);
			if (rb2DLostWolf.transform.position == followPlayerWolfGO.transform.position) {
				rb2DLostWolf.transform.position = followPlayerWolfGO.transform.position;
			}

			float LoneWolfDist = Vector3.Distance (rb2DLostWolf.transform.position, followPlayerWolfGO.transform.position);

			if (PlayerWolfGO.transform.position.x > rb2DLostWolf.transform.position.x) {
				WolfSpiritFaceRight ();
			} else if (PlayerWolfGO.transform.position.x < rb2DLostWolf.transform.position.x) {
				WolfSpiritFaceLeft ();
			}

			if (PlayerWolfGO.GetComponent<PCWolfInput> ().walking || (LoneWolfDist > 0f && LoneWolfDist < 9f)) {
				LostWolfAnim.SetInteger ("LostWolfAnimState", 1);
			} else if (PlayerWolfGO.GetComponent<PCWolfInput> ().running || LoneWolfDist > 9f) {
				LostWolfAnim.SetInteger ("LostWolfAnimState", 7);
			} else if (!PlayerWolfGO.GetComponent<PCWolfInput> ().walking || !PlayerWolfGO.GetComponent<PCWolfInput> ().running) {
				//idle
				LostWolfAnim.SetInteger ("LostWolfAnimState", 0);
			}
		} else if (!isFollowing) {
			if (lostWolfAffection == true) {
				LostWolfAnim.SetInteger ("LostWolfAnimState", 2);
			} 
			else if(lostWolfAffection == false) {
				LostWolfAnim.SetInteger ("LostWolfAnimState", 0);
			}
		}

	}//end update

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "HowlAttract") {
			isFollowing = true;
//			if(runAtkPower == true){
//				PlayerWolfGO.GetComponent<PCWolfInput>().runAtk = true;
//				|| target.gameObject.tag == "LostWolfOrange"
//			}
		} //else 
		if (target.gameObject.tag == "WolfDen") {
			isFollowing = false;
			isInDen = true;
			isTriggeringDen = true;
			//Debug.Log("Wolf triggering wolf den");
			if (runAtkPower == true) {
				PlayerWolfGO.GetComponent<PCWolfInput>().runAtk = false;
				//Debug.Log("run attack power is:" + PlayerWolfGO.GetComponent<PCWolfInput>().runAtk);
			}

		}
	}


	void HowlEnd(){
		isTriggeringDen = false;
		//wolfDenAnim.SetInteger ("DenAnimState", 0);
		Destroy(this.gameObject);
		PlayerWolfCollider.enabled = true;
	}

	void LastHowlEnd(){
		isTriggeringDen = false;
		//wolfDenAnim.SetInteger ("DenAnimState", 0);
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
		AffectionTrigger.AffectionAnimOn += AffectionOn;
		AffectionTrigger.AffectionAnimOff += AffectionOff;
	}
	
	
	void OnDisable()
	{
		WolfDenSpiritMusic.RedWolfCollected -= GameEnd;
		AffectionTrigger.AffectionAnimOn -= AffectionOn;
		AffectionTrigger.AffectionAnimOff -= AffectionOff;
	}

	void AffectionOn()
	{
		lostWolfAffection = true;
		Debug.Log ("den wolf is showing affection");
	}
	
	
	void AffectionOff()
	{
		lostWolfAffection = false;
		Debug.Log ("den wolf stopped showing affection");
	}


}//end whole class