using UnityEngine;
using System.Collections;

public class LastFollowPlayer : MonoBehaviour {
	
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
	float staySpeed = 0f;
	
	public bool isTriggering;
	public bool isTriggeringDen;
	
	//lost wolf
	//public GameObject lostWolf1;
	//public BoxCollider2D lostWolf1Collider;
	
	//Wolf Den Art
	public GameObject wolfDenArt;
	private Animator wolfDenAnim;
	
	private Animator LostWolfAnim;
	
	// Use this for initialization
	void Start () {
		LostWolfAnim = gameObject.GetComponent<Animator> ();
		//LostWolfGO = GameObject.Find("Lost Wolf");
		//LostWolfGO = GameObject.FindWithTag ("LostWolf");
		LostWolfGO = this.gameObject;
		//lostWolf2Pos = GameObject.Find ("LostWolf1Pos");
		//enemyAttackCollider = enemyAttackGO.GetComponent <BoxCollider2D>();
		//LostWolfCollider = LostWolfGO.GetComponent <BoxCollider2D> ();
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
		
		//Vector3 randomPos = new Vector3(Random.Range(-10.0, 10.0), 0, Random.Range(-10.0, 10.0));
		
	}//end start
	
	// Update is called once per frame
	void Update () {
		if (isTriggeringDen) {
			speed = staySpeed;
			LostWolfAnim.SetInteger ("LostWolfAnimState", 6);
			print ("Beam up Update!");
		}
		else if ((followPlayerWolfGO.transform.position.x > transform.position.x) && (!isTriggering)) {
			//anim.SetTrigger("walk");
			LostWolfAnim.SetInteger ("LostWolfAnimState", 3);
			//rb2DenemyWolf.MovePosition (Vector2.MoveTowards (gameObject.transform.parent.gameObject.transform.position, target.gameObject.transform.parent.gameObject.transform.position, speed * Time.deltaTime));
			if (gameObject.transform.localScale.x < 0)
				gameObject.transform.localScale = new Vector3 (1, 1, 1);
		} else if ((followPlayerWolfGO.transform.position.x < transform.position.x) && (!isTriggering)) {
			//anim.SetTrigger("walkLeft");
			LostWolfAnim.SetInteger ("LostWolfAnimState", 3);
			//rb2DenemyWolf.MovePosition (Vector2.MoveTowards (gameObject.transform.parent.gameObject.transform.position, target.gameObject.transform.parent.gameObject.transform.position, speed * Time.deltaTime));
			if (gameObject.transform.localScale.x > 0)
				gameObject.transform.localScale = new Vector3 (-1, 1, 1);	
		} 
		//}//end is triggering false
		//else if (isTriggering == true) {
		else if ((followPlayerWolfGO.transform.position.x > transform.position.x) && (isTriggering)) {
			//anim.SetTrigger("walk");
			LostWolfAnim.SetInteger ("LostWolfAnimState", 1);
			//rb2DenemyWolf.MovePosition (Vector2.MoveTowards (gameObject.transform.parent.gameObject.transform.position, target.gameObject.transform.parent.gameObject.transform.position, speed * Time.deltaTime));
			if (gameObject.transform.localScale.x < 0)
				gameObject.transform.localScale = new Vector3 (1, 1, 1);
		} else if ((followPlayerWolfGO.transform.position.x < transform.position.x) && (isTriggering)) {
			//anim.SetTrigger("walkLeft");
			LostWolfAnim.SetInteger ("LostWolfAnimState", 1);
			//rb2DenemyWolf.MovePosition (Vector2.MoveTowards (gameObject.transform.parent.gameObject.transform.position, target.gameObject.transform.parent.gameObject.transform.position, speed * Time.deltaTime));
			if (gameObject.transform.localScale.x > 0)
				gameObject.transform.localScale = new Vector3 (-1, 1, 1);	
		} else if ((followPlayerWolfGO.transform.position.x == transform.position.x) && (isTriggering)) {
			//anim.SetTrigger("walk");
			LostWolfAnim.SetInteger ("LostWolfAnimState", 0);
			//rb2DenemyWolf.MovePosition (Vector2.MoveTowards (gameObject.transform.parent.gameObject.transform.position, target.gameObject.transform.parent.gameObject.transform.position, speed * Time.deltaTime));
			if (PlayerWolfGO.transform.localScale.x > 0) {
				gameObject.transform.localScale = new Vector3 (1, 1, 1);
			}
			if (PlayerWolfGO.transform.localScale.x < 0) {
				gameObject.transform.localScale = new Vector3 (-1, 1, 1);
			}
		}  
	}//end update
	
	
	void OnTriggerEnter2D(Collider2D target)
	{
		//isTriggering = true;
		if (target.gameObject.tag == "HowlAttract") 
		{
			isTriggering = true;
			//if(attacking)
			//{
			//var explode = target.GetComponent<Explode>() as Explode;
			//explode.OnExplode();
			//Destroy(target.gameObject); 
			rb2DLostWolf.MovePosition (Vector2.MoveTowards (LostWolfGO.transform.position, followPlayerWolfGO.transform.position, speed * Time.deltaTime));
			//followPlayerWolfGO.GetComponent<AudioSource> ().Play ();
			wolfDenAnim.SetInteger ("DenAnimState", 1);
			//PlayerWolfCollider.enabled = false;
			//line above caused wolf to go past edge of stage when guiding wolf.
			
		} 
		
		if (target.gameObject.tag == "WolfDen")
		{
			isTriggering = true;
			isTriggeringDen = true;
		}
		//		else if (target.gameObject.tag == "WolfDen" ) 
		//		{
		//			isTriggeringDen = true;
		//			LostWolfAnim.SetInteger ("LostWolfAnimState", 5);
		//			print ("Beam up!");
		//			//PlayerWolfGO.GetComponent<AudioSource> ().Play ();
		//			
		//			// Instantiates a prefab named "enemy" located in any Resources
		//			// folder in your project's Assets folder.
		//			//GameObject instance = Instantiate(Resources.Load("Lost Wolf", typeof(GameObject))) as GameObject;	
		//			//GameObject instance = Instantiate(Resources.Load("Lost Wolf")) as GameObject;
		//			//instance.transform.position = lostWolf1Pos.transform.position;
		//			//instance.transform.position = new Vector3(Random.Range(-10.0, 10.0), 0, Random.Range(-10.0, 10.0));
		//			//spawn lost wolf
		//			//lostWolf1.SetActive (true);
		//			//lostWolf1.GetComponent<Renderer> ().enabled = true;
		//			//Destroy(gameObject);
		//		}
	}
	
