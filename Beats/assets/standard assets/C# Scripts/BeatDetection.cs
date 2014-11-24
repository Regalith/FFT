﻿/* Module      : Beat Detection
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

public class BeatDetection : MonoBehaviour {

	float[] currentSample = new float[1024];
	float[] subBands = new float[32];
	float[][] subBandHistorys = new float[32][];
	float[] subBandHistoryAverages = new float[32];
	float min = 0;
	float max = 0;
	
	void Start()
	{
		for (int i = 0; i < subBandHistorys.Length; i++)
				subBandHistorys [i] = new float[43];
	}
	// Update is called once per frame
	void Update () 
	{

		float varianceConstant = 0;
		bool beatPresent = false;



		//perform a FFT on the audioclip
		AudioListener.GetSpectrumData(currentSample, 0, FFTWindow.BlackmanHarris);

		GenorateSubBands();


		//create and update the history buff of each subband
		for (int i = 0; i < subBandHistorys.Length; i++) 
		{
			subBandHistoryAverages[i] = GenorateHistoryBuff(subBands[i],subBandHistorys[i]);
		}

		for (int j = 0; j < 6; j++) 
		{

			varianceConstant = GenorateConstant(subBands[j],subBandHistorys[j]);
			if(subBands[j]/subBandHistoryAverages[j] >= varianceConstant)
			{
				if(!beatPresent)
				{
					beatPresent = true;
					Beat ();
				}
			}
		}
	}

	void GenorateSubBands()
	{
		float subBandPeak = 0;
		for(int i = 0; i < subBands.Length;i++)
		{
			for(int j = i * subBands.Length; j < subBands.Length * (i + 1); j++)
			{
				subBandPeak += Mathf.Pow(currentSample[j],2);
			}
			subBands[i] = subBandPeak;
			subBandPeak = 0;
		}
	}

	float GenorateHistoryBuff(float peak,float[] history)
	{
		float average = 0;

		//check if buffer has been propigated yet
		if (history[42] != 0) 
		{

			for (int i = 0; i < history.Length; i++) 
			{
				average += history [i];
			}

			average /= history.Length;
		}

		//shift all values right and place peak at start of array
		for(int j = 41; j >= 0; j--)
		{
			history[j+1] = history[j];
		}
		
		history[0] = peak;


		return average;
	}
	

	float GenorateConstant(float peak, float[] history)
	{
		float constant = 0;
		float variance = 0;

		for (int i = 0; i < history.Length; i++)
		{
			variance += Mathf.Pow((history[i] - peak), 2);
		}
		variance /= history.Length;

		constant = (-0.0025714f * variance) + 1.5142857f;

		return constant;
	}


	void Beat()
	{
	}
	
}
