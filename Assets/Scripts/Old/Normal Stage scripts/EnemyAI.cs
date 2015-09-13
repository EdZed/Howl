using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public BoxCollider2D enemyAttackCollider;
	public GameObject enemyAttackGO;

	public Rigidbody2D rb2DenemyBear;
	float speed;
	float attackSpeed = 100f;

	private GameObject playerWolf;

	// Use this for initialization
	void Start () {
		enemyAttackGO = GameObject.Find("enemyAttack");
		//enemyAttackCollider = enemyAttackGO.GetComponent <BoxCollider2D>();
		enemyAttackCollider = enemyAttackGO.GetComponent <BoxCollider2D> ();
		enemyAttackCollider.enabled = false;

		playerWolf = GameObject.Find("playerWolf");

		speed = attackSpeed;
		rb2DenemyBear = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void BearAttack(){

		rb2DenemyBear.MovePosition (Vector2.MoveTowards (gameObject.transform.position, playerWolf.transform.position, speed * Time.deltaTime));
		//readyToAttack = true;
		//attacking = true;
		//print ("attack true again!");
		//if (attacking) {
			//speed = attackSpeed;
			//rb2DplayerWolf.MovePosition (Vector2.MoveTowards (gameObject.transform.position, targetPos, speed * Time.deltaTime));
			//print ("wolf attack!");
			//attacking = false;
			//print ("attack false again!");
		//} 
	}
	
	void BearAttackTrigger()
	{
		enemyAttackCollider.enabled = true;
		print ("enemy attack collider on!");
		
	}
	
	void BearAttackTriggerOff()
	{
		enemyAttackCollider.enabled = false;
		print ("enemy attack collider off!");
		
	}
}
