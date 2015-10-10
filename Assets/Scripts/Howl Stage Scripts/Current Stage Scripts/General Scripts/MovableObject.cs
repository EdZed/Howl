using UnityEngine;
using System.Collections;

public class MovableObject : MonoBehaviour {

	BoxCollider2D myBox2D;
	Vector2 newOffset;
	public bool isMovable = false;
	public bool stopXMov;
	public bool stopYMov;
	public float pushDist = 15f;
	public float pushSpeed = 15f;

	// Use this for initialization
	void Start () {
		this.gameObject.AddComponent<SpriteZOrder> ();
		this.gameObject.tag = "Movable";
		//stopXMov = stopYMov = false;
		myBox2D = this.gameObject.GetComponent<BoxCollider2D> ();
		newOffset = new Vector2 (0f, (myBox2D.size.y / 2) * -1);
		myBox2D.offset = newOffset;
	}
	
	// Update is called once per frame
	//void Update () {}

	public void MoveObject(Vector2 moveDir, float waitTime){
		Vector2 currPos = transform.position;
		Vector2 newPos = transform.position;

		if (moveDir.x > 0) {
			if (!stopXMov){ newPos.x -= pushDist; }
		} else if (moveDir.x < 0) {
			if (!stopXMov){ newPos.x += pushDist; }
		} else if (moveDir.y > 0) {
			if(!stopYMov){ newPos.y -= pushDist; }
		} else if (moveDir.y < 0) {
			if(!stopYMov){ newPos.y += pushDist; }
		}

		transform.position = Vector2.MoveTowards(currPos, newPos, pushSpeed * Time.deltaTime);
	}
}
