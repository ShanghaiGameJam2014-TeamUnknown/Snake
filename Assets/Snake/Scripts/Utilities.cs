using UnityEngine;
using System.Collections;

public static class Utilities
{
	public enum TileType
	{
		EMPTY = 0,
		BLUE,
		GOLDEN,
		GREEN,
		ORANGE,
		PURPLE,
		WHITE,
		YELLOW
	}

	public const float PIXELPERUNIT = 100;
	public static Vector2 UP
	{
		get {return new Vector2(0, 1);}
	}

	public static Vector2 DOWN
	{
		get{return new Vector2(0, -1);}
	}

	public static Vector2 RIGHT
	{
		get{return new Vector2(1, 0);}
	}

	public static Vector2 LEFT
	{
		get{return new Vector2(-1, 0);}
	}
}
