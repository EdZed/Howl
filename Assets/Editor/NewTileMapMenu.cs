using UnityEngine;
using System.Collections;
using UnityEditor;
//Unity editor added to allow use within editor

//monobehaviour removed since script won't be applied to a Gameobject
public class NewTileMapMenu {
	//meta tag that connects this method to a menu item. Just like a method.
	//pass in path where we want menu to show up
	[MenuItem("GameObject/Tile Map")]
	public static void CreateTileMap(){
		//Debug.Log ("Create new tile map");
		GameObject go = new GameObject ("Tile Map");
		go.AddComponent<TileMap> ();
	}

}
