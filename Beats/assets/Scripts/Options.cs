using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {

	private Color defaultColor;
	// Use this for initialization
	void Start () {
		defaultColor = RenderSettings.ambientLight;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	/// <summary>
	/// Modifies the ambient color to affect brightness.
	/// </summary>
	/// <param name="level">Brightness Level.</param>
	public void ModifyBrightness(float level)
	{
		float delta = (level - .5f) * 3;
		float colorValue = defaultColor.r;
		RenderSettings.ambientLight = new Color (colorValue + (colorValue * delta), colorValue + (colorValue * delta),colorValue + (colorValue * delta));
	}

	public void ModifyVolume(float level)
	{
		AudioListener.volume = level;
	}

	public void ModifyQuality(GameObject level)
	{
		gameObject.GetComponent<AudioSource> ().audio.Play ();

		switch(level.name)
		{
		case "VeryLow":
			QualitySettings.SetQualityLevel(1);
			break;
		case "Low":
			QualitySettings.SetQualityLevel(2);
			break;
		case "Medium":
			QualitySettings.SetQualityLevel(3);
			break;
		case "High":
			QualitySettings.SetQualityLevel(4);
			break;
		case "VeryHigh":
			QualitySettings.SetQualityLevel(5);
			break;
		}
	}
}
