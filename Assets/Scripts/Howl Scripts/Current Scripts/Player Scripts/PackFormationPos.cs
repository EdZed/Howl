using UnityEngine;
using System.Collections;

public class PackFormationPos : MonoBehaviour {

	public GameObject[] PackFormationPoints = new GameObject[5];
	public int packSize = 0;
	public bool doesPackExist = false;
	public GameObject[] PackFormationGOSlots = new GameObject[5];
	public AudioSource[] packMusicLayers = new AudioSource[4];

	//event**
	public delegate void PackNotExist();
	public static event PackNotExist OnPackNotExist;

	public delegate void PackDoesExist();
	public static event PackDoesExist OnPackDoesExist;

	// Use this for initialization
	void Start () {
		//Sends to WolfDen
		if(OnPackNotExist != null){
			OnPackNotExist();
		}
		packMusicLayers [0].mute = true;
		packMusicLayers [1].mute = true;
		packMusicLayers [2].mute = true;
		packMusicLayers [3].mute = true;

	}
	
	// Update is called once per frame
//	void Update () {
//		if (doesPackExist) {
//			if(packSize == 1){
//
//
//			} else if(packSize == 2){
//				
//			}
//			else if(packSize == 3){
//				
//			}
//			else if(packSize == 4){
//				
//			}
//			else if(packSize == 5){
//				
//			}
//		}
//	} //end Update

	void WelcomePackMember (){
		packSize += 1;
		Debug.Log ("pack size is: " + packSize);

		//if just got your first pack member and the bool was false, now it's true
		if (!doesPackExist && packSize >= 1) {
			//only calls once, can send message to wolf den to block howl from switching world
			doesPackExist = true;
			//Sends to WolfDen
			if(OnPackDoesExist != null){
				OnPackDoesExist();
			}
		}

	}//end WelcomePackMember

	public void MinusPackMember(){
		packSize -= 1;
		if (doesPackExist && packSize == 0) {
			if(OnPackNotExist != null){
				OnPackNotExist();
			}
			doesPackExist = false;
		}
	}

	void OnEnable()
	{
		FollowPlayer.AddPackMember += WelcomePackMember;
	} //end OnEnable
	
	
	void OnDisable()
	{
	FollowPlayer.AddPackMember -= WelcomePackMember;

	}//end OnDisable



}//end class script




