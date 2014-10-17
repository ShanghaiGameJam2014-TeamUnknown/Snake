using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	public int MapSize;

	private GameObject[,] Map;

	// Use this for initialization
	void Start () {
		Map = new GameObject[32, 32];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
