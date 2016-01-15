using UnityEngine;
using System.Collections;

public class playerWarmth : MonoBehaviour {
	public DynamicLight dynLightScript;
	public MeshRenderer warmthRend;
	public WorldManager WorldManagerScript;
	public bool isRealWorld;

	// Use this for initialization
	void Start () {
		dynLightScript = GetComponent<DynamicLight> ();
		isRealWorld = true;


	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//called from DynamicLight script
	public void WarmthRendOff(){
		if (warmthRend == null) {
			warmthRend = GetComponent<MeshRenderer> ();
		}
		//warmthRend = GetComponent<MeshRenderer> ();
		warmthRend.enabled = false;
	}
	public void WarmthRendOn(){
		//warmthRend = GetComponent<MeshRenderer> ();
		warmthRend.enabled = true;
	}

	public void GetCold(){
		//warmthRend.enabled = true;
		if (!warmthRend.enabled) {
			WarmthRendOn();
		}

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
			WarmthRendOff();
			CancelInvoke("GetWarm");

		}
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Fire") {
			//			coll.gameObject.GetComponent<PCWolfInput>().WarmingUp = true;
			//			coll.gameObject.GetComponent<PCWolfInput>().WolfEnterTime = Time.time;
			InvokeRepeating("GetWarm",.01f,1);
			CancelInvoke("GetCold");
			Debug.Log ("Player in warm area");
		}
	}
	
	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.tag == "Fire") {
			//coll.gameObject.GetComponent<PCWolfInput>().WarmingUp = false;

			//if exit b/c in real world
			if(isRealWorld){
				StartCoroutine("ResetLightRadius");
			}else if(!isRealWorld){
			//else if exit while still in spirit world
			Debug.Log ("Player in cold area");
			InvokeRepeating("GetCold",2,.9f);
			CancelInvoke("GetWarm");
			}
		}
	}
}
