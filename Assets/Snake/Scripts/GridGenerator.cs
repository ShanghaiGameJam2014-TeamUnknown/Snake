using UnityEngine;
using System.Collections;

public class GridGenerator : MonoBehaviour {

	public GameObject Prefab;

	public int Width;
	public int Height;
	public int UnitSize;

	// Use this for initialization
	void Start () {
		Generate();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Generate()
	{
		for (int i=0; i<Height; i++) {
			for (int j=0; j<Width; j++) {
				Vector3 pos = new Vector3();
				pos.x = UnitSize*j / Utilities.PIXELPERUNIT + GetMapOffsetX();
				pos.y = UnitSize*i / Utilities.PIXELPERUNIT + GetMapOffsetY();
				pos.z = 0;
				Instantiate(Prefab, pos, Quaternion.identity);
			}
		}
	}

	public float GetMapOffsetX()
	{
		return -(Width/2*UnitSize/Utilities.PIXELPERUNIT);
	}
	
	public float GetMapOffsetY()
	{
		return -(Height/2*UnitSize/Utilities.PIXELPERUNIT);
	}
}
