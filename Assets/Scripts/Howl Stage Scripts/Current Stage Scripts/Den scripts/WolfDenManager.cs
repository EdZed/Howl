using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WolfDenManager : MonoBehaviour {

	public Rigidbody2D rb2DLostWolf;
	public GameObject PlayerWolfGO;
	//Lost wolves spawn points
	public GameObject[] lostWolfSpawnPos = new GameObject[4];
	
	//public GameObject lostWolfSpawnPointsGO;
	
	//wolf Den Music Layers
	public GameObject musicLayer1;
	public GameObject musicLayer2;
	public GameObject musicLayer3;
	public GameObject musicLayer4;
	
	//Wolf Den Art
	public GameObject wolfDenArt;
	private Animator wolfDenAnim;
	//private Animator LostWolfAnim;
	
	public int rescuedWolvesCounter;
	
	public bool isTriggering;
	
	public ParticleSystem[] sources = new ParticleSystem[2];
	//public Animator[] anim = new Animator[1];
	public GameObject[] spiritAnim = new GameObject[4];
	//0 = portal
	//1 = snow
	public GameObject[] musicLayers = new GameObject[4];
	
	//snow particle Game Objects
	public GameObject leftSide;
	public GameObject rightSide;

	public delegate IEnumerator LastWolfCollected();
	public static event LastWolfCollected RedWolfCollected;

	public delegate void PlayerHealth();
	public static event PlayerHealth OnHeal;

	// Use this for initialization
	void Start () {
		
		//LostWolfGO = this.gameObject;

		//lostWolfSpawnPointsGO = GameObject.Find ("LostWolfSpawnPoints");

		//Wolf Den Music Layers
		//		musicLayer1 = GameObject.Find ("Music Layer 1");
		//		musicLayer2 = GameObject.Find ("Music Layer 2");
		//		musicLayer3 = GameObject.Find ("Music Layer 3");
		//		musicLayer4 = GameObject.Find ("Music Layer 4");
		
//		spiritAnim [0].GetComponent<Animator> ().enabled = false;
//		spiritAnim [1].GetComponent<Animator> ().enabled = false;
//		spiritAnim [2].GetComponent<Animator> ().enabled = false;
//		spiritAnim [3].GetComponent<Animator> ().enabled = false;
//		
//		spiritAnim [0].GetComponent<SpriteRenderer> ().enabled = false;
//		spiritAnim [1].GetComponent<SpriteRenderer> ().enabled = false;
//		spiritAnim [2].GetComponent<SpriteRenderer> ().enabled = false;
//		spiritAnim [3].GetComponent<SpriteRenderer> ().enabled = false;
		
		//wolf Den
		wolfDenArt = GameObject.Find ("WolfDen");
		
		//PlayerWolfGO = GameObject.Find("playerWolf");
	
		rb2DLostWolf = GetComponent<Rigidbody2D> ();
		wolfDenAnim = wolfDenArt.GetComponent<Animator> ();
		//LostWolfAnim = LostWolfGO.GetComponent<Animator> ();
		wolfDenAnim.SetInteger ("DenAnimState", 0);
		isTriggering = false;
		rescuedWolvesCounter = 0;
		//print ("Rescued Counter:" + rescuedWolvesCounter);
		
		//playerHealthTrack = PlayerWolfGO.GetComponent<PCWolfInput> ().playerHealth;
		
		
		//Vector3 randomPos = new Vector3(Random.Range(-10.0, 10.0), 0, Random.Range(-10.0, 10.0));
	}//end start
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void HealthIncrease()
	{
		if (PlayerWolfGO.GetComponent<PCWolfInput> ().playerHealth == 1|| PlayerWolfGO.GetComponent<PCWolfInput> ().playerHealth == 2){
			PlayerWolfGO.GetComponent<PCWolfInput> ().playerHealth += 1;

				if(OnHeal != null){
				//Sends to PCWolfInput script
					OnHeal();
			}
			//print ("Increase player health by 1");
			//print ("player health:" + PlayerWolfGO.GetComponent<PCWolfInput> ().playerHealth);
		}
	}
	
	IEnumerator GameEnd(){
		
		yield return new WaitForSeconds(5);
		Application.LoadLevel ("Howl Title Screen");
		//Destroy(this.gameObject);
	}

	void OnEnable()
	{
		WolfDen.WolfRescued += SpawnWolf;
	}
	
	
	void OnDisable()
	{
		WolfDen.WolfRescued -= SpawnWolf;
	}

	void SpawnWolf()
	{
		//print ("a wolf was rescued");
		if (rescuedWolvesCounter == 0){
	
			HealthIncrease();
			GameObject instance = Instantiate(Resources.Load("Lost Wolf Green")) as GameObject;
			instance.transform.position = lostWolfSpawnPos[0].transform.position;

			//sources[1].emissionRate = 300;
			//sources[1].transform.localPosition = leftSide.transform.localPosition;
			//sources[1].transform.localRotation = leftSide.transform.localRotation;
			
			//spiritAnim [0].GetComponent<Animator> ().enabled = true;
			//spiritAnim [0].GetComponent<SpriteRenderer> ().enabled = true;
			
			rescuedWolvesCounter = 1;
			//print ("Rescued Counter:" + rescuedWolvesCounter);
			
			//musicLayers [0].GetComponent<AudioSource> ().mute = false;
			//PlayerWolfGO.GetComponent<AudioSource> ().Play ();
		} else if(rescuedWolvesCounter == 1)
		{
			HealthIncrease();
			
			GameObject instance = Instantiate(Resources.Load("Lost Wolf Orange")) as GameObject;
			
			instance.transform.position = lostWolfSpawnPos[1].transform.position;
			//				instance.transform.position = spawnPoints[spawnPointIndex].transform.position;
			//				GetSpawnPoint();
			
//			sources[1].emissionRate = 500;
//			spiritAnim [1].GetComponent<Animator> ().enabled = true;
//			spiritAnim [1].GetComponent<SpriteRenderer> ().enabled = true;
			rescuedWolvesCounter = 2;
			
			musicLayers [1].GetComponent<AudioSource> ().mute = false;
			
		} else if(rescuedWolvesCounter == 2)
		{
			HealthIncrease();
			GameObject instance = Instantiate(Resources.Load("Lost Wolf L Blue")) as GameObject;
			
			instance.transform.position = lostWolfSpawnPos[2].transform.position;
			//				instance.transform.position = spawnPoints[spawnPointIndex].transform.position;
			//				GetSpawnPoint();
			
			//Destroy(target.gameObject);
			sources[1].emissionRate = 700;
			sources[1].transform.localPosition = rightSide.transform.localPosition;
			sources[1].transform.localRotation = rightSide.transform.localRotation;
			spiritAnim [2].GetComponent<Animator> ().enabled = true;
			spiritAnim [2].GetComponent<SpriteRenderer> ().enabled = true;
			rescuedWolvesCounter = 3;
			//musicLayer3.GetComponent<AudioSource> ().mute = false;
			musicLayers [2].GetComponent<AudioSource> ().mute = false;
		} 
		
	}//end spawnWolf
	
}//end whole class
