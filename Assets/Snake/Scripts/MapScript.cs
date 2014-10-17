using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapScript : MonoBehaviour {
	public GameObject TilePrefab;
	public GameObject FoodPrefab;

	private List<GameObject> FoodTiles;
	private GameObject[,] MapTiles;
	private int MapSize;

	// Use this for initialization
	void Start() {
		transform.position = new Vector3(GameMaster.instance.GetMapOffset(), GameMaster.instance.GetMapOffset(), 0);
		MapSize = GameMaster.instance.MapSize;

		MapTiles = new GameObject[MapSize,MapSize];
		for (int i=0; i<MapSize; i++) {
			for (int j=0; j<MapSize; j++) {
				MapTiles[i, j] = Instantiate(TilePrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
				MapTiles[i, j].tag = "food";
				TileScript unit = MapTiles[i, j].GetComponent<TileScript>();
				unit.MapPos.x = i;
				unit.MapPos.y = j;
				unit.MapOffset = GameMaster.instance.GetMapOffset();
				unit.UpdatePosition();
				MapTiles[i, j].SetActive(true);
			}
		}
	}
	
	public List<GameObject> GetFoodTiles() {
		return FoodTiles;
	}

	public void RandomFood(List<Vector2> availableTiles) {
		int randIndex = Random.Range(0, availableTiles.Count);
		Vector2 randPos = availableTiles[randIndex];
		int randX = (int)randPos.x;
		int randY = (int)randPos.y;

		GameObject.Destroy(MapTiles[randX, randY]);
		MapTiles[randX, randY] = Instantiate(FoodPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
		TileScript unit = MapTiles[randX, randY].GetComponent<TileScript>();
		unit.MapPos.x = randX;
		unit.MapPos.y = randY;
		unit.MapOffset = GameMaster.instance.GetMapOffset();
		unit.UpdatePosition();
		MapTiles[randX, randY].SetActive(true);
	}


}
