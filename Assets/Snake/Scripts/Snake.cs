using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snake : MonoBehaviour {
	public List<GameObject> Body;
	public int InitNumber;

	public int Speed;

	public Vector2 CurrentDirection;

	public GameObject FoodPrefab;
	
	private int FramCount;
	// Use this for initialization
	void Start ()
	{
		InitSnake();
	}

	void InitSnake()
	{
		for(int i=0; i<InitNumber; i++)
		{
			GameObject bodyItem = Instantiate(FoodPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;

			bodyItem.tag = "snake";
			
			TileScript unit = bodyItem.GetComponent<TileScript>();
			unit.MapPos.x = i;
			unit.MapPos.y = 0;
			unit.MapOffset = GameMaster.instance.GetMapOffset();
			unit.UpdatePosition();
			bodyItem.SetActive(true);

			Body.Add(bodyItem);
			
		}
		CurrentDirection = Utilities.RIGHT;
	}

	void FixedUpdate()
	{
		if(FramCount < Speed)
		{
			FramCount++;
		}
		else
		{
			TileScript head = Body[Body.Count - 1].gameObject.GetComponent<TileScript>();
			Vector2 newPos = head.MapPos + CurrentDirection;
			//head.MapPos = newPos;
			//head.UpdatePosition();

			UpdateSnakePosition(newPos);
			// Ask GM whether the newPos has food or is snake
			//GameObject toEat = GameMaster.instance.GetTile((int)newPos.x, (int)newPos.y);
			//if (toEat.tag == "food")
			//{
				// Body.Add();
				// AddHead();
			//}
			//else if (toEat.tag == "snake") {
				// Death;
			//}
			FramCount=0;
		}
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void UpdateSnakePosition(Vector2 newHeadPosition)
	{
		Vector2 prePos = newHeadPosition;
		for(int i=Body.Count - 1; i>=0; i--)
		{
			TileScript tile = Body[i].gameObject.GetComponent<TileScript>();
			Vector2 temp = tile.MapPos;
			tile.MapPos = prePos;
			tile.UpdatePosition();

			prePos = temp;
		}
	}

	public void AddHead(GameObject newHead)
	{
		Body.Add(newHead);
	}
}
