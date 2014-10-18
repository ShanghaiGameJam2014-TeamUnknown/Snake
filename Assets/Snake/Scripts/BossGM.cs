using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BossGM : MonoBehaviour {

	public Text t;
	public List<GameObject> SoldierPrefabs;
	public List<GameObject> DefeatrPrefabs;
	public GameObject Boss;

	public int OffsetX;
	public int Speed;
	public Vector3 v;
	public Vector3 SoldierInitPos;
	public Vector3 DefeatPos;

	public List<Image> Bloods;

	private int FrameCount;

	private GameObject test;
	
	// Use this for initialization
	void Start () {
		//Debug.Log(CommanData.CommonSnake.Count);
		//Debug.Log((int)CommanData.CommonSnake[0]);
		test = Instantiate(SoldierPrefabs[6], SoldierInitPos, Quaternion.identity) as GameObject;
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
			if (test) {
				CrashToBoss(test);
			}
			// test if win

			FrameCount = 0;
		}
	}

	void CrashToBoss(GameObject tile)
	{
		tile.transform.position += v;

		if (tile.transform.position.x >= DefeatPos.x)
		{
			tile.SetActive(false);
			GameObject.Destroy(tile);
			GameObject newDefeat = Instantiate(DefeatrPrefabs[6], DefeatPos, Quaternion.identity) as GameObject;
			newDefeat.SetActive(true);
			newDefeat.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
			newDefeat.GetComponent<Animator>().SetTrigger("defeat");
			Debug.Log("Dead");
		}
	}
}
