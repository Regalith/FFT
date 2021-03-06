﻿using UnityEngine;
using System.Collections;

public class BPMController : MonoBehaviour {
	
	public int bpm;
	public GameObject wall;
	public GameObject powerUp;
	public GameObject powerUpSpawnLocation;
	public GameObject floor;



	public AudioClip song;
	public float songDurration{ get; set;}
	public float durrationPlayed{ get; set;}
	public bool songFinished{ get; set; }



	public bool wait = false;
	public int spawnCounter = 0;
	Color ambColor;
	int last = 0;


	public bool playSong{ get; set; }
	private int countDownNum;
	private float defaultDuration;
	public float numberDuration;
	public GameObject[] countDownGameObjects;

	public int spawnInterval = 4;
	// Use this for initialization

	// Make this game object and all its transform children
	// survive when loading a new scene.
	void Awake () {
		DontDestroyOnLoad (transform.gameObject);
		songDurration = song.length;
	}

	/// <summary>
	/// Raises the level was loaded event.
	/// </summary>
	/// <param name="level">Level.</param>
	void OnLevelWasLoaded(int level)
	{
		if(level == 1)
		{
			//Find GameObjects in Scene
			wall = GameObject.FindGameObjectWithTag("Wall");
			floor = GameObject.FindGameObjectWithTag ("Floor");
			powerUpSpawnLocation = GameObject.FindGameObjectWithTag("PowerUpSpawner");
			countDownGameObjects[0] = GameObject.FindGameObjectWithTag ("Go");
			countDownGameObjects[1] = GameObject.FindGameObjectWithTag ("1");
			countDownGameObjects[2] = GameObject.FindGameObjectWithTag ("2");
			countDownGameObjects[3] = GameObject.FindGameObjectWithTag ("3");
			//Turn Off 2 and 1
			countDownGameObjects[0].SetActive(false);
			countDownGameObjects[1].SetActive(false);
			countDownGameObjects[2].SetActive(false);

			countDownNum = 3;
			defaultDuration = 1;
			durrationPlayed = 0;
			playSong = false;
			songFinished = false;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if(Application.loadedLevel == 1)
		{
			if(playSong)
				GenerateWalls ();
			else
				CountDown();
		}
		if (durrationPlayed >= songDurration)
		{
			playSong = false;
			songFinished = true;
		}
		else
		{
			durrationPlayed += Time.deltaTime;
		}

	}

	/// <summary>
	/// Displays count down numbers in order
	/// </summary>
	void CountDown()
	{
		if(numberDuration <= 0)
		{
			if(countDownNum < 0)
			{
				playSong = true;
				countDownGameObjects[0].SetActive(false);
			}
			else
			{
				numberDuration = defaultDuration;
				countDownGameObjects[countDownNum].SetActive(false);
				countDownNum--;
				if(countDownNum >= 0)
					countDownGameObjects[countDownNum].SetActive(true);
			} 
		}
		else
		{
			numberDuration -= Time.deltaTime;
		}
	}
	/// <summary>
	/// Generates level walls.
	/// </summary>
	void GenerateWalls()
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
		}

	}

	IEnumerator waitCoRoutine(){
		//this doesnt seem right.
		yield return new WaitForSeconds(60/bpm);
		spawnCounter++;
		wait = false;
	}
	/// <summary>
	/// Spawns the wall.
	/// </summary>
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

		int chance = Random.Range (0, 4);

		if (chance == 1)
			spawnPowerUp ();
	}

	void spawnPowerUp()
	{
		GameObject powerUpClone = null;
		powerUpClone = Instantiate(powerUp,powerUpSpawnLocation.transform.position, powerUpSpawnLocation.transform.rotation) as GameObject;
		powerUpClone.transform.parent = floor.transform;
		powerUpClone.layer = 0;
	}

	/// <summary>
	/// Turns the a random wall segment.
	/// </summary>
	/// <returns><c>true</c>, if off segment was turned, <c>false</c> otherwise.</returns>
	/// <param name="wallClone">Wall clone.</param>
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
	public void StopCoutines()
	{
		StopCoroutine("waitCoRoutine");
	}
}
