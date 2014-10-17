using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapScript : MonoBehaviour {
	public GameObject TilePrefab;
	public GameObject FoodPrefab;

	private List<GameObject> FoodTiles;

	void Awake()
	{
		FoodTiles = new List<GameObject>();
	}
	// Use this for initialization
	void Start() {
		transform.position = new Vector3(GameMaster.instance.GetMapOffset(), GameMaster.instance.GetMapOffset(), 0);
	}
	
	public List<GameObject> GetFoodTiles() {
		return FoodTiles;
	}

	public void RandomFood(List<Vector2> availableTiles) {

		for(int i=0; i<FoodTiles.Count; i++)
		{
			GameObject.Destroy(FoodTiles[i]);
		}

		FoodTiles.Clear();

		for(int i=0; i<2; i++)
		{
			int randIndex = Random.Range(0, availableTiles.Count);
			Vector2 randPos = availableTiles[randIndex];
			availableTiles.RemoveAt(randIndex);
			int randX = (int)randPos.x;
			int randY = (int)randPos.y;

			GameObject food = Instantiate(FoodPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;

			TileScript unit = food.GetComponent<TileScript>();
			unit.MapPos.x = randX;
			unit.MapPos.y = randY;
			unit.MapOffset = GameMaster.instance.GetMapOffset();
			unit.UpdatePosition();
			food.SetActive(true);

			FoodTiles.Add(food);
		}

	}


}
