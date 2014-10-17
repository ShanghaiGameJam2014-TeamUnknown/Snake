using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {
	
	public int MapSize;
	public GameObject TilePrefab;

	private GameObject[,] MapTiles;

	// Use this for initialization
	void Start () {
		MapTiles = new GameObject[MapSize,MapSize];

		for (int i=0; i<MapSize; i++) {
			for (int j=0; j<MapSize; j++) {
				MapTiles[i, j] = Instantiate(TilePrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
				TileScript unit = MapTiles[i, j].GetComponent<TileScript>();
				unit.MapPos.x = i;
				unit.MapPos.y = j;
				unit.UpdatePosition();
				MapTiles[i, j].SetActive(true);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
