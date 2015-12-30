using UnityEngine;
using System.Collections;

public class RotateFollowPos : MonoBehaviour {

//	GameObject den;
	GameObject playerWolfGO;

	public new GameObject anchorPosGO {
		get;
		private set;
	}

	// Use this for initialization
	void Start () {
		//transform = gameObject.transform;
		playerWolfGO = GameObject.Find("playerWolf");
		anchorPosGO = this.gameObject;
//		particleToDen = GameObject.Find("Particle To Den");
//		den = GameObject.Find("WolfDen");
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (playerWolfGO.transform);
	}
}
