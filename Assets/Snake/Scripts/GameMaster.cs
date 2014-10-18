﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

	public GameObject GameMap;
	public GameObject PlayerSnake;

	public int MapSizeX;
	public int MapSizeY;
	public GameObject TilePrefab;

	public float regenTime;

	private MapScript mapLogic;
	private float mapOffsetX;
	private float mapOffsetY;
	private float passedTime;

	private int[,] mapStatus;

	private bool IsGameOver;
	private static GameMaster _instance;
	public static GameMaster instance {
		get {
			if(_instance == null) {
				_instance = GameObject.FindObjectOfType<GameMaster>();
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}
	void Awake() {
		if (_instance == null) {
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else {
			if (this != _instance) {
				Destroy(this.gameObject);
			}
		}
	}

	// Use this for initialization
	void Start () {
		passedTime = 3;
		mapStatus = new int[MapSizeX,MapSizeY];

		mapLogic = GameMap.GetComponent<MapScript>();
		ClearMapStatus();

		mapLogic.RandomFood(GetAvailableTileIndex());
	}
	
	void FixedUpdate () {
		if(IsGameOver) {
			return;
		}
		// generate foods
		passedTime += Time.deltaTime;
		if (passedTime >= regenTime) {
			RegenFoods();
		}

		// change directions
		if (Input.GetKey(KeyCode.O)) {
			Application.LoadLevel("Boss");
			PlayerSnake.GetComponent<Snake>().Die();
		}
	}

	public void RegenFoods() {
		mapLogic.RandomFood(GetAvailableTileIndex());
		passedTime = 0;
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

	void ClearMapStatus() {
		for(int i=0;i<MapSizeX;i++) {
			for(int j=0; j<MapSizeY; j++) {
				mapStatus[i,j] = (int)TileType.EMPTY;
			}
		}
	}

	public List<Vector2> GetAvailableTileIndex() {
		ClearMapStatus();

		// iterate snakeTiles, set mapStatus
		List<GameObject> snakeTiles = PlayerSnake.GetComponent<Snake>().Body;

		for(int i=0; i<snakeTiles.Count; i++)
		{
			TileScript tile = snakeTiles[i].GetComponent<TileScript>();
			mapStatus[(int)tile.MapPos.x, (int)tile.MapPos.y] = (int)TileType.SNAKE;
		}

		List<Vector2> availableTileIndex = new List<Vector2>();
		for (int i=0;i<MapSizeX;i++) {
			for (int j=0; j<MapSizeY; j++) {
				if (mapStatus[i,j] == 0) {
					availableTileIndex.Add(new Vector2(i, j));
				}
			}
		}

		return availableTileIndex;
	}

	public float GetMapOffsetX() {
		int unitSize = TilePrefab.GetComponent<TileScript>().UnitSize;
		return -(MapSizeX/2*unitSize/Utilities.PIXELPERUNIT);
	}

	public float GetMapOffsetY() {
		int unitSize = TilePrefab.GetComponent<TileScript>().UnitSize;
		return -(MapSizeY/2*unitSize/Utilities.PIXELPERUNIT);
	}

	public void GameOver() {
		IsGameOver = true;
	}
}