	void OnTriggerStay2D(Collider2D target)
	{
		//isTriggering = true;
		if (target.gameObject.tag == "HowlAttract") {
			isTriggering = true;
			//if(attacking)
			//{
			//var explode = target.GetComponent<Explode>() as Explode;
			//explode.OnExplode();
			//Destroy(target.gameObject); 
			rb2DLostWolf.MovePosition (Vector2.MoveTowards (LostWolfGO.transform.position, followPlayerWolfGO.transform.position, speed * Time.deltaTime));
			wolfDenAnim.SetInteger ("DenAnimState", 1);
		}//end if target howl attract
		
		//		else if (target.gameObject.tag == "WolfDen") 
		//		{
		//			isTriggeringDen = true;
		//			LostWolfAnim.SetInteger ("LostWolfAnimState", 5);
		//			print ("Beam up!");
		//			//PlayerWolfGO.GetComponent<AudioSource> ().Play ();
		//			
		//			// Instantiates a prefab named "enemy" located in any Resources
		//			// folder in your project's Assets folder.
		//			//GameObject instance = Instantiate(Resources.Load("Lost Wolf", typeof(GameObject))) as GameObject;	
		//			//GameObject instance = Instantiate(Resources.Load("Lost Wolf")) as GameObject;
		//			//instance.transform.position = lostWolf1Pos.transform.position;
		//			//instance.transform.position = new Vector3(Random.Range(-10.0, 10.0), 0, Random.Range(-10.0, 10.0));
		//			//spawn lost wolf
		//			//lostWolf1.SetActive (true);
		//			//lostWolf1.GetComponent<Renderer> ().enabled = true;
		//			//Destroy(gameObject);
		//		}
		
		
		//		if (target.gameObject.tag == "WolfDen") 
		//		{
		//			LostWolfAnim.SetInteger ("LostWolfAnimState", 5);
		//			print ("Beam up!");
		//			//PlayerWolfGO.GetComponent<AudioSource> ().Play ();
		//			
		//			// Instantiates a prefab named "enemy" located in any Resources
		//			// folder in your project's Assets folder.
		//			//GameObject instance = Instantiate(Resources.Load("Lost Wolf", typeof(GameObject))) as GameObject;	
		//			//GameObject instance = Instantiate(Resources.Load("Lost Wolf")) as GameObject;
		//			//instance.transform.position = lostWolf1Pos.transform.position;
		//			//instance.transform.position = new Vector3(Random.Range(-10.0, 10.0), 0, Random.Range(-10.0, 10.0));
		//			//spawn lost wolf
		//			//lostWolf1.SetActive (true);
		//			//lostWolf1.GetComponent<Renderer> ().enabled = true;
		//			//Destroy(gameObject);
		//		}
	}//end on trigger stay
	
