﻿using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	private Animator animEnemy;

	public BoxCollider2D enemyAttackCollider;
	public GameObject enemyAttackGO;

	public GameObject enemyBear;
	public Rigidbody2D rb2DenemyBear;
	//float speed;
	public float speed;
	//float attackSpeed = 100f;
	float moveSpeed = 4f;
	float attackSpeed = 7f;
	float stopSpeed = 0f;

	private GameObject playerWolf;
	public Transform[] wayPoints = new Transform[2];
	int wayPoint = 1;

	//public BoxCollider2D[] bearColliders = new BoxCollider2D[1];
	public BoxCollider2D bearProximity;
	bool playerNearBear;

	bool bearAttacking;

	// Use this for initialization
	void Start () {
		enemyAttackGO = GameObject.Find("enemyAttack");
		//enemyAttackCollider = enemyAttackGO.GetComponent <BoxCollider2D>();
		enemyAttackCollider = enemyAttackGO.GetComponent <BoxCollider2D> ();
		enemyAttackCollider.enabled = false;

		playerWolf = GameObject.Find("playerWolf");

		speed = attackSpeed;
		enemyBear = this.gameObject;
		rb2DenemyBear = GetComponent<Rigidbody2D> ();
		//bearProximity = GetComponentInChildren<BoxCollider2D> ();

		animEnemy = gameObject.GetComponent<Animator> ();
		animEnemy.SetInteger ("AnimState", 0);

		//wayPoints [0].GetComponent<gameObject>().
		//wayPoint1 = wayPoints [0].GetComponent<gameObject> ();
		playerNearBear = false;
		bearAttacking = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (playerNearBear) {
			//BearAttack();
			//anim below activates BearAttack method
			speed = stopSpeed;

			animEnemy.SetInteger ("AnimState", 3);

			if(bearAttacking == true){
				speed = attackSpeed;
				enemyBear.transform.position = Vector3.MoveTowards(enemyBear.transform.position, playerWolf.transform.position, speed * Time.deltaTime);
				Debug.Log ("Bear attacking player");
			} else{}
			//Debug.Log ("Bear attacking player");
		} else if (!playerNearBear) {
			BearPatrol ();
	
			Debug.Log ("Bear walking");
		} 


	}

//	void OnEnable(){
//		EnemyProximity.TurnNearBearTrue += NearBearOn;
//		//EnemyProximity.TurnNearBearFalse += NearBearOff;
//	}
//
//	void OnDisable(){
//		EnemyProximity.TurnNearBearTrue -= NearBearOn;
//		//EnemyProximity.TurnNearBearFalse -= NearBearOff;
//	}

	public void NearBearOn(){
		playerNearBear = true;
	}

	void NearBearOff(){
		playerNearBear = false;
	}

	void BearAttack(){
		//speed = attackSpeed;
		//rb2DenemyBear.MovePosition (Vector2.MoveTowards (gameObject.transform.position, playerWolf.transform.position, speed * Time.deltaTime));
//		if(bearAttacking = true){
//		enemyBear.transform.position = Vector3.MoveTowards(enemyBear.transform.position, playerWolf.transform.position, speed * Time.deltaTime);
//		Debug.Log ("Bear attacking player");
//		}
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


	void BearPatrol(){
		bearAttacking = false;
		animEnemy.SetInteger ("AnimState", 1);
		//transform.position = Vector2.Lerp(transform.position,wayPoints[wayPoint].transform.position, Time.deltaTime);
		speed = moveSpeed;
		enemyBear.transform.position = Vector3.MoveTowards(enemyBear.transform.position, wayPoints[wayPoint].transform.position, speed * Time.deltaTime);

		if(transform.position == wayPoints[wayPoint].transform.position){  
			if(wayPoint == 1){
				wayPoint=0;
				BearFaceLeft();
				//Debug.Log ("switch to left point here!");
			}  else if(wayPoint == 0){
				wayPoint=1;
				BearFaceRight();
			}  
		}

		//if (wayPoint == 1) {
		if (wayPoints[wayPoint].transform.position.x > enemyBear.transform.position.x) {
			BearFaceRight ();

		} else if (wayPoints[wayPoint].transform.position.x < enemyBear.transform.position.x) {
			BearFaceLeft ();
		}
		//}

//		if(wayPoint == 0){
//			BearFaceLeft();
//		}

	}
	
	void BearAttackTrigger()
	{
		bearAttacking = true;
		enemyAttackCollider.enabled = true;
		print ("enemy attack collider on!");
		print (bearAttacking);
	}
	
	void BearAttackTriggerOff()
	{
		bearAttacking = false;
		enemyAttackCollider.enabled = false;
		print ("enemy attack collider off!");
		print (bearAttacking);
	}

	void BearFaceRight(){
		if (gameObject.transform.localScale.x < 0){
			gameObject.transform.localScale = new Vector3 (1, 1, 1);
		}
	}
	
	void BearFaceLeft(){
		if (gameObject.transform.localScale.x > 0){
			gameObject.transform.localScale = new Vector3 (-1, 1, 1);	
		}	
	}


}
