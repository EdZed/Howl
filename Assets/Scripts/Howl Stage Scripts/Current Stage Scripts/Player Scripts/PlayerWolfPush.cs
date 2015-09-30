using UnityEngine;
using System.Collections;

public class PlayerWolfPush : MonoBehaviour {
	GameObject myPlayerWolf;
	SpriteRenderer WolfSprRend;
	bool objMoving; //to use later for animation

	// Use this for initialization
	void Start () {
		myPlayerWolf = transform.parent.gameObject;
		WolfSprRend = GetComponentInParent<SpriteRenderer> ();
	}

	void OnCollisionEnter2D(Collision2D myCol){
		MovableObject moveScript = myCol.gameObject.GetComponent<MovableObject> ();
		Vector3 contactDir = myCol.contacts [0].normal;

		if (moveScript != null) {
			myPlayerWolf.GetComponent<PCWolfInput> ().canMove = false;
			moveScript.MoveObject (contactDir, 2f);
			myPlayerWolf.GetComponent<PCWolfInput> ().canMove = true;
		}
	}
}
