using UnityEngine;
using System.Collections;

public class MoonRotator : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (0, speed * Time.deltaTime, 0);
	}
}
