using UnityEngine;
using System.Collections;

public class playerWarmth : MonoBehaviour {
	public DynamicLight dynLightScript;
	public MeshRenderer warmthRend;

	// Use this for initialization
	void Start () {
		dynLightScript = GetComponent<DynamicLight> ();


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void WarmthRendOff(){

		warmthRend = GetComponent<MeshRenderer> ();
		warmthRend.enabled = false;
	}
}
