using UnityEngine;
using System.Collections;

public class RockCracked : MonoBehaviour {

	public BoxCollider2D rockCollider;
	Color startColor;
	public bool cracked;

	// Use this for initialization
	void Start () {
		rockCollider = this.gameObject.GetComponent<BoxCollider2D> ();
		startColor = this.gameObject.GetComponent<SpriteRenderer> ().color;
		cracked = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "FallingRock") 
		{
			//code to change rock color and switch on collider
//			Color myOrgColor = this.gameObject.GetComponent<SpriteRenderer>().color;
//			myOrgColor.r += 0.4f;
//			this.gameObject.GetComponent<SpriteRenderer>().color = myOrgColor;
//			cracked = true;
//			print ("rock is cracked");
		} 	

		if (target.gameObject.tag == "playerRunAtk") {
			//Quaternion rotation = Quaternion.Euler(90, 0, 0);
			//gameObject.transform.Rotate (0, 0, -90);
			//fallingDown = true;
			//rockCollider.enabled = false;
			if(cracked == true){
				Destroy(this.gameObject);
			}

			//print ("tree falls down");
			
		} 
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "FallingRock") {
			//coll.gameObject.SendMessage("ApplyDamage", 10);
			Color crackedColor = this.gameObject.GetComponent<SpriteRenderer> ().color;
			crackedColor.r += 3.4f;
			crackedColor = Color.Lerp (startColor, Color.yellow, 4);
			this.gameObject.GetComponent<SpriteRenderer> ().color = crackedColor;

//			Color myHurtColor = playerWolf.GetComponent<SpriteRenderer> ().color;
//			//myHurtColor.r += 0.4f;
//			
//			myHurtColor = Color.Lerp (startColor, Color.white, 4);
//			playerWolf.GetComponent<SpriteRenderer> ().color = myHurtColor;

			cracked = true;
			print ("rock is cracked");
		}
		
	}
	
}
