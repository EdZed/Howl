using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	//Vector3 targetPos = Vector3.zero;

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
	// Update is called once per frame
	void Update () {

		#if UNITY_IOS

		if (Input.touchCount > 0) 
		{
			Touch touch = Input.touches [0];
			
			switch(touch.phase)
			{
			case TouchPhase.Began:
				//targetPos = Camera.main.ScreenToWorldPoint (touch.position);
				RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint((Input.GetTouch (0).position)), Vector2.zero);
	
				if (hit.collider != null && hit.collider.gameObject.tag == "Title")
				{
					//hit.GetComponent<TouchObjectScript>().ApplyForce(); 
					Application.LoadLevel ("Howl Last Stage");
				}

				//targetPos = Camera.main.ScreenToWorldPoint (touch.position);
				//print ("wolf started!");
				break;
			case TouchPhase.Stationary:

				//break;
			case TouchPhase.Moved:

				//targetPos = Camera.main.ScreenToWorldPoint (touch.position);
				break;
			default:
			case TouchPhase.Ended:

				//break;
				
			case TouchPhase.Canceled:

				break;
				
			}//end of switch touch.phase	
			
		}//end touch count
		#endif

		#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE
		if(Input.GetMouseButton(0)){
			
			
			//Vector3 targetPos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint((Input.mousePosition)), Vector2.zero);
			
			if (hit.collider != null && hit.collider.gameObject.tag == "Title")
			{
				//hit.GetComponent<TouchObjectScript>().ApplyForce(); 
				//Application.LoadLevel ("Howl Last Stage");
				Application.LoadLevel ("Howl PS Demo");
			}
			//			if (Physics.Raycast(targetPos)){
			//				//Instantiate(particle, transform.position, transform.rotation);
			//			}
		}//ends getmousebuttondown

		if(Input.GetKeyUp(KeyCode.Space)) {
			Application.LoadLevel ("Howl PS Demo");
			
		}
		#endif
	}//end update
}//end whole class
