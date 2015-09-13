using UnityEngine;
using System.Collections;

public class FollowPlayer1 : MonoBehaviour {
	
	public BoxCollider2D LostWolfCollider;
	public GameObject LostWolfGO;
	public Rigidbody2D rb2DLostWolf;
	public GameObject PlayerWolfGO;
	float speed;
	float moveSpeed = 3f;
	
	private Animator LostWolfAnim;
	
	// Use this for initialization
	void Start () {
		LostWolfAnim = gameObject.GetComponent<Animator> ();
		LostWolfGO = GameObject.Find("Lost Wolf 1");
		//enemyAttackCollider = enemyAttackGO.GetComponent <BoxCollider2D>();
		LostWolfCollider = LostWolfGO.GetComponent <BoxCollider2D> ();
		//LostWolfCollider.enabled = false;
		
		PlayerWolfGO = GameObject.Find("playerWolf");
		
		speed = moveSpeed;
		rb2DLostWolf = GetComponent<Rigidbody2D> ();
		LostWolfAnim = GetComponent<Animator> ();
		LostWolfAnim.SetInteger ("LostWolfAnimState", 0);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "HowlAttract") 
		{
			//if(attacking)
			//{
			//var explode = target.GetComponent<Explode>() as Explode;
			//explode.OnExplode();
			//Destroy(target.gameObject); 
			rb2DLostWolf.MovePosition (Vector2.MoveTowards (LostWolfGO.transform.position, PlayerWolfGO.transform.position, speed * Time.deltaTime));
			PlayerWolfGO.GetComponent<AudioSource> ().Play ();
			
			//			
			
		} 
		if (target.gameObject.tag == "WolfDen") 
		{
			
			
			//PlayerWolfGO.GetComponent<AudioSource> ().Play ();
			
			// Instantiates a prefab named "enemy" located in any Resources
			// folder in your project's Assets folder.
			//GameObject instance = Instantiate(Resources.Load("Lost Wolf", typeof(GameObject))) as GameObject;		
			Destroy(gameObject);
		}
	}
	
	void OnTriggerStay2D(Collider2D target)
	{
		if (target.gameObject.tag == "HowlAttract") 
		{
			//if(attacking)
			//{
			//var explode = target.GetComponent<Explode>() as Explode;
			//explode.OnExplode();
			//Destroy(target.gameObject); 
			rb2DLostWolf.MovePosition (Vector2.MoveTowards (LostWolfGO.transform.position, PlayerWolfGO.transform.position, speed * Time.deltaTime));
			if (target.gameObject.transform.position.x > transform.position.x) 
			{
				//anim.SetTrigger("walk");
				LostWolfAnim.SetInteger ("LostWolfAnimState", 2);
				//rb2DenemyWolf.MovePosition (Vector2.MoveTowards (gameObject.transform.parent.gameObject.transform.position, target.gameObject.transform.parent.gameObject.transform.position, speed * Time.deltaTime));
				if (gameObject.transform.localScale.x < 0)
					gameObject.transform.localScale = new Vector3 (1, 1, 1);
			} else if (target.gameObject.transform.position.x < transform.position.x) {
				//anim.SetTrigger("walkLeft");
				LostWolfAnim.SetInteger ("LostWolfAnimState", 1);
				//rb2DenemyWolf.MovePosition (Vector2.MoveTowards (gameObject.transform.parent.gameObject.transform.position, target.gameObject.transform.parent.gameObject.transform.position, speed * Time.deltaTime));
				if (gameObject.transform.localScale.x > 0)
					gameObject.transform.localScale = new Vector3 (-1, 1, 1);	
			} 
		} else {
			//LostWolfAnim.SetInteger("LostWolfAnimState", 0);
		}	
	}
	
	void OnTriggerExit2D(Collider2D target){
		//readyToAttack = false;
		//attacking = false;
		LostWolfAnim.SetInteger ("LostWolfAnimState", 0);
	}
}
