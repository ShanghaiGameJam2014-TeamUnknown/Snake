using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapScript : MonoBehaviour {
	public GameObject FoodPrefab;
	public List<GameObject> FoodPrefabs;

	private List<GameObject> FoodTiles;

	void Awake() {
		FoodTiles = new List<GameObject>();
	}
	
	public List<GameObject> GetFoodTiles() {
		return FoodTiles;
	}

	public void RandomFood(List<Vector2> availableTiles) {
		foreach (GameObject ft in FoodTiles) {
			GameObject.Destroy(ft);
		}
		FoodTiles.Clear();

		for(int i=0; i<2; i++) {
			int randIndex = Random.Range(0, availableTiles.Count);
			Vector2 randPos = availableTiles[randIndex];
			availableTiles.RemoveAt(randIndex);
			int randX = (int)randPos.x;
			int randY = (int)randPos.y;

			int randPrefabIndex = Random.Range(0, FoodPrefabs.Count);
			GameObject randPrefab = FoodPrefabs[randPrefabIndex];

			GameObject food = Instantiate(FoodPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
			food.tag = "food";
			TileScript unit = food.GetComponent<TileScript>();
			unit.MapPos.x = randX;
			unit.MapPos.y = randY;
			unit.MapOffsetX = GameMaster.instance.GetMapOffsetX();
			unit.MapOffsetY = GameMaster.instance.GetMapOffsetY();
			unit.UpdatePosition();
			food.SetActive(true);
			food.GetComponent<Animator>().SetTrigger("create");
			FoodTiles.Add(food);
		}
	}
}
