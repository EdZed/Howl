using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour 
{
	public BoxCollider2D LostWolfCollider;
	public GameObject LostWolfGO;
	public Rigidbody2D rb2DLostWolf;
	public GameObject lostWolfProximityGO;
	public CircleCollider2D lostWolfProximityCol;

	//player wolf
	public GameObject PlayerWolfGO;
	public BoxCollider2D PlayerWolfCollider;


	public GameObject followPlayerWolfGO;
	public GameObject followPosNeutral;
	public GameObject followPosLeftFront;
	public GameObject followPosRightFront;
	public GameObject followPosLeftMid;
	public GameObject followPosRightMid;

	SpriteRenderer LostWolfSpriteRend;

	//public GameObject lostWolf2Pos;
	float speed;
	float moveSpeed = 9f;
	float runSpeed = 14f;
	float staySpeed = 0f;

	bool isFollowing = false;
	bool isInDen = false;
	 
	public bool isTriggeringDen;
	//public bool 
	//public bool redWolf;

	//Wolf Den Art
	public GameObject wolfDenArt;
	//private Animator wolfDenAnim;
	private Animator LostWolfAnim;
	//string OrangeWolfString ;

	public bool runAtkPower;
	public bool howlFreezePower;
	public bool lostWolfAffection;

	PackFormationPos PackFormationPosScript;
	public GameObject packPosFormationGO;

	public delegate void PackMember();
	public static event PackMember AddPackMember;

	public GameObject LostWolfSlot1;
	public GameObject LostWolfSlot2;
	public GameObject LostWolfSlot3;
	public GameObject LostWolfSlot4;
	public GameObject LostWolfSlot5;

	public delegate void AddToCounter();
	public static event AddToCounter OnAddToCounter;

	//event**
	public delegate void PackNotExist();
	public static event PackNotExist OnPackNotExist;

	public AudioSource lostWolfHowl;
	public ParticleSystem lostWolfParticleSystem;

	//find script
//	GameObject WorldManagerGO;
//	public WorldManager WorldManagerScript;


	// Use this for initialization
	void Start () 
	{
		//Debug.Log(gameObject);
		runAtkPower = false;
		howlFreezePower = false;
		//OrangeWolfString = "Lost Wolf Orange";
		//redWolf = false;

		LostWolfGO = this.gameObject;
		LostWolfAnim = LostWolfGO.GetComponent<Animator> ();
		LostWolfCollider = GetComponent <BoxCollider2D> ();

		lostWolfProximityGO = LostWolfGO.transform.Find("proximity").gameObject;
		lostWolfProximityCol = lostWolfProximityGO.GetComponent<CircleCollider2D> ();
		//lostWolfProximityCol = LostWolfGO.transform.Find ("proximity").gameObject.GetComponent<CircleCollider2D> ();

		PlayerWolfGO = GameObject.Find("playerWolf");
		PlayerWolfCollider = PlayerWolfGO.GetComponent <BoxCollider2D> ();
		PlayerWolfCollider.enabled = true;

		followPlayerWolfGO = GameObject.Find("FollowPlayerWolf");
		followPosNeutral = GameObject.Find("Pos Neutral");
		followPosLeftFront = GameObject.Find("Pos Left Front");
		followPosRightFront = GameObject.Find("Pos Right Front");
		followPosLeftMid = GameObject.Find("Pos Left Mid");
		followPosRightMid = GameObject.Find("Pos Right Mid");
		
		speed = moveSpeed;
		rb2DLostWolf = GetComponent<Rigidbody2D> ();
		//LostWolfAnim = GetComponent<Animator> ();
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

		packPosFormationGO = GameObject.Find ("Pack Pos Formation");
		//howlFreeze = false;
		PackFormationPosScript = packPosFormationGO.GetComponent<PackFormationPos> ();
		LostWolfSlot1 = PackFormationPosScript.PackFormationGOSlots [0];
		LostWolfSlot2 = PackFormationPosScript.PackFormationGOSlots [1];
		LostWolfSlot3 = PackFormationPosScript.PackFormationGOSlots [2];
		LostWolfSlot4 = PackFormationPosScript.PackFormationGOSlots [3];
		LostWolfSlot5 = PackFormationPosScript.PackFormationGOSlots [4];

		LostWolfSpriteRend = GetComponent<SpriteRenderer> ();

		lostWolfHowl = GetComponent<AudioSource> ();

		//LostWolfGO.SetActive(false);
		LostWolfSpriteRend.enabled = false;
		LostWolfCollider.enabled = false;
		lostWolfProximityCol.enabled = false;

		//lostWolfParticleSystem = this.gameObject.GetComponent<ParticleSystem> ();
		lostWolfParticleSystem = this.gameObject.transform.Find ("Particle to Den").GetComponent<ParticleSystem> ();
		lostWolfParticleSystem.enableEmission = false;

//		WorldManagerGO = GameObject.Find ("WorldManager");
//		WorldManagerScript = WorldManagerGO.GetComponent<WorldManager> ();

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
		//causes error that registers 143 times
//		if (isInDen) {
//			rb2DLostWolf.transform.position = Vector3.MoveTowards(rb2DLostWolf.transform.position, wolfDenArt.transform.position, speed * Time.deltaTime);
//			if(rb2DLostWolf.transform.position == wolfDenArt.transform.position){
//				rb2DLostWolf.transform.position = wolfDenArt.transform.position;
//			}
//		}
	
		if (isFollowing) {
			if(PackFormationPosScript.packSize == 1){
				if(LostWolfSlot1  == this.gameObject){
					FollowInPack(followPosNeutral);
					//Debug.Log ("if this wolf is 1st, start following");
				}
				
				
			} else if(PackFormationPosScript.packSize >= 2){
				if(LostWolfSlot1  == this.gameObject){
					FollowInPack(followPosLeftFront);
					//Debug.Log ("1st wolf on left slot");
				}

				if(LostWolfSlot2  == this.gameObject){
					FollowInPack(followPosRightFront);
					//Debug.Log ("2nd wolf on right slot");
				}
			}

			if(PackFormationPosScript.packSize == 3){
				if(LostWolfSlot3  == this.gameObject){
					FollowInPack(followPosNeutral);
					//Debug.Log ("if this wolf is 1st, start following");
				}
			}
			else if(PackFormationPosScript.packSize >= 4){
				if(LostWolfSlot3  == this.gameObject){
					FollowInPack(followPosLeftMid);
					//Debug.Log ("1st wolf on left slot");
				}
				
				if(LostWolfSlot4  == this.gameObject){
					FollowInPack(followPosRightMid);
					//Debug.Log ("2nd wolf on right slot");
				}
			}
			if(PackFormationPosScript.packSize == 5){
				if(LostWolfSlot5  == this.gameObject){
					FollowInPack(followPosNeutral);
					//Debug.Log ("if this wolf is 1st, start following");
				}
			}

			

		} 

//		else if (!isFollowing) {
//			if (lostWolfAffection == true) {
//				LostWolfAnim.SetInteger ("LostWolfAnimState", 2);
//			} 
//			else if(lostWolfAffection == false) {
//				LostWolfAnim.SetInteger ("LostWolfAnimState", 0);
//			}
//		}

	}//end update

	void OnTriggerEnter2D(Collider2D target){
		if (!isFollowing) {
			if (target.gameObject.tag == "HowlAttract") {
				LostWolfSpriteRend.enabled = true;
				lostWolfParticleSystem.enableEmission = true;
				if (AddPackMember != null) {
					AddPackMember ();
					//Debug.Log ("affect trig stay");
				}

				AddToPackSlot ();
				Debug.Log ("add member to pack");
				this.gameObject.AddComponent<MovingSpriteZOrder> ();
				//staticSpriteZOrder.enabled = false / To remove/disable current z order script?

				lostWolfProximityGO.SetActive(false);

				isFollowing = true;
				//send message to PackFormationScript adding wolf to packSize and turning on bool

//			if(runAtkPower == true){
//				PlayerWolfGO.GetComponent<PCWolfInput>().runAtk = true;
//				|| target.gameObject.tag == "LostWolfOrange"
//			}
			} //else 

		}
		if (target.gameObject.tag == "WolfDen") {

			//Debug.Log("Wolf triggering wolf den");
			//lostWolfHowl.enabled = true;
			lostWolfHowl.Play();
			if (runAtkPower == true) {
				PlayerWolfGO.GetComponent<PCWolfInput>().runAtk = false;
				//Debug.Log("run attack power is:" + PlayerWolfGO.GetComponent<PCWolfInput>().runAtk);
			}
			PackFormationPosScript.packMusicLayers[0].mute = true;
			PackFormationPosScript.packMusicLayers[1].mute = true;
			PackFormationPosScript.packMusicLayers[2].mute = true;
			PackFormationPosScript.packMusicLayers[3].mute = true;

			//WorldManager.
			//sends to worldManager
			if (OnAddToCounter != null) {
				OnAddToCounter ();
				Debug.Log ("added to counter from den");
			}
			//sends to WolfDen script
			if (OnPackNotExist != null) {
				OnPackNotExist ();
				Debug.Log ("pack not exist from den");
			}

			isFollowing = false;
			isInDen = true;
			isTriggeringDen = true;

		}
	}

	void FollowInPack (GameObject placeHolderGO){
		//follow behind player
		//rb2DLostWolf.transform.position = Vector3.MoveTowards (rb2DLostWolf.transform.position, followPlayerWolfGO.transform.position, speed * Time.deltaTime);
		//Debug.Log ("a wolf should be following");
		rb2DLostWolf.transform.position = Vector3.MoveTowards (rb2DLostWolf.transform.position, placeHolderGO.transform.position, speed * Time.deltaTime);
		if (rb2DLostWolf.transform.position == placeHolderGO.transform.position) {
			rb2DLostWolf.transform.position = placeHolderGO.transform.position;
		}
		
		float LoneWolfDist = Vector3.Distance (rb2DLostWolf.transform.position, placeHolderGO.transform.position);
		
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

	}

	void AddToPackSlot(){
		Debug.Log ("add to pack slot called");
		if(PackFormationPosScript.packSize == 1){
			LostWolfSlot1 = this.gameObject;
			Debug.Log ("this wolf is 1st");
			PackFormationPosScript.packMusicLayers[0].mute = false;
		} else if(PackFormationPosScript.packSize == 2){
			LostWolfSlot2 = this.gameObject;
			PackFormationPosScript.packMusicLayers[1].mute = false;
		} else if(PackFormationPosScript.packSize == 3){
			LostWolfSlot3 = this.gameObject;
			PackFormationPosScript.packMusicLayers[2].mute = false;
		} else if(PackFormationPosScript.packSize == 4){
			LostWolfSlot4 = this.gameObject;
			PackFormationPosScript.packMusicLayers[3].mute = false;
		} else if(PackFormationPosScript.packSize == 5){
			LostWolfSlot5 = this.gameObject;
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

//	IEnumerator GameEnd(){
//		Debug.Log("Before Waiting 7 seconds");
//		yield return new WaitForSeconds(7);
//		Debug.Log("After Waiting 7 Seconds");
//		Application.LoadLevel ("Howl Title Screen");
//		Destroy(this.gameObject);
//	}

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
		//WolfDenSpiritMusic.RedWolfCollected += GameEnd;
		AffectionTrigger.AffectionAnimOn += AffectionOn;
		AffectionTrigger.AffectionAnimOff += AffectionOff;
		WorldManager.OnLostWolfActive += LostWolfActive;
		WorldManager.OnLostWolfNotActive += LostWolfNotActive;
	}
	
	
	void OnDisable()
	{
		//WolfDenSpiritMusic.RedWolfCollected -= GameEnd;
		AffectionTrigger.AffectionAnimOn -= AffectionOn;
		AffectionTrigger.AffectionAnimOff -= AffectionOff;
		WorldManager.OnLostWolfActive -= LostWolfActive;
		WorldManager.OnLostWolfNotActive -= LostWolfNotActive;
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

	void LostWolfActive()
	{
		//lostWolfAffection = false;
		//Debug.Log ("den wolf stopped showing affection");

//		LostWolfCollider.enabled = !LostWolfCollider.enabled;
//		lostWolfProximityCol.enabled = !lostWolfProximityCol.enabled;
		LostWolfCollider.enabled = true;
		lostWolfProximityCol.enabled = true;
	}

	//called by worldmanager script
	void LostWolfNotActive()
	{
		isFollowing = false;
		LostWolfSpriteRend.enabled = false;
		PackFormationPosScript.MinusPackMember ();
		LostWolfAnim.SetInteger ("LostWolfAnimState", 0);
		Debug.Log ("lost wolf not active/ not following");

		LostWolfCollider.enabled = false;
		lostWolfProximityCol.enabled = false;
		//Only un comment if can stll trigger in real world
//		LostWolfCollider.enabled = !LostWolfCollider.enabled;
//		lostWolfProximityCol.enabled = !lostWolfProximityCol.enabled;
	}


}//end whole class














