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

			this.renderer.material.color = new Color(255.0f,255.0f,255.0f);
		}
		else
		{
			this.renderer.material.color = new Color(0.0f,0.0f,0.0f);
		}
	}
}
