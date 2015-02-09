using UnityEngine;
using System.Collections;

public class WallDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider collision)
	{
		if (collision.transform.tag == "Wall" || collision.transform.tag == "PowerUp") 
		{
			Destroy(collision.gameObject);
		}
	}
}
