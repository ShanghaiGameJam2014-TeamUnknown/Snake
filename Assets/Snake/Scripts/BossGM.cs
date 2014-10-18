using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossGM : MonoBehaviour {
	
	private List<GameObject> SnakeBody;
	// Use this for initialization
	void Start () {
		SnakeBody = Snake.instance.Body;
		Debug.Log(SnakeBody.Count);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
