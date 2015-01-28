using UnityEngine;
using System.Collections;

public class RotorSpin : MonoBehaviour {

	public int rotationSpeed;

	// Use this for initialization
	void Start () {
		foreach(AnimationState state in animation)
		{
			state.speed = rotationSpeed;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
