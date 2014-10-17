using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapScript : MonoBehaviour {
	
	public int MapSize;
	public GameObject TilePrefab;
	public GameObject FoodPrefab;

	private GameObject[,] MapTiles;
	private float MapOffset;

	// Use this for initialization
	void Start() {
		int unitSize = TilePrefab.GetComponent<TileScript>().UnitSize;
		MapOffset = -(MapSize/2*unitSize/Utilities.PIXELPERUNIT);

		GameMaster.instance.SetMapOffset(MapOffset);
		Debug.Log(GameMaster.instance.GetMapOffset());

		transform.position = new Vector3(MapOffset, MapOffset, 0);

		MapTiles = new GameObject[MapSize,MapSize];
		for (int i=0; i<MapSize; i++) {
			for (int j=0; j<MapSize; j++) {
				MapTiles[i, j] = Instantiate(TilePrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
				MapTiles[i, j].tag = "food";
				TileScript unit = MapTiles[i, j].GetComponent<TileScript>();
				unit.MapPos.x = i;
				unit.MapPos.y = j;
				unit.MapOffset = MapOffset;
				unit.UpdatePosition();
				MapTiles[i, j].SetActive(true);
			}
		}
	}
	
	// Update is called once per frame
	void Update() {
		
	}

	public void RandomFood() {

		List<Vector2> availableTiles = GameMaster.instance.GetAvailableTileIndex();
		int randIndex = Random.Range(0, availableTiles.Count);
		Vector2 randPos = availableTiles[randIndex];

		
		int randX = (int)randPos.x;
		int randY = (int)randPos.y;

		GameObject.Destroy(MapTiles[randX, randY]);
		MapTiles[randX, randY] = Instantiate(FoodPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
		TileScript unit = MapTiles[randX, randY].GetComponent<TileScript>();
		unit.MapPos.x = randX;
		unit.MapPos.y = randY;
		unit.MapOffset = MapOffset;
		unit.UpdatePosition();
		MapTiles[randX, randY].SetActive(true);

		GameMaster.instance.SetMapStatus(randX, randY, 1);
	}


}
