using UnityEngine;
using System.Collections;

public class FlockMember : MonoBehaviour {

	//store objects current velocity
	private Vector3 velocity = new Vector3();

	private float neighbourRadius = 10.0f;


	//less cost than Get component calls
	//caching position
	public new Transform transform {
		get;
		private set;
	}

	// Use this for initialization
	void Start () {
		transform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		//smooth movement applied relative to world coordinate system
		gameObject.transform.Translate (velocity * Time.deltaTime, Space.World);
	} //end Update**

	public void AddVelocity(Vector3 force){
		velocity += force;
		velocity.Normalize ();
	}

	public Vector3 GetVelocity(){
		return velocity;
	}

	public float GetNeighbourRadius(){
		return neighbourRadius;
	}


}//end Class Script ***





