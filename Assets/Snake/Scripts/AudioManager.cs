﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
	public AudioSource audioMgr;
	//private float musiclength;
	private float waittime;
	private int idx;
	private int k;
	private float[] TimeArray;

	private int current;
	public List<int> sequence;

	void Awake()
	{
		sequence = new List<int>();
		k = 0;
		idx = 0;
	}

	// Use this for initialization
	void Start () {
		//TimeArray = new float[]{1f,0.5f,1f,0.5f,1f,0.5f,1f,0.5f};
		TimeArray = new float[]{0.5f,0.5f,1f,0.5f,0.5f,1f,0.5f,0.5f,1f,0.5f,0.5f,1f};
	}
	
	// Update is called once per frame
	void Update () {
		if(sequence.Count <=0)
		{
			return;
		}

		if (current >= 1) 
		{
			waittime += Time.deltaTime;

		}

		if (waittime>=TimeArray[k]/2|| current==1)
		{
			  string filename = ((int)CommanData.CommonSnake[idx]).ToString();
			  Play (filename);
			current=sequence[0]+1;
			sequence.RemoveAt(0);
			//Random.Range (1,7);
			  waittime=0;
			  if (k<=TimeArray.Length-2)
			  {
				k+=1;
			  }
			  else
			  {
				idx ++;
				if (idx >= CommanData.CommonSnake.Count) {
					idx = 0;
				}
				k=0;
			   }
			}
		}

	public void Play(string str)
	{
		audioMgr.clip = (AudioClip)Resources.Load(str, typeof(AudioClip));//调用Resources方法加载AudioClip资源
		audioMgr.volume = 3;
		//musiclength = audioMgr.clip.length;
		audioMgr.Play();
		//audioMgr.clip = Resources.LoadAssetAtPath<AudioClip>("../../1");
		//audioMgr.Play();
		//Debug.Break();

	}

	private static AudioManager _instance;
	public static AudioManager instance {
		get {
			if(_instance == null) {
				_instance = GameObject.FindObjectOfType<AudioManager>();
			}
			return _instance;
		}
	}
}