using UnityEngine;
using System.Collections;

public class WolfAttackCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "ToAttack") 
		{
			//if(attacking)
			//{
			//var explode = target.GetComponent<Explode>() as Explode;
			//explode.OnExplode();
			Destroy(target.gameObject); 
			//This only removes the GO with the hitbox
		} 

		if (target.gameObject.tag == "EnemyToAttack") 
		{
			//if(attacking)
			//{
			//var explode = target.GetComponent<Explode>() as Explode;
			//explode.OnExplode();
			Destroy(target.transform.parent.gameObject); 
			//This removes the parent of the GO w/ the hitbox
		} 

	}
	//}
	
	void OnTriggerExit2D(Collider2D target){
		//readyToAttack = false;
		//attacking = false;
		//anim.SetInteger ("AnimState", 0);
	}
}
