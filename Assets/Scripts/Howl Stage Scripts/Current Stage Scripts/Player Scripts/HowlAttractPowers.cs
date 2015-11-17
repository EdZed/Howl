using UnityEngine;
using System.Collections;

public class HowlAttractPowers : MonoBehaviour {
	GameObject PlayerWolfGO;
	public bool howlFreeze;

	public delegate void PowerActivate();
	public static event PowerActivate HowlFreezePower;

	// Use this for initialization
	void Start () {
	
		PlayerWolfGO = GameObject.Find ("playerWolf");
		howlFreeze = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "LostWolfOrange") {
			PlayerWolfGO.GetComponent<PCWolfInput>().runAtk = true;

		} 

		if (target.gameObject.tag == "LostWolfGreen") {
			howlFreeze = true;
			
		} 
		if (target.gameObject.tag == "EnemyToAttack") {
			if(howlFreeze == true){
				//StartCoroutine(BearImmobolize());
				//sends message to ?
				if (HowlFreezePower != null) {
					HowlFreezePower ();
					print ("Enemy almost frozen");
				}
			}
			
		} 
	}//end ontrigger
	

}
