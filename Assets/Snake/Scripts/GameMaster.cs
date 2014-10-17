using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

	public GameObject GameMap;
	public GameObject PlayerSnake;

	public int MapSize;
	public GameObject TilePrefab;

	private float mapOffset;
	private float passedTime;

	private int[,] mapStatus;

	//Here is a private reference only this class can access
	private static GameMaster _instance;
	
	//This is the public reference that other classes will use
	public static GameMaster instance
	{
		get
		{
			//If _instance hasn't been set yet, we grab it from the scene!
			//This will only happen the first time this reference is used.
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<GameMaster>();
			}
			return _instance;
		}
	}


	// Use this for initialization
	void Start () {
		passedTime = 0;
		mapStatus = new int[MapSize,MapSize];
		ClearMapStatus();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// generate foods
		passedTime += Time.deltaTime;
		if (passedTime > 2) {
			//mapLogic.RandomFood(GetAvailableTileIndex());
			passedTime = 0;
		}

		// change directions
	}

	public GameObject GetTile(int x, int y) {
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
