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
	public int BossHP;

	private int FrameCount;

	private GameObject runningTile;
	private int currentIndex;
	
	// Use this for initialization
	void Start () {
		//Debug.Log(CommanData.CommonSnake.Count);
		//Debug.Log((int)CommanData.CommonSnake[0]);
		//for(int i=0; i<CommanData.CommonSnake.Count;i++)
		//{
		//	AudioManager.instance.sequence.Add((int)CommanData.CommonSnake[i]);
		//}
		//t.text = "Hello";
		currentIndex = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (currentIndex >= CommanData.CommonSnake.Count) {
			// fail
			t.text = "Fail";
			return ;
		}
		t.text = BossHP.ToString();
		if(FrameCount<=Speed)
		{
			FrameCount++;
		}
		else
		{
			if (!runningTile) {
				runningTile = Instantiate(SoldierPrefabs[(int)CommanData.CommonSnake[currentIndex]-1], SoldierInitPos, Quaternion.identity) as GameObject;
				runningTile.SetActive(true);
				runningTile.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
				runningTile.GetComponent<Animator>().SetTrigger("walk_right");
			}

			CrashToBoss();

			if (BossHP <= 0) {
				Application.LoadLevel("win");
			}

			FrameCount = 0;
		}
	}

	void CrashToBoss()
	{
		runningTile.transform.position += v;

		if (runningTile.transform.position.x >= DefeatPos.x)
		{
			runningTile.SetActive(false);
			GameObject.Destroy(runningTile);
			runningTile = null;
			GameObject newDefeat = Instantiate(DefeatrPrefabs[(int)CommanData.CommonSnake[currentIndex]-1], DefeatPos, Quaternion.identity) as GameObject;
			newDefeat.SetActive(true);
			newDefeat.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
			newDefeat.GetComponent<Animator>().SetTrigger("defeat");
			currentIndex++;
			BossHP--;
		}
	}
}
