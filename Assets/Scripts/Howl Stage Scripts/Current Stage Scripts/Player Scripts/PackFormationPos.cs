using UnityEngine;
using System.Collections;

public class PackFormationPos : MonoBehaviour {

	public GameObject[] PackFormationPoints = new GameObject[5];
	public int packSize = 0;
	public bool doesPackExist = false;
	public GameObject[] PackFormationGOSlots = new GameObject[5];
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (doesPackExist) {
			if(packSize == 1){


			} else if(packSize == 2){
				
			}
			else if(packSize == 3){
				
			}
			else if(packSize == 4){
				
			}
			else if(packSize == 5){
				
			}
		}
	} //end Update

	void WelcomePackMember (){
		packSize += 1;
		Debug.Log ("pack size is: " + packSize);
		
		if (!doesPackExist && packSize >= 1) {
			doesPackExist = true;
		}

	}//end WelcomePackMember

	void OnEnable()
	{
		FollowPlayer.AddPackMember += WelcomePackMember;
	} //end OnEnable
	
	
	void OnDisable()
	{
	FollowPlayer.AddPackMember -= WelcomePackMember;

	}//end OnDisable



}//end class script




