using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snake : MonoBehaviour {
	public List<GameObject> Body;
	public int InitNumber;

	public int Speed;

	public Vector2 CurrentDirection;

	private Vector2 PreviousDirection;

	public GameObject FoodPrefab;
	
	private int FramCount;
	private bool IsDead;

	void Start () {
		InitSnake();
	}

	void InitSnake() {
		for(int i=0; i<InitNumber; i++) {
			GameObject bodyItem = Instantiate(FoodPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;

			bodyItem.tag = "snake";
			
			TileScript unit = bodyItem.GetComponent<TileScript>();
			unit.MapPos.x = i;
			unit.MapPos.y = 0;
			unit.MapOffsetX = GameMaster.instance.GetMapOffsetX();
			unit.MapOffsetY = GameMaster.instance.GetMapOffsetY();
			unit.UpdatePosition();
			bodyItem.SetActive(true);

			Body.Add(bodyItem);
			
		}
	}

	void FixedUpdate() {
		if(FramCount < Speed) {
			FramCount++;
		}
		else {
			TileScript head = Body[Body.Count - 1].gameObject.GetComponent<TileScript>();
			Vector2 newPos = head.MapPos + CurrentDirection;
			if ((newPos.x<0) || (newPos.x>=GameMaster.instance.MapSizeX)) {
				Die();
			}
			else if ((newPos.y<0) || (newPos.y >=GameMaster.instance.MapSizeY)) {
				Die();
			}
			else {
				// Ask GM whether the newPos has food or is snake
				GameObject toEat = GameMaster.instance.GetTile((int)newPos.x, (int)newPos.y);
				if (toEat!=null && toEat.tag == "food")
				{
					// Body.Add();
					GameObject newHead = Instantiate(toEat, new Vector3(0,0,0), Quaternion.identity) as GameObject;
					newHead.tag = "snake";
					TileScript unit = newHead.GetComponent<TileScript>();
					unit.MapPos.x = (int)newPos.x;
					unit.MapPos.y = (int)newPos.y;
					unit.MapOffsetX = GameMaster.instance.GetMapOffsetX();
					unit.MapOffsetY = GameMaster.instance.GetMapOffsetY();
					unit.UpdatePosition();
					newHead.SetActive(true);
					toEat.SetActive(false);
					AddHead(newHead);

					//newHead.GetComponent<TileScript>().Anim = Body[FramCount-1]
					UpdateSnakeAnimation(CurrentDirection, newHead);
					UpdateSnakeAnimation(CurrentDirection, Body[Body.Count-2]);
				}
				else if (toEat!=null && toEat.tag == "snake") {
					// Death;
					Die();
				}
				else
				{
					UpdateSnakePosition(newPos);
				}
			}
			FramCount=0;
		}

		if(!IsDead) {
			InputX();
		}
	}

	void Die() {
		for(int i=0; i<Body.Count; i++) {
			Body[i].GetComponent<TileScript>().Anim = "dead";
			Body[i].GetComponent<Animator>().SetTrigger("dead");
		}

		IsDead = true;
		GameMaster.instance.GameOver();
	}

	bool GetIsDead() {
		return IsDead;
	}

	void InputX() {
		TileScript head = Body[Body.Count - 1].gameObject.GetComponent<TileScript>();
		
		Vector2 pendingDirection = CurrentDirection;
		
		if(Input.GetKey(KeyCode.A)) {
			pendingDirection = Utilities.LEFT;
		}
		else if(Input.GetKey(KeyCode.D)) {	
			pendingDirection = Utilities.RIGHT;
		}
		else if(Input.GetKey(KeyCode.W)) {	
			pendingDirection = Utilities.UP;
		}
		else if(Input.GetKey(KeyCode.S)) {
			pendingDirection = Utilities.DOWN;
		}
		Vector2 newPos = head.MapPos + pendingDirection;
		
		GameObject tile = GameMaster.instance.GetTile((int)newPos.x, (int)newPos.y);
		if (tile==null || tile.tag != "snake") {
			if(CurrentDirection!=pendingDirection) {
				CurrentDirection = pendingDirection;
			}
		}
	}

	void UpdateSnakeAnimation(Vector2 direction, GameObject tile) {
		string anim="create";
		if (direction == Utilities.LEFT) {
			anim = "walk_left";
		}
		else if (direction == Utilities.RIGHT) {
			anim = "walk_right";
		}
		else if (direction==Utilities.UP) {
			anim = "walk_back";
		}
		else if (direction==Utilities.DOWN) {
			anim = "walk";
		}

		if(tile.GetComponent<TileScript>().Anim !=anim) {
			tile.GetComponent<TileScript>().Anim = anim;
			tile.GetComponent<Animator>().SetTrigger(anim);
		}
	}

	void UpdateSnakePosition(Vector2 newHeadPosition) {
		Vector2 prePos = newHeadPosition;
		for(int i=Body.Count - 1; i>=0; i--) {
			TileScript tile = Body[i].gameObject.GetComponent<TileScript>();
			Vector2 temp = tile.MapPos;
			tile.MapPos = prePos;
			
			UpdateSnakeAnimation(prePos - temp, Body[i]);
			tile.UpdatePosition();

			prePos = temp;
		}
	}

	public void AddHead(GameObject newHead) {
		Body.Add(newHead);
	}
}
