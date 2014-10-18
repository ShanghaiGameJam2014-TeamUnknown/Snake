using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snake : MonoBehaviour {
	public List<GameObject> Body;
	public int InitNumber;
	
	public int SlowestFrameCount;
	public int FastestFrameCount;
	public float SpeedMultiplier;

	public Vector2 CurrentDirection;

	public GameObject LeadMarkPrefab;

	private GameObject LeadMark;
	private Vector2 PreviousDirection;

	public GameObject InitPrefab;

	private int Speed;
	private int FramCount;
	private bool IsDead;

	public AudioSource foodSound;
	public AudioSource wallSound;
	public AudioSource snakeSound;

	void Start () {
		Speed = SlowestFrameCount;
		InitSnake();
	}

	void InitSnake() {
		for(int i=0; i<InitNumber; i++) {
			GameObject bodyItem = Instantiate(InitPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;

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

		LeadMark = Instantiate(LeadMarkPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
		LeadMark.transform.position = Body[Body.Count-1].transform.position;
		LeadMark.transform.parent = Body[Body.Count-1].transform;
	}

	void FixedUpdate() {
		if (IsDead) {
			return;
		}
		if(FramCount < Speed) {
			FramCount++;
		}
		else {
			TileScript head = Body[Body.Count - 1].gameObject.GetComponent<TileScript>();
			Vector2 newPos = head.MapPos + CurrentDirection;
			if (GameMaster.instance.BossUnlocked && (newPos.x == 14) && (newPos.y == 4)) {
				GameMaster.instance.BossLevel();
			}
			else if ((newPos.x<0) || (newPos.x>=GameMaster.instance.MapSizeX)) {
				// Play sound
				wallSound.Play();
				Die();
			}
			else if ((newPos.y<0) || (newPos.y >=GameMaster.instance.MapSizeY)) {
				// Play sound
				wallSound.Play();
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

					// Play sound
					foodSound.Play();

					// Update speed
					Speed = Mathf.Max(FastestFrameCount, SlowestFrameCount - (int)SpeedMultiplier*(Body.Count - InitNumber));

					// Regen Foods
					GameMaster.instance.RegenFoods();
				}
				else if (toEat!=null && toEat.tag == "snake") {
					// Play sound
					snakeSound.Play();
					Die();
				}
				else
				{
					UpdateSnakePosition(newPos);
				}
			}
			FramCount=0;
		}

		InputX();
		ChangeBGM();
	}

	public void Die() {
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
		LeadMark.transform.position = newHead.transform.position;
		LeadMark.transform.parent = newHead.transform;
	}

	void ChangeBGM()
	{
		if(Body.Count>=0 && Body.Count<10)
		{
			BGMManager.instance.Play(0);
		}
		else if(Body.Count>=10 /*&& Body.Count<30*/)
		{
			BGMManager.instance.Play(1);
		}
		//else if(Body.Count >=20)
		//{
		//	BGMManager.instance.Play(2);
		//}
	}
}
