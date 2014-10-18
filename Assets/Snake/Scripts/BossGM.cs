using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BossGM : MonoBehaviour {

	public Text t;
	public List<GameObject> SoldierPrefabs;
	public GameObject Boss;

	public int OffsetX;
	public int Speed;
	public Vector3 v;
	public Vector3 SoldierInitPos;

	public List<Image> Bloods;

	private int FrameCount;

	private GameObject test;
	
	// Use this for initialization
	void Start () {
		//Debug.Log(CommanData.CommonSnake.Count);
		//Debug.Log((int)CommanData.CommonSnake[0]);
		test = Instantiate(SoldierPrefabs[0], SoldierInitPos, Quaternion.identity) as GameObject;
		test.SetActive(true);
		test.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
		test.GetComponent<Animator>().SetTrigger("walk_right");
		//for(int i=0; i<CommanData.CommonSnake.Count;i++)
		//{
		//	AudioManager.instance.sequence.Add((int)CommanData.CommonSnake[i]);
		//}
		t.text = "Hello";

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(FrameCount<=Speed)
		{
			FrameCount++;
		}
		else
		{
			//first test if fail

			//else
				CrashToBoss(test);
			// test if win

			FrameCount = 0;
		}
	}

	void CrashToBoss(GameObject tile)
	{
		tile.transform.position += v;

		if(tile.transform.position.x >= Boss.transform.position.x + OffsetX)
		{
			//tile.settrigger()
			Debug.Log("Dead");
		}
	}
}
