using UnityEngine;
using System.Collections;

public class WallDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "Wall") 
		{
			Destroy(collision.gameObject);
		}
	}
}
