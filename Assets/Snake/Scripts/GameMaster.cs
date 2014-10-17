using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

	public GameObject GameMap;
	public GameObject PlayerSnake;

	public int MapSize;
	public GameObject TilePrefab;

	private MapScript mapLogic;
	private float mapOffset;
	private float passedTime;

	private int[,] mapStatus;
	
	private static GameMaster _instance;
	public static GameMaster instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<GameMaster>();
			}
			return _instance;
		}
	}


	// Use this for initialization
	void Start () {
		passedTime = 3;
		mapStatus = new int[MapSize,MapSize];

		mapLogic = GameMap.GetComponent<MapScript>();
		ClearMapStatus();

		mapLogic.RandomFood(GetAvailableTileIndex());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// generate foods
		passedTime += Time.deltaTime;
		if (passedTime > 2) {
			mapLogic.RandomFood(GetAvailableTileIndex());
			passedTime = 0;
		}

		// change directions
	}

	public GameObject GetTile(int x, int y) {
		List<GameObject> foodTiles = mapLogic.GetFoodTiles();
		foreach (GameObject ft in foodTiles) {
			Vector2 pos = ft.GetComponent<TileScript>().MapPos;
			if ((pos.x == x) && (pos.y == y)) {
				return ft;
			}
		}

		List<GameObject> bodyTiles = PlayerSnake.GetComponent<Snake>().Body;
		foreach (GameObject bt in bodyTiles) {
			Vector2 pos = bt.GetComponent<TileScript>().MapPos;
			if ((pos.x == x) && (pos.y == y)) {
				return bt;
			}
		}
		return null;
	}

	void ClearMapStatus()
	{
		for(int i=0;i<MapSize;i++) {
			for(int j=0; j<MapSize; j++) {
				mapStatus[i,j] = (int)TileType.EMPTY;
			}
		}
	}

	public List<Vector2> GetAvailableTileIndex()
	{
		ClearMapStatus();

		// iterate snakeTiles, set mapStatus
		List<GameObject> snakeTiles = PlayerSnake.GetComponent<Snake>().Body;

		for(int i=0; i<snakeTiles.Count; i++)
		{
			TileScript tile = snakeTiles[i].GetComponent<TileScript>();
			mapStatus[(int)tile.MapPos.x, (int)tile.MapPos.y] = (int)TileType.SNAKE;
		}

		List<Vector2> availableTileIndex = new List<Vector2>();
		for (int i=0;i<MapSize;i++) {
			for (int j=0; j<MapSize; j++) {
				if (mapStatus[i,j] == 0) {
					availableTileIndex.Add(new Vector2(i, j));
				}
			}
		}

		return availableTileIndex;
	}

	public float GetMapOffset()
	{
		int unitSize = TilePrefab.GetComponent<TileScript>().UnitSize;
		return -(MapSize/2*unitSize/Utilities.PIXELPERUNIT);
	}
}
