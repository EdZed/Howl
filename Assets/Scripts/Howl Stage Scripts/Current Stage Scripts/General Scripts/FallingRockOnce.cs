using UnityEngine;
using System.Collections;

public class FallingRockOnce : MonoBehaviour {

	Vector3 startPos;
	float fallSpeed = 14.0f;
	GameObject myPlayerWolf;
	//GameObject rockShadow;
	//public GameObject[] rockShadowSpawnPos = new GameObject[4];

	//GameObject parentFallRocks;

	// Use this for initialization
	void Start () {
		//set rock spawn pos to spawn again after being destroyed
		startPos = gameObject.transform.position;

		myPlayerWolf = GameObject.Find("playerWolf");

		//spawn shadow positions
		//parentFallRocks = gameObject.transform.parent.gameObject;
		//parentFallRocks = gameObject.transform.
		//ParentGameObject.transform.GetChild (0).gameObject; 
//		rockShadowSpawnPos [0] = parentFallRocks.transform.GetChild (5).gameObject;
//		rockShadowSpawnPos [1] = parentFallRocks.transform.GetChild (6).gameObject;
//		rockShadowSpawnPos [2] = parentFallRocks.transform.GetChild (7).gameObject;
//		rockShadowSpawnPos [3] = parentFallRocks.transform.GetChild (8).gameObject;
//		rockShadowSpawnPos[0] = GameObject.Find("RockShadowPos1");
//		rockShadowSpawnPos[1] = GameObject.Find("RockShadowPos2");
//		rockShadowSpawnPos[2] = GameObject.Find("RockShadowPos3");
//		rockShadowSpawnPos[3] = GameObject.Find("RockShadowPos4");

		//spawn shadow
		//rockShadow = Instantiate(Resources.Load("Shadow")) as GameObject;
		//print ("start pos:" +startPos + "shadow pos:" + rockShadowSpawnPos);


//		if(startPos.x == rockShadowSpawnPos[0].transform.position.x){
//			rockShadow.transform.position = rockShadowSpawnPos[0].transform.position;
//		} else if(startPos.x == rockShadowSpawnPos[1].transform.position.x){
//			rockShadow.transform.position = rockShadowSpawnPos[1].transform.position;
//		} else if(startPos.x == rockShadowSpawnPos[2].transform.position.x){
//			rockShadow.transform.position = rockShadowSpawnPos[2].transform.position;
//		} else if(startPos.x == rockShadowSpawnPos[3].transform.position.x){
//			rockShadow.transform.position = rockShadowSpawnPos[3].transform.position;
//		}

	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "RockDisappear") 
		{
			//GameObject instance = Instantiate(Resources.Load("Falling Rock")) as GameObject;
			//print ("Spawn new rock");
			//This was preventing the rocks from spawning
			//instance.transform.parent = transform;
			//instance.transform.position = startPos;
			Destroy(this.gameObject);
		} 	

		if (target.gameObject.tag == "PlayerToAttack") 
		{
			myPlayerWolf.GetComponent<PCWolfInput> ().playerHealth-= 1 ;
			StartCoroutine(PlayerHurtFlash());

			//GameObject instance = Instantiate(Resources.Load("Falling Rock")) as GameObject;
			//instance.transform.parent = transform;
			//instance.transform.position = startPos;
			Destroy(this.gameObject);
		} 

		if (target.gameObject.tag == "Movable") 
		{
			//			myPlayerWolf.GetComponent<PCWolfInput> ().playerHealth-= 1 ;
			//			StartCoroutine(PlayerHurtFlash());
			
			//GameObject instance = Instantiate(Resources.Load("Falling Rock")) as GameObject;
			//instance.transform.parent = transform;
			//instance.transform.position = startPos;
			Destroy(this.gameObject);
		} 
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Movable") {
			Destroy(this.gameObject);
		}
		
	}

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
}
