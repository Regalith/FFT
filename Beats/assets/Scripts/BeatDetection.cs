/* Module      : Beat Detection
 * Author      : Timothy Bujnevicie
 * Email       : tsbujnevicie@wpi.edu
 *
 * Description : Determine the propper BPM of the song 
 *
 * Date        : 2014/01/27
 *
 *
 */

using UnityEngine;
using System.Collections;
using System;

public class BeatDetection : MonoBehaviour 
{
	//public AudioListener pAudioListener;
	public float[] currentSample = new float[1024];
	public float[] historyBuffer = new float[43];
	public float currentEnergy = 0;
	public float averageEnergy = 0;
	public float variance = 0;
	public float constant = 0;

	public int textureIndex = 0;
	private Color white = new Color (255, 255, 255);
	private Color black = new Color(0,0,0);
	public static bool beat = false;
	void Start()
	{
		//pAudioListener = GameObject.FindGameObjectWithTag ("Player").GetComponent<AudioListener> ();;
	}

	void Update()
	{
		//perform a FFT on the audioclip
		AudioListener.GetSpectrumData(currentSample, 0, FFTWindow.BlackmanHarris);

		currentEnergy = CalculateInstantEnergy ();

		averageEnergy = CalculateAverageLocalEnergy ();

		variance = CalculateHistoryVariance ();

		constant = (-.0025714f * variance + 1.5142857f);

		ShiftHistoryBuffer ();

		if(currentEnergy > constant * averageEnergy)
		{
			beat = true;
		}
		else
		{
			beat = false;
		}
		Beat ();
	}


	float CalculateInstantEnergy()
	{
		float instantEnergy = 0;
		for(int i = 0; i < currentSample.Length; i++)
		{
			instantEnergy += Mathf.Pow(currentSample[i],2.0f);
		}
		return instantEnergy;
	}

	float CalculateAverageLocalEnergy()
	{
		float average = 0;
		for(int i = 0; i < historyBuffer.Length; i++)
		{
			average += Mathf.Pow(historyBuffer[i],2.0f);
		}
		average /= historyBuffer.Length;

		return average;
	}

	float CalculateHistoryVariance()
	{
		float variance = 0;
		for(int i = 0; i < historyBuffer.Length; i++)
		{
			variance += Mathf.Pow(historyBuffer[i] - averageEnergy,2.0f);
		}
		variance /= historyBuffer.Length;

		return variance;
	}

	void ShiftHistoryBuffer()
	{
		float[] newHistoryBuffer = new float[43];
		Array.Copy (historyBuffer, 0, newHistoryBuffer, 1, newHistoryBuffer.Length - 1);
		newHistoryBuffer [0] = currentEnergy;

		historyBuffer = newHistoryBuffer;
	}

	void Beat()
	{
		if (beat)
			this.gameObject.renderer.materials [textureIndex].color = white;
		else
			this.gameObject.renderer.materials [textureIndex].color = black;
	}
}

