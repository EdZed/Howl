using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WolfDen : MonoBehaviour {
	
	//Wolf Den Art
	public GameObject wolfDenArt;
	private Animator wolfDenAnim;	
	public bool isTriggering;
	public GameObject[] spiritAnim = new GameObject[4];	

	//event**
	public delegate void Rescues();
	public static event Rescues WolfRescued;
	
	// Use this for initialization
	void Start () {
//		spiritAnim [0].GetComponent<Animator> ().enabled = false;
//		spiritAnim [1].GetComponent<Animator> ().enabled = false;
//		spiritAnim [2].GetComponent<Animator> ().enabled = false;
//		spiritAnim [3].GetComponent<Animator> ().enabled = false;
		
//		spiritAnim [0].GetComponent<SpriteRenderer> ().enabled = false;
//		spiritAnim [1].GetComponent<SpriteRenderer> ().enabled = false;
//		spiritAnim [2].GetComponent<SpriteRenderer> ().enabled = false;
//		spiritAnim [3].GetComponent<SpriteRenderer> ().enabled = false;
		
		//wolf Den
		wolfDenArt = GameObject.Find ("WolfDen");
		wolfDenAnim = wolfDenArt.GetComponent<Animator> ();

		wolfDenAnim.SetInteger ("DenAnimState", 0);
		isTriggering = false;
	}//end start
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D target)
	{
		isTriggering = true;
		
		//send message to WolfDeManager
		if (target.gameObject.tag == "LostWolf") 
		{
			if(WolfRescued != null){
				WolfRescued();
			}
		}//end target tag LostWolf
	}//end on trigger enter

}//end whole class
