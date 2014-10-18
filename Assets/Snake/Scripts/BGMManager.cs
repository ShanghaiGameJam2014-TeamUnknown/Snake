using UnityEngine;
using System.Collections;

public class BGMManager : MonoBehaviour {
	public AudioSource audioMgr;

	public AudioClip[] BGMS;
	private int CurrentBGMIndex = -1;

	private static BGMManager _instance;
	public static BGMManager instance {
		get {
			if(_instance == null) {
				_instance = GameObject.FindObjectOfType<BGMManager>();
			}
			return _instance;
		}
	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play(int n)
	{
		if(CurrentBGMIndex!=n)
		{
			audioMgr.Pause();
			audioMgr.clip = BGMS[n];
			//musiclength = audioMgr.clip.length;
			audioMgr.Play();
			CurrentBGMIndex = n;
		}

		//audioMgr.clip = Resources.LoadAssetAtPath<AudioClip>("../../1");
		//audioMgr.Play();
		//Debug.Break();
		
	}
}
