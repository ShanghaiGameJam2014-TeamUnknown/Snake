using UnityEngine;
using System.Collections;

public enum TileType
{
	EMPTY = 0,
	SNAKE,
	FOOD1,
	FOOD2
}

public class TileScript : MonoBehaviour {

	public TileType TileType;
	public int UnitSize;
	public float MapOffsetX;
	public float MapOffsetY;

	public Vector2 MapPos;
	public string Anim;

	// Use this for initialization
	void Start ()
	{
		Anim = "create";
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void UpdatePosition()
	{
		Vector3 pos = new Vector3();
		pos.x = UnitSize*MapPos.x / Utilities.PIXELPERUNIT + MapOffsetX;
		pos.y = UnitSize*MapPos.y / Utilities.PIXELPERUNIT + MapOffsetY;
		pos.z = 0;

		transform.position = pos;
	}

	public void MoveUp()
	{
		MapPos.y +=1;
		UpdatePosition();
	}

	public void MoveDown()
	{	
		MapPos.y -=1;
		UpdatePosition();
	}

	public void MoveLeft()
	{	
		MapPos.x -=1;
		UpdatePosition();
	}

	public void MoveRight()
	{
		MapPos.x +=1;
		UpdatePosition();
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Player")
		{
			Debug.Log("Player hit");
			//Snake snake = col.gameObject.GetComponent<Snake>();
		}
	}
}
