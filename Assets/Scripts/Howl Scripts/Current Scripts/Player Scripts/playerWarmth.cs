using UnityEngine;
using System.Collections;

public class playerWarmth : MonoBehaviour {
	public DynamicLight dynLightScript;
	public MeshRenderer warmthRend;
	public WorldManager WorldManagerScript;

	// Use this for initialization
	void Start () {
		dynLightScript = GetComponent<DynamicLight> ();


	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//called from DynamicLight script
	public void WarmthRendOff(){

		warmthRend = GetComponent<MeshRenderer> ();
		warmthRend.enabled = false;
	}

	public void GetCold(){
		//warmthRend.enabled = true;

		//sfx here
		dynLightScript.lightRadius -= 1;

		//if hits 2, snap out of spirit world
		if (dynLightScript.lightRadius <= 2) {
			WorldManagerScript.StartCoroutine ("WorldTypeSwitch");
			StartCoroutine("ResetLightRadius");
			CancelInvoke("GetCold");
		}

	}

	public IEnumerator ResetLightRadius(){
		warmthRend.enabled = false;
		yield return new WaitForSeconds(1);
		dynLightScript.lightRadius = 20;
	}

	public void GetWarm(){

		
		//sfx here
		dynLightScript.lightRadius += 1;
		
		//if hits 20, make playerWarmth disappear
		if (dynLightScript.lightRadius >= 20) {
			CancelInvoke("GetWarm");
		}
		
	}
}
