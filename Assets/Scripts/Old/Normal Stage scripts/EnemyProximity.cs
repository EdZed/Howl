using UnityEngine;
using System.Collections;

public class EnemyProximity : MonoBehaviour {
	//public Rigidbody2D rb2DenemyWolf;
	private Animator animEnemy;
	//float speed;
	//float attackSpeed = 200f;

	// Use this for initialization
	void Start () {
		//animEnemy = GetComponent<Animator> ();
		animEnemy = gameObject.GetComponentInParent<Animator> ();
		//anim.SetTrigger("stand");
		animEnemy.SetInteger ("AnimState", 0);
		//rb2DenemyWolf = gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>();
		//rb2DenemyWolf = gameObject.GetComponentInParent<Rigidbody2D> ();
		//speed = attackSpeed;


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "PlayerToAttack") 
		{
			//if(attacking)
			//{
			//var explode = target.GetComponent<Explode>() as Explode;
			//explode.OnExplode();
			//Destroy(target.gameObject); 
			//Destroy(target.transform.parent.gameObject); 

			if (target.transform.parent.gameObject.transform.position.x > transform.position.x) 
			{
				//anim.SetTrigger("walk");
				animEnemy.SetInteger ("AnimState", 3);
				//rb2DenemyWolf.MovePosition (Vector2.MoveTowards (gameObject.transform.parent.gameObject.transform.position, target.gameObject.transform.parent.gameObject.transform.position, speed * Time.deltaTime));
				if (gameObject.transform.parent.gameObject.transform.localScale.x < 0)
					gameObject.transform.parent.gameObject.transform.localScale = new Vector3 (1, 1, 1);
			} else if (target.transform.parent.gameObject.transform.position.x < transform.position.x) {
				//anim.SetTrigger("walkLeft");
				animEnemy.SetInteger ("AnimState", 3);
				//rb2DenemyWolf.MovePosition (Vector2.MoveTowards (gameObject.transform.parent.gameObject.transform.position, target.gameObject.transform.parent.gameObject.transform.position, speed * Time.deltaTime));
				if (gameObject.transform.parent.gameObject.transform.localScale.x > 0)
					gameObject.transform.parent.gameObject.transform.localScale = new Vector3 (-1, 1, 1);	
			} 

		} 	
	}
	//}
	
	void OnTriggerExit2D(Collider2D target){
		//readyToAttack = false;
		//attacking = false;
		animEnemy.SetInteger ("AnimState", 0);
	}
}
