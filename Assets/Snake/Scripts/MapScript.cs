using UnityEngine;
using System.Collections;

public class MapScript : MonoBehaviour {
	
	public int MapSize;
	public GameObject TilePrefab;
	public GameObject FoodPrefab;

	private GameObject[,] MapTiles;
	private float MapOffset;

	// Use this for initialization
	void Start () {
		int unitSize = TilePrefab.GetComponent<TileScript>().UnitSize;
		MapOffset = -(MapSize/2*unitSize/Utilities.PIXELPERUNIT);
		transform.position = new Vector3(MapOffset, MapOffset, 0);

		MapTiles = new GameObject[MapSize,MapSize];
		for (int i=0; i<MapSize; i++) {
			for (int j=0; j<MapSize; j++) {
				MapTiles[i, j] = Instantiate(TilePrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
				TileScript unit = MapTiles[i, j].GetComponent<TileScript>();
				unit.MapPos.x = i;
				unit.MapPos.y = j;
				unit.MapOffset = MapOffset;
				unit.UpdatePosition();
				MapTiles[i, j].SetActive(true);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RandomFood() {
		int randX = Random.Range(0, MapSize-1);
		int randY = Random.Range(0, MapSize-1);
		GameObject.Destroy(MapTiles[randX, randY]);
		MapTiles[randX, randY] = Instantiate(FoodPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
		TileScript unit = MapTiles[randX, randY].GetComponent<TileScript>();
		unit.MapPos.x = randX;
		unit.MapPos.y = randY;
		unit.MapOffset = MapOffset;
		unit.UpdatePosition();
		MapTiles[randX, randY].SetActive(true);
	}
}
