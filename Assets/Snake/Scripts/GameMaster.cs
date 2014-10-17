using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

	public GameObject GameMap;
	public GameObject PlayerSnake;

	private float mapOffset;
	private float passedTime;
	private MapScript mapLogic;

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
		mapLogic = GameMap.GetComponent<MapScript>();
		InitMapStatus();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// generate foods
		passedTime += Time.deltaTime;
		if (passedTime > 2) {
			mapLogic.RandomFood();
			passedTime = 0;
		}

		// change directions
	}

	public GameObject GetTile(int x, int y) {
		return null;
	}

	void InitMapStatus()
	{
		mapStatus = new int[mapLogic.MapSize,mapLogic.MapSize];

		for(int i=0;i<mapLogic.MapSize;i++)
		{
			for(int j=0; j<mapLogic.MapSize; j++)
			{
				mapStatus[i,j] = 0;
			}
		}
	}

	public void SetMapStatus(int i, int j, int type)
	{
		mapStatus[i,j] = type;
	}

	public List<Vector2> GetAvailableTileIndex()
	{
		List<Vector2> availableTileIndex = new List<Vector2>();
		for(int i=0;i<mapLogic.MapSize;i++)
		{
			for(int j=0; j<mapLogic.MapSize; j++)
			{
				if(mapStatus[i,j] == 0)
				{
					availableTileIndex.Add(new Vector2(i, j));
				}
			}
		}

		return availableTileIndex;
	}

	public void SetMapOffset(float offset)
	{
		mapOffset = offset;
	}

	public float GetMapOffset()
	{
		return mapOffset;
	}
}
