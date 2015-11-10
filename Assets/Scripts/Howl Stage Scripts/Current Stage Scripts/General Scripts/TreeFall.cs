using UnityEngine;
using System.Collections;

public class TreeFall : MonoBehaviour {
	public BoxCollider2D treeCollider;
	public bool fallingDown;

	// Use this for initialization
	void Start () {
		treeCollider = this.gameObject.GetComponent<BoxCollider2D> ();
		fallingDown = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (fallingDown == true) {
//			while(gameObject.transform.rotation.z  270){
//				gameObject.transform.Rotate (0, 0, -10 * Time.deltaTime);
//			}
//			if(gameObject.transform.rotation.z <= 270){
//
//				gameObject.transform.Rotate(0, 0, 270);
//			}else if(gameObject.transform.rotation.z != 270){
//				gameObject.transform.Rotate (0, 0, -10 * Time.deltaTime);
//			}

		}
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "playerRunAtk") {
			//Quaternion rotation = Quaternion.Euler(90, 0, 0);
			gameObject.transform.Rotate (0, 0, -90);
			fallingDown = true;
			treeCollider.enabled = false;
			print ("tree falls down");
			
		} 
	}//end ontrigger

//	void OnCollisionEnter(Collision collision) {
//		if (collision.gameObject.tag == "playerRunAtk") {
//			//Transform.eulerAngles.y = 90;
//			//gameObject.transform.eulerAngles.y = 90;
//			Quaternion rotation = Quaternion.Euler(90, 0, 0);
//			print ("tree falls down");
//		} 
//		
//	}
}
