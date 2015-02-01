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
		songSelect.SetActive (false);
		options.SetActive (false);
		initialPage.SetActive (true);
		help.SetActive (false);
		wall.SetActive (true);
	}

	public void SongSelect()
	{
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
		initialPage.SetActive (false);
		options.SetActive (true);
	}
	public void Help()
	{
		initialPage.SetActive (false);
		help.SetActive (true);
	}
	public void StartGame()
	{
		Application.LoadLevel (1);
	}


	public void Quit()
	{
		Application.Quit ();
	}
}
