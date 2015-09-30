using UnityEngine;
using System.Collections;

public class WolfParticleToDen : MonoBehaviour {
	ParticleSystem particleToDen;

	ParticleEmitter emitter;
	GameObject Den;
	Vector2 DenPoint;

	// Use this for initialization
	void Start () {
		emitter = GetComponent<ParticleEmitter> ();
		particleToDen = GetComponent<ParticleSystem> ();
		Den = GameObject.Find("WolfDen");
		DenPoint = Den.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//emitter.Emit(Vector3.zero, DenPoint, 0.2, 2, Color.yellow);
		//particleToDen.
	}
}
