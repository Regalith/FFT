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
	private PlayerController player;
	// Use this for initialization
	void Start () {
		SetUpSongInfo ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateScore ();
	}
	
	private void SetUpSongInfo()
	{
		BPMController bpmController = GameObject.FindGameObjectWithTag ("BPMController").GetComponent<BPMController> ();
		songName = bpmController.GetSong ();
		artistName = bpmController.GetArtist ();

		songGui.GetComponent<UILabel> ().text = "Song: " + songName;
		artistGui.GetComponent<UILabel> ().text = "Artist: " + artistName;
	}

	private void UpdateScore()
	{
		scoreGui.GetComponent<UILabel> ().text = "Score: " + player.score;
		multiplierGui.GetComponent<UILabel> ().text = "Multiplier: " + player.multiplier;
	}


	
}
