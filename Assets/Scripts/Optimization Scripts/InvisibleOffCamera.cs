using UnityEngine;
using System.Collections;

public class InvisibleOffCamera : MonoBehaviour {

	Animator GOAnimator;
	// Use this for initialization
	void Start () {
		GOAnimator = this.gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
//	void Update () {
//	
//	}

	void OnBecameVisible() {
		//enabled = true;
		GOAnimator.enabled = true;
		Debug.Log ("Objects prev off camera now visible");
	}
	void OnBecameInvisible() {
		//enabled = false;
		GOAnimator.enabled = false;
		Debug.Log ("Objects prev on camera now invisible");
	}

}
