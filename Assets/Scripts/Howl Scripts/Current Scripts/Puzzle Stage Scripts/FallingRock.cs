using UnityEngine;
using System.Collections;

public class FallingRock : MonoBehaviour {

	Vector3 startPos;
	float fallSpeed = 14.0f;
	GameObject myPlayerWolf;
	public AudioSource[] rockHitGround = new AudioSource[1];

	//to make rock spriterenderer transparent
	private SpriteRenderer spriteRenderer;
	private Color start;
	private Color end;
	private float t = 0.0f;
	private Renderer rendMaterialColor;

	bool isRockBreaking;
//	Color dmgColor;
//	float dmgTimeStart;
//	float dmgTimeLength = 0.5f;
//	float invincTimeLength = 0.8f; //MUST be longer than dmgTimeLength
	//GameObject rockShadow;
	//public GameObject[] rockShadowSpawnPos = new GameObject[4];

	//GameObject parentFallRocks;

	// Use this for initialization
	void Start () {
		//set rock spawn pos to spawn again after being destroyed
		startPos = gameObject.transform.position;

		myPlayerWolf = GameObject.Find("playerWolf");
		rockHitGround [0].enabled = false;

		//to make rock spriterenderer transparent
		spriteRenderer = GetComponent<SpriteRenderer> ();
		start = spriteRenderer.color;
		end = new Color (start.r, start.g, start.b, 0.0f);
		rendMaterialColor = GetComponent<Renderer> ();

		isRockBreaking = false;

		//spawn shadow positions
		//parentFallRocks = gameObject.transform.parent.gameObject;
		//parentFallRocks = gameObject.transform.
		//ParentGameObject.transform.GetChild (0).gameObject; 
//		rockShadowSpawnPos [0] = parentFallRocks.transform.GetChild (5).gameObject;
//		rockShadowSpawnPos [1] = parentFallRocks.transform.GetChild (6).gameObject;
//		rockShadowSpawnPos [2] = parentFallRocks.transform.GetChild (7).gameObject;
//		rockShadowSpawnPos [3] = parentFallRocks.transform.GetChild (8).gameObject;
//		rockShadowSpawnPos[0] = GameObject.Find("RockShadowPos1");
//		rockShadowSpawnPos[1] = GameObject.Find("RockShadowPos2");
//		rockShadowSpawnPos[2] = GameObject.Find("RockShadowPos3");
//		rockShadowSpawnPos[3] = GameObject.Find("RockShadowPos4");

		//spawn shadow
		//rockShadow = Instantiate(Resources.Load("Shadow")) as GameObject;
		//print ("start pos:" +startPos + "shadow pos:" + rockShadowSpawnPos);


//		if(startPos.x == rockShadowSpawnPos[0].transform.position.x){
//			rockShadow.transform.position = rockShadowSpawnPos[0].transform.position;
//		} else if(startPos.x == rockShadowSpawnPos[1].transform.position.x){
//			rockShadow.transform.position = rockShadowSpawnPos[1].transform.position;
//		} else if(startPos.x == rockShadowSpawnPos[2].transform.position.x){
//			rockShadow.transform.position = rockShadowSpawnPos[2].transform.position;
//		} else if(startPos.x == rockShadowSpawnPos[3].transform.position.x){
//			rockShadow.transform.position = rockShadowSpawnPos[3].transform.position;
//		}

	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
		t += Time.deltaTime;
		if (isRockBreaking == true) {
			rendMaterialColor.material.color = Color.Lerp (start, end, t / 1.5f);
//			if (rendMaterialColor.material.color.a <= 0.0) {
//				Destroy (gameObject);
//			}
		}
		
		//GetComponent<Renderer>().material.color = Color.Lerp (start, end, t / 2);

	}

	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.gameObject.tag == "RockDisappear") 
		{
			rockHitGround [0].enabled = true;
			GameObject instance = Instantiate(Resources.Load("Falling Rock")) as GameObject;
			//print ("Spawn new rock");
			//This was preventing the rocks from spawning
			//instance.transform.parent = transform;
			instance.transform.position = startPos;
			StartCoroutine(RockBreaks());
			//RockBreaks();
		} 	

		if (target.gameObject.tag == "PlayerToAttack") 
		{
			rockHitGround [0].enabled = true;
			if (!myPlayerWolf.GetComponent<PCWolfInput>().invincible){
				myPlayerWolf.GetComponent<PCWolfInput> ().playerHealth-= 1 ;
				myPlayerWolf.GetComponent<PCWolfInput>().dmgTimeStart = Time.time;
				myPlayerWolf.GetComponent<PCWolfInput>().damaged = true;
			}

			GameObject instance = Instantiate(Resources.Load("Falling Rock")) as GameObject;
			//instance.transform.parent = transform;
			instance.transform.position = startPos;
			StartCoroutine(RockBreaks());
			//RockBreaks();
		} 

		if (target.gameObject.tag == "Movable") 
		{
//			myPlayerWolf.GetComponent<PCWolfInput> ().playerHealth-= 1 ;
//			StartCoroutine(PlayerHurtFlash());
			
			//GameObject instance = Instantiate(Resources.Load("Falling Rock")) as GameObject;
			//instance.transform.parent = transform;
			//instance.transform.position = startPos;
			rockHitGround [0].enabled = true;
			StartCoroutine(RockBreaks());
			//RockBreaks();
		} 
	}

	IEnumerator RockBreaks(){
		isRockBreaking = true;
		yield return new WaitForSeconds(.5f);
		Destroy(this.gameObject);
	}



}










