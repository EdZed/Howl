using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WolfDenSpiritMusic : MonoBehaviour {
	
	public BoxCollider2D LostWolfCollider;
	public GameObject LostWolfGO;
	public Rigidbody2D rb2DLostWolf;
	public GameObject PlayerWolfGO;
	//public SpriteRenderer PlayerWolfRenderer;
	//Lost wolves spawn points
	public GameObject lostWolf2Pos;
	public GameObject lostWolf3Pos;
	public GameObject lostWolf4Pos;
	public GameObject lostWolf5Pos;

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
	float speed;
	float moveSpeed = 4f;
	
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

	//define a list
	public static List<GameObject> spawnPoints = new List<GameObject>();
	int spawnPointIndex;

	public delegate IEnumerator LastWolfCollected();
	public static event LastWolfCollected RedWolfCollected;
	
	// Use this for initialization
	void Start () {

		LostWolfGO = this.gameObject;
		lostWolf2Pos = GameObject.Find ("LostWolf2Pos");
		lostWolf3Pos = GameObject.Find ("LostWolf3Pos");
		lostWolf4Pos = GameObject.Find ("LostWolf4Pos");
		lostWolf5Pos = GameObject.Find ("LostWolf5Pos");
		//lostWolfSpawnPointsGO = GameObject.Find ("LostWolfSpawnPoints");

		//List of spawnPoints added
		spawnPoints.Add (lostWolf2Pos);
		spawnPoints.Add (lostWolf3Pos);
		spawnPoints.Add (lostWolf4Pos);
		spawnPoints.Add (lostWolf5Pos);
		print ("Starting spawn points:" + spawnPoints.Count);
		//spawnPointIndex = UnityEngine.Random.Range (0, spawnPoints.Count);
		spawnPointIndex = Random.Range (0, spawnPoints.Count);
		//print ("Starting spawn point index:" + spawnPoint);
		print (spawnPoints [spawnPointIndex]);

		//Wolf Den Music Layers
//		musicLayer1 = GameObject.Find ("Music Layer 1");
//		musicLayer2 = GameObject.Find ("Music Layer 2");
//		musicLayer3 = GameObject.Find ("Music Layer 3");
//		musicLayer4 = GameObject.Find ("Music Layer 4");

		spiritAnim [0].GetComponent<Animator> ().enabled = false;
		spiritAnim [1].GetComponent<Animator> ().enabled = false;
		spiritAnim [2].GetComponent<Animator> ().enabled = false;
		spiritAnim [3].GetComponent<Animator> ().enabled = false;

		spiritAnim [0].GetComponent<SpriteRenderer> ().enabled = false;
		spiritAnim [1].GetComponent<SpriteRenderer> ().enabled = false;
		spiritAnim [2].GetComponent<SpriteRenderer> ().enabled = false;
		spiritAnim [3].GetComponent<SpriteRenderer> ().enabled = false;

		//wolf Den
		wolfDenArt = GameObject.Find ("WolfDen");

		//enemyAttackCollider = enemyAttackGO.GetComponent <BoxCollider2D>();
		//LostWolfCollider = LostWolfGO.GetComponent <BoxCollider2D> ();
		LostWolfCollider = GetComponent <BoxCollider2D> ();
		//LostWolfCollider.enabled = false;
		
		PlayerWolfGO = GameObject.Find("playerWolf");
		//PlayerWolfRenderer = PlayerWolfGO.GetComponent <SpriteRenderer> ();
		
		speed = moveSpeed;
		rb2DLostWolf = GetComponent<Rigidbody2D> ();
		wolfDenAnim = wolfDenArt.GetComponent<Animator> ();
		//LostWolfAnim = LostWolfGO.GetComponent<Animator> ();
		wolfDenAnim.SetInteger ("DenAnimState", 0);
		isTriggering = false;
		rescuedWolvesCounter = 0;
		
		//Vector3 randomPos = new Vector3(Random.Range(-10.0, 10.0), 0, Random.Range(-10.0, 10.0));
	}//end start
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D target)
	{
		isTriggering = true;
 
		if (target.gameObject.tag == "LostWolf") 
		{
			if (rescuedWolvesCounter == 0){
			//PlayerWolfGO.GetComponent<AudioSource> ().Play ();
			
			// Instantiates a prefab located in any Resources
			// folder in your project's Assets folder.
			//GameObject instance = Instantiate(Resources.Load("Lost Wolf", typeof(GameObject))) as GameObject;	
				//LostWolfAnim.SetInteger ("LostWolfAnimState", 5);

				GameObject instance = Instantiate(Resources.Load("Lost Wolf Orange")) as GameObject;
		
				//instance.transform.position = lostWolf2Pos.transform.position;
				instance.transform.position = spawnPoints[spawnPointIndex].transform.position;
				GetSpawnPoint();
				//spawnPoints.Remove(
				//spawnPoints.Remove.r


				//instance.transform.position = new Vector3(Random.Range(-10.0, 10.0), 0, Random.Range(-10.0, 10.0));
			
				sources[1].emissionRate = 120;
				sources[1].transform.localPosition = leftSide.transform.localPosition;
				sources[1].transform.localRotation = leftSide.transform.localRotation;

				spiritAnim [0].GetComponent<Animator> ().enabled = true;
				spiritAnim [0].GetComponent<SpriteRenderer> ().enabled = true;
				//anim[0].SetInteger ("AnimState", 1);
				rescuedWolvesCounter = 1;
				//musicLayer1.GetComponent<AudioSource> ().mute = false;
				musicLayers [0].GetComponent<AudioSource> ().mute = false;
				//PlayerWolfGO.GetComponent<AudioSource> ().Play ();
			} else if(rescuedWolvesCounter == 1)
			{
				GameObject instance = Instantiate(Resources.Load("Lost Wolf Purple")) as GameObject;

				//instance.transform.position = lostWolf3Pos.transform.position;
				instance.transform.position = spawnPoints[spawnPointIndex].transform.position;
				GetSpawnPoint();

				sources[1].emissionRate = 500;
				spiritAnim [1].GetComponent<Animator> ().enabled = true;
				spiritAnim [1].GetComponent<SpriteRenderer> ().enabled = true;
				rescuedWolvesCounter = 2;

				musicLayers [1].GetComponent<AudioSource> ().mute = false;

			} else if(rescuedWolvesCounter == 2)
			{

				GameObject instance = Instantiate(Resources.Load("Lost Wolf L Blue")) as GameObject;

				//instance.transform.position = lostWolf4Pos.transform.position;
				instance.transform.position = spawnPoints[spawnPointIndex].transform.position;
				GetSpawnPoint();
				//Destroy(target.gameObject);
				sources[1].emissionRate = 700;
				sources[1].transform.localPosition = rightSide.transform.localPosition;
				sources[1].transform.localRotation = rightSide.transform.localRotation;
				spiritAnim [2].GetComponent<Animator> ().enabled = true;
				spiritAnim [2].GetComponent<SpriteRenderer> ().enabled = true;
				rescuedWolvesCounter = 3;
				//musicLayer3.GetComponent<AudioSource> ().mute = false;
				musicLayers [2].GetComponent<AudioSource> ().mute = false;
			} else if(rescuedWolvesCounter == 3)
			{
				GameObject instance = Instantiate(Resources.Load("Lost Wolf Red")) as GameObject;

				//instance.transform.position = lostWolf5Pos.transform.position;

				instance.transform.position = spawnPoints[spawnPointIndex].transform.position;
				GetSpawnPoint();
				//Destroy(target.gameObject);
				sources[1].emissionRate = 900;
				sources[1].transform.localPosition = leftSide.transform.localPosition;
				sources[1].transform.localRotation = leftSide.transform.localRotation;
				spiritAnim [3].GetComponent<Animator> ().enabled = true;
				spiritAnim [3].GetComponent<SpriteRenderer> ().enabled = true;
				rescuedWolvesCounter = 4;
				//musicLayer4.GetComponent<AudioSource> ().mute = false;
				musicLayers [3].GetComponent<AudioSource> ().mute = false;
			} else if(rescuedWolvesCounter == 4)
			{
				sources[1].emissionRate = 1000;
				rescuedWolvesCounter = 5;
				if(RedWolfCollected != null)
				{
					RedWolfCollected();
				}
				//PlayerWolfRenderer.enabled = false;
				//Application.LoadLevel ("Howl Title Screen");
				//GameEnd();
			}
		}//end target tag LostWolf
	}//end on trigger enter

	void GetSpawnPoint()
	{
		if (spawnPoints.Count == 1) 
		{

		} else {
			//instance.transform.position = spawnPoints[spawnPointIndex].transform.position;
			spawnPoints.RemoveAt (spawnPointIndex);
			spawnPointIndex = Random.Range (0, spawnPoints.Count - 1);
			print (spawnPoints [spawnPointIndex]);
			print ("There are " + spawnPoints.Count + " spawn points left.");
			//print ("Spawn point index left:" + spawnPointIndex);
		}
	}

}//end whole class
