using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void RestartGame()
	{
		Time.timeScale = 1;
		Application.LoadLevel (Application.loadedLevel);
	}
	public void QuitGame()
	{
		Application.Quit ();
	}

	
}
