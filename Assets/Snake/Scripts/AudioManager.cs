using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	public AudioSource audioMgr;
	//private float musiclength;
	private float waittime;
	private int yinfu;
	private int k;
	private float[] TimeArray;
	// Use this for initialization
	void Start () {
		//TimeArray = new float[]{1f,0.5f,1f,0.5f,1f,0.5f,1f,0.5f};
		TimeArray = new float[]{0.5f,0.5f,1f,0.5f,0.5f,1f,0.5f,0.5f,1f,0.5f,0.5f,1f};
	}
	
	// Update is called once per frame
	void Update () {
		if (yinfu >= 1) 
		{
			waittime += Time.deltaTime;

		}
		if (Input.anyKeyDown) {
			yinfu=1;
			k=0;
		}
			if (waittime>=TimeArray[k]/2|| yinfu==1)
		{
			  string filename = yinfu.ToString();
			  Play (filename);
			  yinfu=Random.Range (1,7);
			  waittime=0;
			  if (k<=TimeArray.Length-2)
			  {
				k+=1;
			  }
			  else
			  {
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
}