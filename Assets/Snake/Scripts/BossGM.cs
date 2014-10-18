using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossGM : MonoBehaviour {

	public List<GameObject> SoldierPrefabs;
	public GameObject Boss;

	// Use this for initialization
	void Start () {
		Debug.Log(CommanData.CommonSnake.Count);
		Debug.Log((int)CommanData.CommonSnake[0]);
		GameObject test = Instantiate(SoldierPrefabs[(int)CommanData.CommonSnake[0] - 1], new Vector3(0,0,0), Quaternion.identity) as GameObject;
		test.SetActive(true);


		for(int i=0; i<CommanData.CommonSnake.Count;i++)
		{
			AudioManager.instance.sequence.Add((int)CommanData.CommonSnake[i]);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
