using UnityEngine;
using System.Collections;

public class GlowIntensity : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(BeatDetection.beat)
		{

			this.GetComponent<UISprite>().enabled = true;
		}
		else
		{
			this.GetComponent<UISprite>().enabled = false;
		}
	}
}
