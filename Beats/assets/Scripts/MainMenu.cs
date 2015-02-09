using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public BPMController bpmController;
	public GameObject initialPage;
	public GameObject songSelect;
	public GameObject options;
	public GameObject help;
	public GameObject wall;

	public void Home()
	{
		GetComponent<AudioSource> ().audio.Play ();
		songSelect.SetActive (false);
		options.SetActive (false);
		initialPage.SetActive (true);
		help.SetActive (false);
		wall.SetActive (true);
	}

	public void SongSelect()
	{
		GetComponent<AudioSource> ().audio.Play ();
		initialPage.SetActive (false);
		wall.SetActive (false);
		songSelect.SetActive (true);
	}
	public void SetSong(AudioClip clip)
	{
		bpmController.song = clip;
	}	
	public void Options()
	{
		GetComponent<AudioSource> ().audio.Play ();
		initialPage.SetActive (false);
		options.SetActive (true);
	}
	public void Help()
	{		
		GetComponent<AudioSource> ().audio.Play ();
		initialPage.SetActive (false);
		help.SetActive (true);
	}
	public void StartGame()
	{
		GetComponent<AudioSource> ().audio.Play ();
		Application.LoadLevel (1);
	}


	public void Quit()
	{
		GetComponent<AudioSource> ().audio.Play ();
		Application.Quit ();
	}
}
