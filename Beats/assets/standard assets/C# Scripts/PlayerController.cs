using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float min, max;
	public int speed;
	float currentRotation = 0;
	public GameObject ship;
	public int maxRotation;
	public float rotationSpeed;
	private Quaternion shipRotation;
	public float turnSpeed;
	private float translation;

	// Use this for initialization
	void Start () {
		shipRotation = ship.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		 translation = Input.GetAxis("Horizontal") * speed; 
		//Debug.Log (translation);


		if (this.transform.position.z > max) 
		{
			this.transform.Translate (-turnSpeed * Time.deltaTime, 0, 0);
		}
		else if (this.transform.position.z < min)
		{
			this.transform.Translate (turnSpeed * Time.deltaTime, 0, 0);
		}
		else 
		{
			this.transform.Translate (translation * turnSpeed * Time.deltaTime, 0, 0);
		}



		//RotateCamera(translation);
	}



}
