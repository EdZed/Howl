using UnityEngine;
using System.Collections;
using UnityEditor;

//Associates this class with a custom editor
//whenever we select any tileMap in scene, this custom editor will appear in inspector
[CustomEditor(typeof(TileMap))]
public class TileMapEditor : Editor {

	public TileMap map;

	TileBrush brush;
	Vector3 mouseHitPos;

	public override void OnInspectorGUI(){
		//Static method that creates a label field
		//EditorGUILayout.LabelField ("Our custom editor");
		EditorGUILayout.BeginVertical ();

		var oldSize = map.mapSize;

		//TileMap class will read these values
		//this will return new values from changes in inspector
		map.mapSize = EditorGUILayout.Vector2Field ("Map Size:", map.mapSize);
		//map.mapSize = EditorGUILayout.Vector2Field ("Map Size:", new Vector2());
		if (map.mapSize != oldSize) {
			UpdateCalculations();
		}

		map.texture2D = (Texture2D)EditorGUILayout.ObjectField ("Texture2D:", map.texture2D, typeof(Texture2D), false);
		map.tileGameObject = (GameObject)EditorGUILayout.ObjectField ("GameObject:", map.tileGameObject, typeof(GameObject), false);

		if (map.texture2D == null && map.tileGameObject == null) {
			EditorGUILayout.HelpBox ("You have not selected a texture 2D or game object yet.", MessageType.Warning);
		} else {
			EditorGUILayout.LabelField("Tile Size:", map.tileSize.x + "x" + map.tileSize.y);
			EditorGUILayout.LabelField("Grid Size In Units:", map.gridSize.x +"x"+ map.gridSize.y);
			EditorGUILayout.LabelField("Pixels To Units:", map.pixelsToUnits.ToString() );
			UpdateBrush(map.currentTileBrush);
		}

		EditorGUILayout.EndVertical ();
	}

	//Reference to currently selected item in inspector
	void OnEnable(){
		//value of map field to target abd recast as tilemap
		map = target as TileMap;
		Tools.current = Tool.View;

		if(map.texture2D != null){
			UpdateCalculations();
			NewBrush();
		}

		if (map.tileGameObject != null) {
			var path = AssetDatabase.GetAssetPath(map.tileGameObject);
			map.GOReferences = AssetDatabase.LoadAllAssetsAtPath(path);

			//array causes error because GO source does not have multiple images in one like the texture can.
			var gameObj = (GameObject)map.GOReferences[1];
			var width = gameObj.GetComponent<SpriteRenderer>().sprite.textureRect.width;
			var height = gameObj.GetComponent<SpriteRenderer>().sprite.textureRect.height;

			map.tileSize = new Vector2(width, height);
			map.pixelsToUnits = (int)(gameObj.GetComponent<SpriteRenderer>().sprite.rect.width / gameObj.GetComponent<SpriteRenderer>().sprite.bounds.size.x);
			map.gridSize = new Vector2((width/map.pixelsToUnits) *map.mapSize.x, (height/map.pixelsToUnits) * map.mapSize.y);

		}

	}

	void OnDisable(){
		DestroyBrush ();
	}

	void UpdateCalculations(){
			var path = AssetDatabase.GetAssetPath(map.texture2D);
			map.spriteReferences = AssetDatabase.LoadAllAssetsAtPath(path);

			var sprite = (Sprite)map.spriteReferences[1];
			var width = sprite.textureRect.width;
			var height = sprite.textureRect.height;

			map.tileSize = new Vector2(width, height);
			map.pixelsToUnits = (int)(sprite.rect.width / sprite.bounds.size.x);
			map.gridSize = new Vector2((width/map.pixelsToUnits) *map.mapSize.x, (height/map.pixelsToUnits) * map.mapSize.y);
	}

	void CreateBrush(){

		var sprite = map.currentTileBrush;
		if(sprite != null){
			GameObject go = new GameObject("Brush");
			go.transform.SetParent(map.transform);

			brush = go.AddComponent<TileBrush>();
			brush.renderer2D = go.AddComponent<SpriteRenderer>();

			var pixelsToUnits = map.pixelsToUnits;
			brush.brushSize = new Vector2(sprite.textureRect.width / pixelsToUnits,
			                              sprite.textureRect.height / pixelsToUnits);
			brush.UpdateBrush(sprite);
		}
	}

	void NewBrush(){
		if (brush == null) {
			CreateBrush();
		}
	}

	void DestroyBrush(){
		if (brush != null) {
			DestroyImmediate(brush.gameObject);
		}
	}

	public void UpdateBrush(Sprite sprite){
		if(brush != null){
			brush.UpdateBrush (sprite);
		}
	}

	void UpdateHitPosition(){

		var p = new Plane (map.transform.TransformDirection (Vector3.forward), Vector3.zero);
		var ray = HandleUtility.GUIPointToWorldRay (Event.current.mousePosition);
		var hit = Vector3.zero;
		var dist = 0f;

		if(p.Raycast(ray, out dist)){
			hit = ray.origin + ray.direction.normalized * dist;
		}

		mouseHitPos = map.transform.InverseTransformPoint (hit);

	}

	

} //end of class script