	void OnTriggerExit2D(Collider2D target){
		//readyToAttack = false;
		//attacking = false;
		//LostWolfAnim.SetInteger ("LostWolfAnimState", 0);
		wolfDenAnim.SetInteger ("DenAnimState", 0);
		isTriggering = false;
		if ((followPlayerWolfGO.transform.position.x > transform.position.x) && (!isTriggering)) {
			//anim.SetTrigger("walk");
			LostWolfAnim.SetInteger ("LostWolfAnimState", 3);
			//rb2DenemyWolf.MovePosition (Vector2.MoveTowards (gameObject.transform.parent.gameObject.transform.position, target.gameObject.transform.parent.gameObject.transform.position, speed * Time.deltaTime));
			if (gameObject.transform.localScale.x < 0)
				gameObject.transform.localScale = new Vector3 (1, 1, 1);
		} else if ((followPlayerWolfGO.transform.position.x < transform.position.x) && (!isTriggering)) {
			//anim.SetTrigger("walkLeft");
			LostWolfAnim.SetInteger ("LostWolfAnimState", 3);
			//rb2DenemyWolf.MovePosition (Vector2.MoveTowards (gameObject.transform.parent.gameObject.transform.position, target.gameObject.transform.parent.gameObject.transform.position, speed * Time.deltaTime));
			if (gameObject.transform.localScale.x > 0)
				gameObject.transform.localScale = new Vector3 (-1, 1, 1);	
		} 
		else if (isTriggering == true) {
			if ((PlayerWolfGO.transform.position.x > transform.position.x) && (isTriggering)) {
				//anim.SetTrigger("walk");
				LostWolfAnim.SetInteger ("LostWolfAnimState", 1);
				//rb2DenemyWolf.MovePosition (Vector2.MoveTowards (gameObject.transform.parent.gameObject.transform.position, target.gameObject.transform.parent.gameObject.transform.position, speed * Time.deltaTime));
				if (gameObject.transform.localScale.x < 0)
					gameObject.transform.localScale = new Vector3 (1, 1, 1);
			} else if ((PlayerWolfGO.transform.position.x < transform.position.x) && (isTriggering)) {
				//anim.SetTrigger("walkLeft");
				LostWolfAnim.SetInteger ("LostWolfAnimState", 1);
				//rb2DenemyWolf.MovePosition (Vector2.MoveTowards (gameObject.transform.parent.gameObject.transform.position, target.gameObject.transform.parent.gameObject.transform.position, speed * Time.deltaTime));
				if (gameObject.transform.localScale.x > 0)
					gameObject.transform.localScale = new Vector3 (-1, 1, 1);	
			} 
		} //end is triggering true
	}//end onTriggerExit
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
	
	void GameEnd(){
		
		Application.LoadLevel ("Howl Title Screen");
		Destroy(this.gameObject);
		//isTriggeringDen = false;
		//wolfDenAnim.SetInteger ("DenAnimState", 0);
		//Destroy(this.gameObject);
		//PlayerWolfCollider.enabled = true;
	}
	
}//end onTriggerExit

//end whole class
