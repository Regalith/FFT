using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{

	public float min, max;
	public int speed;
	public GameObject ship;

	private float translation;
	private float mobileTranslation;

	//Rotation Variables
	public float rotationSpeed;
	public float maxRotation;
	private Quaternion defaultRotation;
	private Quaternion leftRotation;
	private Quaternion rightRotation;


	// Use this for initialization
	void Start () 
	{
		defaultRotation = this.transform.rotation;
		leftRotation.eulerAngles = new Vector3(defaultRotation.eulerAngles.x,defaultRotation.eulerAngles.y, defaultRotation.eulerAngles.z + maxRotation);
		rightRotation.eulerAngles = new Vector3(defaultRotation.eulerAngles.x,defaultRotation.eulerAngles.y, defaultRotation.eulerAngles.z - maxRotation);

	}
	
	// Update is called once per frame
	void Update () 
	{

		/**
		 * For debugging purposes
		 */
		translation = Input.GetAxis("Horizontal") * speed; 
		//Debug.Log (translation);
		
		if(translation == 0)
		{
			translation = mobileTranslation;
		}
		/**
		 * For debugging purposes
		 */

		RotatePlayer();


		if(translation > 0 && this.transform.position.z < max)
		{
			transform.Translate(0,0,translation * speed * Time.deltaTime,Space.World);
		}
		else if( translation < 0 && this.transform.position.z > min)
		{
			transform.Translate(0,0,translation * speed * Time.deltaTime,Space.World);
		}


	}




	void RotatePlayer()
	{
		/**
		 * Desktop Smooth Rotation 
		 */

		if(translation > .8f)
		{
			transform.rotation = Quaternion.Lerp (this.transform.rotation, rightRotation, rotationSpeed * Time.deltaTime);
		}
		else if(translation < -.8f)
		{
			transform.rotation = Quaternion.Lerp (this.transform.rotation, leftRotation, rotationSpeed * Time.deltaTime);
		}
		else
		{
			transform.rotation = Quaternion.Lerp (this.transform.rotation, defaultRotation, rotationSpeed * Time.deltaTime);
		}

		/**
		 * Mobile Smooth Rotation 
		 */
		/*
		if(translation == 5)
		{
			transform.rotation = Quaternion.Lerp (this.transform.rotation, rightRotation, rotationSpeed * Time.deltaTime);
		}
		else if(translation == -5)
		{
			transform.rotation = Quaternion.Lerp (this.transform.rotation, leftRotation, rotationSpeed * Time.deltaTime);
		}
		else
		{
			transform.rotation = Quaternion.Lerp (this.transform.rotation, defaultRotation, rotationSpeed * Time.deltaTime);
		}
		*/

	}


	void OnGUI()
	{
		if(GUI.RepeatButton(new Rect(0,0,Screen.width/2, Screen.height),"", GUIStyle.none))
		{
			mobileTranslation = -5;
		}
		else if(GUI.RepeatButton(new Rect(Screen.width/2, 0, Screen.width/2,Screen.height),"", GUIStyle.none))
		{
			mobileTranslation = 5;
		}
		else
		{
			mobileTranslation = 0;
		}

	}


}
