using UnityEngine;
using System.Collections;

public class FFT : MonoBehaviour {

	public GameObject[] blocks;
	private float[] sample = new float[512];
	private float[] values = new float[8];


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		int count = 0;
		//float diff = 0;
		
		AudioListener.GetSpectrumData(sample, 0, FFTWindow.BlackmanHarris);
		for (int i = 0; i < 8; i++)
		{
			float average = 0;
			int sampleCount = (int) Mathf.Pow(2,i)*2;
			
			for(int j = 0; j < sampleCount; j++)
			{
				average += sample[count]*(count + 1);
				count++;
			}
			
			average/=count;
			
			
			//diff = Mathf.Clamp(average * 10 - values[i%8], 0, 4);
			
			values[i] =  Mathf.Clamp(average * 20, 1,8);
			EQColumn script;
			script = blocks[i].gameObject.GetComponent<EQColumn>();
			script.updateBlocks((int)values[i]);
		}
	}
}
