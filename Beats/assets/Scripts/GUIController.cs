using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {


	private string songName;
	private string artistName;
	//Text Meshes
	public GameObject scoreGui;
	public GameObject multiplierGui;
	public GameObject artistGui;
	public GameObject songGui;
	public GameObject timeGUI;
	public GameObject pauseMenu;
	public GameObject winScreen;

	private PlayerController player;
	private BPMController bpmController;

	private bool finished;
	// Use this for initialization
	void Start () {
		finished = false;
		SetUpSongInfo ();
		Debug.Log (Time.timeScale);
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateScore ();
		if(Input.GetKeyUp(KeyCode.Escape) && !bpmController.songFinished && bpmController.playSong)
		{
			PauseGame();
		}
		if(bpmController.songFinished && !finished)
		{
			finished = true;
			WinScreen();
		}
	}
	
	private void SetUpSongInfo()
	{
		bpmController = GameObject.FindGameObjectWithTag ("BPMController").GetComponent<BPMController> ();
		songName = bpmController.GetSong ();
		artistName = bpmController.GetArtist ();

		songGui.GetComponent<UILabel> ().text = "Song: " + songName;
		artistGui.GetComponent<UILabel> ().text = "Artist: " + artistName;
		timeGUI.GetComponent<UILabel> ().text = "Time: " + (int)bpmController.durrationPlayed + " / " + (int)bpmController.songDurration;
	}

	private void UpdateScore()
	{
			scoreGui.GetComponent<UILabel> ().text = "Score: " + player.score;
			multiplierGui.GetComponent<UILabel> ().text = "Multiplier: " + player.multiplier;
			timeGUI.GetComponent<UILabel> ().text = "Time: " + (int)bpmController.durrationPlayed + " / " + (int)bpmController.songDurration;
	}

	public void PauseGame()
	{
		scoreGui.SetActive (false);
		multiplierGui.SetActive (false);
		songGui.SetActive (false);
		artistGui.SetActive (false);
		timeGUI.SetActive (false);
		pauseMenu.SetActive (true);
		player.audio.Pause ();
		Time.timeScale = 0;
	}
	public void ResumeGame()
	{
		bpmController.StopCoutines ();
		//bpmController.wait = false;
		//bpmController.spawnCounter = 0;
		scoreGui.SetActive (true);
		multiplierGui.SetActive (true);
		songGui.SetActive (true);
		artistGui.SetActive (true);
		timeGUI.SetActive (true);
		pauseMenu.SetActive (false);
		player.audio.Play ();
		Time.timeScale = 1;
	}

	public void WinScreen()
	{
		scoreGui.SetActive (false);
		multiplierGui.SetActive (false);
		songGui.SetActive (false);
		artistGui.SetActive (false);
		timeGUI.SetActive (false);
		winScreen.SetActive (true);
	}

}
