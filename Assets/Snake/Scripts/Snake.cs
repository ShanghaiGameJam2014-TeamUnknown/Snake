using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snake : MonoBehaviour {
	public List<GameObject> Body;
	public int InitNumber;

	public int Speed;

	public Vector2 CurrentDirection;
	
	private int FramCount;
	// Use this for initialization
	void Start ()
	{
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
			
			// Ask GM whether the newPos has food or is snake
			GameObject toEat = GameMaster.instance.GetTile((int)newPos.x, (int)newPos.y);
			if (toEat.tag == "food")
			{
				// Body.Add();
				// AddHead();
			}
			else if (toEat.tag == "snake") {
				// Death;
			}
			FramCount=0;
		}
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void AddHead(GameObject newHead)
	{
		Body.Add(newHead);
	}
}
