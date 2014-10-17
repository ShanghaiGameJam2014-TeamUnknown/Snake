using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public GameObject GameMap;

	private float passedTime;
	private MapScript mapLogic;

	// Use this for initialization
	void Start () {
		passedTime = 0;
		mapLogic = GameMap.GetComponent<MapScript>();
	}
	
	// Update is called once per frame
	void Update () {
		passedTime += Time.deltaTime;
		if (passedTime > 2) {
			mapLogic.RandomFood();
			passedTime = 0;
		}
	}
}
