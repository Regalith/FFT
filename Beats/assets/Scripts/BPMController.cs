﻿using UnityEngine;
using System.Collections;

public class BPMController : MonoBehaviour {
	
	public int bpm;
	public GameObject wall;
	public GameObject floor;
	public AudioClip song;
	bool wait = false;
	int spawnCounter = 0;
	Color ambColor;
	int last = 0;

	public int spawnInterval = 4;
	// Use this for initialization

	// Make this game object and all its transform children
	// survive when loading a new scene.
	void Awake () {
		DontDestroyOnLoad (transform.gameObject);
	}

	void OnLevelWasLoaded(int level)
	{
		if(level == 1)
		{
			wall = GameObject.FindGameObjectWithTag("Wall");
			floor = GameObject.FindGameObjectWithTag ("Floor");
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if(Application.loadedLevel == 1)
		{
			if (!wait) 
			{
				wait = true;
				StartCoroutine(waitCoRoutine());
			}
			if(spawnCounter >= spawnInterval)
			{
				spawnWall();
				spawnCounter = 0;

				//pulse ambient light
				//PulseAmbient();
			}
			//if(ambColor.r <= 142)
				//DimPulse();
		}

	}

	IEnumerator waitCoRoutine(){
		//this doesnt seem right.
		yield return new WaitForSeconds(60/bpm);
		spawnCounter++;
		wait = false;
	}
	void spawnWall(){
		GameObject wallClone = null;

		wallClone = Instantiate (wall, wall.transform.position, wall.transform.rotation) as GameObject;
		wallClone.transform.parent = floor.transform;
		wallClone.layer = 0;

		//delete a random column from the wall
		while (!TurnOffSegment(wallClone)) 
		{
			TurnOffSegment(wallClone);
		}



	}
	bool TurnOffSegment(GameObject wallClone)
	{
		int rand;
		rand = Random.Range (0, 7);
		if(Mathf.Abs(rand - last) < 6)
		{
			wallClone.transform.GetChild (rand).gameObject.SetActive(false);
			last = rand;
			return true;
		}
		else
		{
			return false;
		}
		 

	}
	void PulseAmbient()
	{
		ambColor = Color.white;
		RenderSettings.ambientLight = Color.white;
	}
	void DimPulse()
	{
		ambColor.r++;
		ambColor.b++;
		ambColor.g++;
		RenderSettings.ambientLight = ambColor;
	}

	public AudioClip GetAudio()
	{
		return song;
	}
	public string GetArtist()
	{
		return  song.ToString ().Split ('_') [0];
	}
	public string GetSong()
	{
		return  song.ToString ().Split ('_') [1].Split ('(')[0];
	}
}