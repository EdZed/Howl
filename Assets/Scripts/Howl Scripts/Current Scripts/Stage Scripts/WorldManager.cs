using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {

	[Range (0,2)]
	public int WorldType;
	/*
	  0 = Real World 
	  1 = Spirit World
	  2 = Both Worlds
	*/

	public CC_Vintage SpiritWorldOverlay;
	public playerWarmth playerWarmthScript;

	public GameObject HearDenGO;

	public bool isWorldTransitioning;
	//to change camera filter color
	public bool isWorldCold;

	//Sends to Followplayer script?
	public delegate void LostWolfActive();
	public static event LostWolfActive OnLostWolfActive;
	public static event LostWolfActive OnLostWolfNotActive;

	public delegate void WarmActive();
	public static event WarmActive OnWarmActive;
	public static event WarmActive OnWarmNotActive;

	//Den Stuff
	public int rescuedWolvesCounter;
	public int rescueAmountOpenExit;

	public GameObject StageExitGO;
	public BoxCollider2D StageExitCol;

	// Use this for initialization
	void Start () {
		playerWarmthScript = GameObject.Find ("playerWolf").transform.Find ("playerWarmth").GetComponent<playerWarmth> ();
		isWorldTransitioning = false;
		StageExitGO = GameObject.Find("StageExit");
		StageExitCol = StageExitGO.GetComponent <BoxCollider2D> ();

		if(isWorldCold){
			SpiritWorldOverlay.filter = CC_Vintage.Filter.F1977;
		} else if(!isWorldCold){
			SpiritWorldOverlay.filter = CC_Vintage.Filter.Kelvin;
			//CC_Vintage.Filter.Kelvin;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(rescuedWolvesCounter == rescueAmountOpenExit){
			//turn coll off on exit
			StageExitCol.enabled = false;
		}
	}

	public IEnumerator WorldTypeSwitch(){
		if(WorldType == 0){
			//if currently in real world, switch to spirit

			SpiritWorldOverlay.amount = .5f;
			HearDenGO.SetActive(false);
			Debug.Log ("world type switch function working");
			//LostWolfGO.SetActive (true);
			//sends to FollowPlayer script
			if(OnLostWolfActive != null){
				OnLostWolfActive();
				Debug.Log ("Event msg sent?");
			}

			if(OnWarmActive != null){
				OnWarmActive();
				Debug.Log ("on warm active sent?");
			}
			//change layer on playerwarmth to trigger with warm Area
			playerWarmthScript.isRealWorld = false;
			playerWarmthScript.gameObject.layer = LayerMask.NameToLayer("Default");

			//send event (invoke?) to playerWarmth script which makes radius appear and decrease
			if(isWorldCold){
				playerWarmthScript.InvokeRepeating("GetCold",1,1);
				playerWarmthScript.warmthRend.enabled = true;
			}
			WorldType = 1;


		} else if(WorldType == 1){
			//if currently in spirit world, switch to real world
			isWorldTransitioning = true;

			SpiritWorldOverlay.amount = 0f;
//			if(OnLostWolfActive != null){
//				OnLostWolfActive();
//				Debug.Log ("Event msg sent?");
//			}
			if(OnLostWolfNotActive != null){
				OnLostWolfNotActive();
				Debug.Log ("Event msg sent?");
			}
			if(OnWarmNotActive != null){
				OnWarmNotActive();
				Debug.Log ("on warm NOT active sent?");
			}
			//change layer on playerwarmth to not trigger with warm Area anymore
			playerWarmthScript.isRealWorld = true;
			playerWarmthScript.gameObject.layer = LayerMask.NameToLayer("IgnoreLayer");
			playerWarmthScript.StartCoroutine("ResetLightRadius");
			HearDenGO.SetActive(true);
			yield return new WaitForSeconds(4);

			isWorldTransitioning = false;
			WorldType = 0;


		}
	}

	void OnEnable()
	{
		FollowPlayer.OnAddToCounter += AddToRescueCounter;
	}
	
	
	void OnDisable()
	{
		FollowPlayer.OnAddToCounter -= AddToRescueCounter;
	}

	void AddToRescueCounter(){
		rescuedWolvesCounter += 1;
	}
}









