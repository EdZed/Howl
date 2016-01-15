using UnityEngine;
using System.Collections;

public class GameObjectStay : MonoBehaviour {


	void Awake () {
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
//	void Update () {
//	
//	}
}
