using UnityEngine;
using System.Collections;

public class SpriteZOrder : MonoBehaviour {

	public bool IsStatic;
	public float AnchorOffset;
	
	private SpriteRenderer Sprite;

	// Use this for initialization
	void Start () {
		Sprite = GetComponent<SpriteRenderer>();
		AssignSortOrder();
	}
	
	// Update is called once per frame
	void Update () {
		if (!IsStatic)
		{
			AssignSortOrder();
		}
	}

	private void AssignSortOrder() 
	{
		//number at end decides that amount of "lanes" that are made on the y axis. Each lane
		//is a previous or next z-order. The lower the number, the more lanes. Higher number is,
		// the less amount of lanes there are. Something around .1 or something a bit higher will
		//work for me.
		Sprite.sortingOrder = -Mathf.RoundToInt((transform.position.y + AnchorOffset) / 0.1f);
	}
}
