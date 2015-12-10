using UnityEngine;
using System.Collections;

public class AffectionTrigger : MonoBehaviour {
	GameObject playerWolfGO;

	//public bool howlFreeze;

	//Both Events send message to FollowPlayer.cs
	public delegate void AffectionAnimTrigger();
	public static event AffectionAnimTrigger AffectionAnimOn;

	public delegate void AffectionAnimTriggerOff();
	public static event AffectionAnimTriggerOff AffectionAnimOff;

	public delegate void PlayerCloseToLostWolf();
	public static event PlayerCloseToLostWolf OnPlayerClose;
	public static event PlayerCloseToLostWolf OnPlayerAway;

	//bool FollowPlayerAffection;
	//ScriptableObject PlayerWolfScript;
	PCWolfInput PlayerWolfScript;
	bool playerWolfClose;
	bool callOnceInvoke;
	bool callOnceNoInvoke;

	// Use this for initialization
	void Start () {
		//LostWolfGO = 
		playerWolfGO = GameObject.Find ("playerWolf");
		//howlFreeze = false;
		PlayerWolfScript = playerWolfGO.GetComponent<PCWolfInput> ();
		playerWolfClose = false;
		callOnceInvoke = false;
		callOnceNoInvoke = true;
		//PlayerWolfGO.GetComponent<PCWolfInput>()

	}
	
	// Update is called once per frame
	void Update () {
		if (playerWolfClose == true) {
			if (PlayerWolfScript.affection == true) {
				//PlayerWolfGO.GetComponent<PCWolfInput>().runAtk = true;
				if (AffectionAnimOn != null) {
					AffectionAnimOn ();
					Debug.Log ("affect trig stay");
				}
				if (!callOnceInvoke){
					//makes player start healing if they're close to lost wolf and showing affection
					PlayerWolfScript.InvokeRepeating("PlayerWarmUp", .5f,.5f);
					//stops cold from hurting ONLY while healing
					PlayerWolfScript.CancelInvoke("OutInCold");
					//CancelInvoke("OutInCold");
					callOnceInvoke = true;
					callOnceNoInvoke = false;
				}

			} else {
				if (AffectionAnimOff != null) {
					AffectionAnimOff ();
					Debug.Log ("affect trig exit");
				}
				if (!callOnceNoInvoke){
					//Player no longer cuddling so no longer warming up
					PlayerWolfScript.CancelInvoke("PlayerWarmUp");
					//flips boolean back to cause cold to continue hurting.
					PlayerWolfScript.callOnce = false;
					callOnceNoInvoke = true;
					callOnceInvoke = false;
				}

			}
	
		}//end playerwolfclose true
	}

//	void OnTriggerEnter2D(Collider2D target){
//		if (target.gameObject.tag == "playerAffection") {
//			if (PlayerWolfScript.affection == true) {
//				//PlayerWolfGO.GetComponent<PCWolfInput>().runAtk = true;
//				if (AffectionAnimOn != null) {
//					AffectionAnimOn ();
//					Debug.Log ("affect trig enter");
//				}
//
//			} 
//		}
//	}//end ontriggerEnter

	void OnTriggerStay2D(Collider2D target){
		if (target.gameObject.tag == "playerAffection") {
			Debug.Log ("player wolf stay");
			playerWolfClose = true;

//			if (OnPlayerClose != null) {
//				OnPlayerClose ();
//				//sent to PCWolfInput
//				//Debug.Log ("affect trig exit");
//			}
			
		}
	}//end ontriggerStay

	void OnTriggerExit2D(Collider2D target){
		if (target.gameObject.tag == "playerAffection") {
			//PlayerWolfGO.GetComponent<PCWolfInput>().runAtk = true;
			playerWolfClose = false;
			if (AffectionAnimOff != null) {
				AffectionAnimOff ();
				Debug.Log ("affect trig exit");
			}

//			if(OnPlayerAway !=null){
//				OnPlayerAway();
//			}
			
		} 
		
	}//end ontriggerStay

}
