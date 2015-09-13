using UnityEngine;
using System.Collections;

public class EnemyAttackCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
			//Destroy(target.transform.parent.gameObject); 
		} 
		
//		if (target.gameObject.tag == "EnemyToAttack") 
//		{
//			//if(attacking)
//			//{
//			//var explode = target.GetComponent<Explode>() as Explode;
//			//explode.OnExplode();
//			Destroy(target.transform.parent.gameObject); 
//		} 
		
	}
	//}
	
	void OnTriggerExit2D(Collider2D target){
		//readyToAttack = false;
		//attacking = false;
		//anim.SetInteger ("AnimState", 0);
	}
}
