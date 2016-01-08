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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void WorldTypeSwitch(){
		if(WorldType == 0){
			//if currently in real world, switch to spirit
			WorldType = 1;
			SpiritWorldOverlay.amount = .5f;
		} else if(WorldType == 1){
			//if currently in spirit world, switch to world
			WorldType = 0;
			SpiritWorldOverlay.amount = 0f;
		}
	}
}
