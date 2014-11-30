using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

	public float score = 0f;

	private int multiplier = 1;
	private int comboCount = 0;

	//Text Meshes
	public TextMesh scoreMesh;
	public TextMesh multiplierMesh;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		score += Time.deltaTime * 100f * multiplier;
		UpdateTextMeshes();
	}


	void UpdateTextMeshes()
	{
		scoreMesh.text = "Score: " + (int)score;
		multiplierMesh.text = "Multiplier: " + multiplier + "X";
	}


	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag == "column")
		{
			score -= 100;
			comboCount = 0;
		}
		if(other.transform.tag == "coin")
		{
			score += 100;
			comboCount++;
		}
	}

	void OnGUI()
	{
		GUI.Box (new Rect(0, Screen.height * 3/4, Screen.width/3, Screen.height/4),"");
	}



}
