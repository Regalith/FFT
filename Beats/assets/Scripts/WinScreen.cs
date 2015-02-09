using UnityEngine;
using System.Collections;

public class WinScreen : MonoBehaviour {

	public GameObject scoreGui;
	public GameObject artistGui;
	public GameObject songGui;

	public BPMController bpmController;
	public PlayerController player;
	// Use this for initialization
	void OnEnable () {
		bpmController = GameObject.FindGameObjectWithTag ("BPMController").GetComponent<BPMController> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();

		scoreGui.GetComponent<UILabel> ().text = "Score: " + player.score;
		songGui.GetComponent<UILabel> ().text = "Song: " + bpmController.GetSong ();
		artistGui.GetComponent<UILabel> ().text = "Artist: " + bpmController.GetArtist ();
	}

	public void RestartGame()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	public void Home()
	{
		Application.LoadLevel (0);

	}
}
