using UnityEngine;
using System.Collections;

public class UnitTile : MonoBehaviour {

	public int UnitSize;

	public Vector2 MapPos;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void UpdatePosition()
	{
		Vector3 pos = new Vector3();
		pos.x = UnitSize*MapPos.x / Utilities.PIXELPERUNIT;
		pos.y = UnitSize*MapPos.y / Utilities.PIXELPERUNIT;
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

			//col.gameObject.GetComponent();
		}
	}
}
