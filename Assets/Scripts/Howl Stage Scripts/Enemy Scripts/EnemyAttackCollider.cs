using UnityEngine;
using System.Collections;

public class EnemyAttackCollider : MonoBehaviour {
	GameObject myPlayerWolf;

	// Use this for initialization
	void Start () {
		myPlayerWolf = GameObject.Find("playerWolf");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "PlayerToAttack") 
		{
			myPlayerWolf.GetComponent<PCWolfInput> ().playerHealth-= 1 ;
			StartCoroutine(PlayerHurtFlash());



//			myPlayerWolf.GetComponent<PCWolfInput> ().s
//			renderer.material.color = colors[0];
//			yield WaitForSeconds(.5);
//			renderer.material.color = colors[1];
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

	IEnumerator PlayerHurtFlash(){
		Color myOrgColor = myPlayerWolf.GetComponent<SpriteRenderer>().color;

		myOrgColor.r += 0.8f;
		myPlayerWolf.GetComponent<SpriteRenderer>().color = myOrgColor;
		//yield return new WaitForSeconds(.5);
		yield return new WaitForSeconds(.1f);
		myOrgColor.r -= 0.8f;
		myPlayerWolf.GetComponent<SpriteRenderer>().color = myOrgColor;
		yield return new WaitForSeconds(.1f);
		myOrgColor.r += 0.8f;
		myPlayerWolf.GetComponent<SpriteRenderer>().color = myOrgColor;
		yield return new WaitForSeconds(.1f);
		myOrgColor.r -= 0.8f;
		myPlayerWolf.GetComponent<SpriteRenderer>().color = myOrgColor;
	}
	
	void OnTriggerExit2D(Collider2D target){
		//readyToAttack = false;
		//attacking = false;
		//anim.SetInteger ("AnimState", 0);
	}
}
