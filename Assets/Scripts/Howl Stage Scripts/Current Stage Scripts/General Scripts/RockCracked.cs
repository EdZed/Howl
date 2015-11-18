using UnityEngine;
using System.Collections;

public class RockCracked : MonoBehaviour {

	public BoxCollider2D rockCollider;
	Color startColor;
	public bool cracked;

	public AudioSource[] rockBroken = new AudioSource[1];
	
	//to make rock spriterenderer transparent
	private SpriteRenderer spriteRenderer;
	private Color start;
	private Color end;
	private float t = 0.0f;
	private Renderer rendMaterialColor;
	
	bool isRockBreaking;

	// Use this for initialization
	void Start () {
		rockCollider = this.gameObject.GetComponent<BoxCollider2D> ();
		startColor = this.gameObject.GetComponent<SpriteRenderer> ().color;

		rockBroken [0].enabled = false;
		isRockBreaking = false;
		
		//to make rock spriterenderer transparent
		spriteRenderer = GetComponent<SpriteRenderer> ();
		start = spriteRenderer.color;
		end = new Color (start.r, start.g, start.b, 0.0f);
		rendMaterialColor = GetComponent<Renderer> ();

		//cracked = false;

	if (cracked == true) {
			Color crackedColor = this.gameObject.GetComponent<SpriteRenderer> ().color;
			crackedColor.r += 3.4f;
			crackedColor = Color.Lerp (startColor, Color.yellow, 4);
			this.gameObject.GetComponent<SpriteRenderer> ().color = crackedColor;
		}
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		if (isRockBreaking == true) {
			rendMaterialColor.material.color = Color.Lerp (start, end, t / 1.5f);
			//			if (rendMaterialColor.material.color.a <= 0.0) {
			//				Destroy (gameObject);
			//			}
		}
	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "FallingRock") 
		{
			//code to change rock color and switch on collider
//			Color myOrgColor = this.gameObject.GetComponent<SpriteRenderer>().color;
//			myOrgColor.r += 0.4f;
//			this.gameObject.GetComponent<SpriteRenderer>().color = myOrgColor;
//			cracked = true;
//			print ("rock is cracked");
		} 	

		if (target.gameObject.tag == "playerRunAtk") {
			//Quaternion rotation = Quaternion.Euler(90, 0, 0);
			//gameObject.transform.Rotate (0, 0, -90);
			//fallingDown = true;
			//rockCollider.enabled = false;
			if(cracked == true){
				//Destroy(this.gameObject);
				rockBroken [0].enabled = true;
				StartCoroutine(RockBreaks());
			}

			//print ("tree falls down");
			
		} 
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "FallingRock") {
			//coll.gameObject.SendMessage("ApplyDamage", 10);
			Color crackedColor = this.gameObject.GetComponent<SpriteRenderer> ().color;
			crackedColor.r += 3.4f;
			crackedColor = Color.Lerp (startColor, Color.yellow, 4);
			this.gameObject.GetComponent<SpriteRenderer> ().color = crackedColor;

//			Color myHurtColor = playerWolf.GetComponent<SpriteRenderer> ().color;
//			//myHurtColor.r += 0.4f;
//			
//			myHurtColor = Color.Lerp (startColor, Color.white, 4);
//			playerWolf.GetComponent<SpriteRenderer> ().color = myHurtColor;

			cracked = true;
			//print ("rock is cracked");
		}
		
	}

	IEnumerator RockBreaks(){
		isRockBreaking = true;
		rockCollider.enabled = false;
		yield return new WaitForSeconds(.5f);
		Destroy(this.gameObject);
	}
	
}
