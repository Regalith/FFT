using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	//Movement Data
	public float min, max;
	public int speed;
	private float translation;
	private float mobileTranslation;
	
	//Score Data
	public int score{ get; set; }
	private float fScore = 0;
	public int multiplier { get; set;}
	public float baseMultiplier = 1;
	private BPMController bpmController;

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
		this.GetComponent<AudioSource> ().clip = GameObject.FindGameObjectWithTag ("BPMController").GetComponent<BPMController> ().GetAudio ();
		score = 0;
		multiplier = 0;
		bpmController = GameObject.FindGameObjectWithTag ("BPMController").GetComponent<BPMController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{


		translation = Input.GetAxis("Horizontal") * speed; 

		//if(translation == 0)
		//{
		//	translation = mobileTranslation;
		//}


		RotatePlayer();


		if(translation > 0 && this.transform.position.z < max)
		{
			transform.Translate(0,0,translation * speed * Time.deltaTime,Space.World);
		}
		else if( translation < 0 && this.transform.position.z > min)
		{
			transform.Translate(0,0,translation * speed * Time.deltaTime,Space.World);
		}

		if(bpmController.beginSong)
			IncrementScore ();
			
	}




	void RotatePlayer()
	{
		/**
		 * Desktop Smooth Rotation 
		 */

		if(translation > .99f)
		{
			transform.rotation = Quaternion.Lerp (this.transform.rotation, rightRotation, rotationSpeed * Time.deltaTime);
		}
		else if(translation < -.99f)
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

	private void IncrementScore()
	{
		fScore += Time.deltaTime * baseMultiplier;
		score = (int)fScore;
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
