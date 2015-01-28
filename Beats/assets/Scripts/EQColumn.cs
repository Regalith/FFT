using UnityEngine;
using System.Collections;

public class EQColumn : MonoBehaviour {

	public GameObject[] blockArr = new GameObject[8];
	public bool moving = false;
	public GameObject prefab;
	public int speed;


	void Start () 
	{
		/*for (int i = 1; i < blockArr.Length; i++) 
		{
			blockArr[i].gameObject.renderer.enabled = false;
		}*/
	}
	
	void Update () 
	{
	}

	public void updateBlocks(int count)
	{
		for(int i = 1; i < count; i++)
		{
			blockArr[i].gameObject.renderer.enabled = true;
		}
		for (int j = count; j < blockArr.Length; j++) 
		{
			blockArr[j].gameObject.renderer.enabled = false;
		}
	}
	/*
	public void beginMovement()
	{
		GameObject clone;
		clone = (GameObject) Instantiate(prefab,this.transform.position,this.transform.rotation);
		EQColumn script = (EQColumn) clone.GetComponent("EQColumn");
		script.moving = true;
	}*/
}
