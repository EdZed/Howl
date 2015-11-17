using UnityEngine;
using System.Collections;

public class RockPiece : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private Color start;
	private Color end;
	private float t = 0.0f;
	private Renderer rendMaterialColor;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		start = spriteRenderer.color;
		end = new Color (start.r, start.g, start.b, 0.0f);
		rendMaterialColor = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;

		//GetComponent<Renderer>().material.color = Color.Lerp (start, end, t / 2);
		rendMaterialColor.material.color= Color.Lerp (start, end, t / 2);
		if (rendMaterialColor.material.color.a <= 0.0) {
			Destroy (gameObject);
		}
	}
}
