#pragma strict

var scale : float; //Scale along x (length)
var yScale : float = 1; //Scale of all frequency-based motion on the y
var scaleOffset : float; //Scaling of sine waves
var speedOffset : float = 8; //Speed of sine waves
var minSin : float; //Minimum amplitude multiplier of sine waves.
var channel : int; //Which channel to pull audio from (0 or 1)
var numSamples : int; //Number of samples to take from audio
var ad : AudioSource; //AudioSource to pull data from
var sineFade : float; //Number of points along lineRenderer it takes for sine wave to fade out
public var audSource: GameObject;

private var fCur : float[]; //Current frequency value (per vertex)
private var fMax : float[]; //Target frequency value (per vertex)

private var spectrum : float[]; //Arrat to store spectrum data from audio
private var rmsValue : float;
private var volume : float[]; //Array to store outpu data in for calculating volume
private var mMax : float; // Target value for sine amplitude multiplier
private var mCur : float = 0; //Current value for sine amplitude multiplier

var renderers : LineRenderer[];

function Start () {
		
		//Allocate arrays
		volume = new float[numSamples];
		spectrum = new float[numSamples];
		fCur = new float [numSamples];
		fMax = new float [numSamples];
		ad = audSource.GetComponent(AudioSource);
		for(var l : LineRenderer in renderers){
        	l.SetVertexCount (numSamples);
        	l.useWorldSpace = false;
        }
}

function Update () {
	
	/**Get volume - Thank you to Aldo Naletto on Unity Answers**/
	ad.GetOutputData(volume, channel); // fill array with samples
	var p: int;
	var sum: float = 0;
	for (p=0; p < numSamples; p++){
		sum += volume[p]*volume[p]; // sum squared samples
	}
	
	rmsValue = Mathf.Sqrt(sum/numSamples); // rms = square root of average
	rmsValue = Mathf.Max(rmsValue, .3);
	/***********************************************************/
	
	//Get Spectrum data
	ad.GetSpectrumData(spectrum, channel, FFTWindow.Rectangular);


	/**Transform sine amplitude multiplier towards target**/
	//Multiplier is based on the frequency of the first data point as well as the overall volume
	
	mMax = Mathf.Max(mMax, rmsValue*.5 + spectrum[0]*.5);//We want to hit the highest value, so compare current target with new data point
	var mDif : float = Mathf.Sign(mMax - mCur); //Get direction of difference between target and current
	mCur = Mathf.Clamp(mCur + mDif*Time.deltaTime*5, minSin , mMax); //Transform towards target
	
	//If we have surpassed the target, start lowering it
	if(mCur >= mMax){
		mMax -= Time.deltaTime;
	}
	//Set the multiplier 
	//(It's also a sine to add to the pulsing effect)
	var sc : float = Mathf.Sin(Time.time*speedOffset)*mCur*scaleOffset;
	/*********************************************************/
	
	
	
	//Iterate through each sample and linerenderer point
	for(var i : int = 0; i < numSamples; i++){
			
		   //Move along x plane based on scale
		   var x : float = scale*i;
		   
		   //Move last segment out into the distance to make beam look longer
		   if(i == numSamples-1){
		   	x+=400;
		   }
		   
		   //Get spectrum data for current index. Multiply in scale and volume.
		   var y : float = spectrum[i]*rmsValue*2*yScale;
		   
		   //Transform y value towards target
		   fMax[i] = Mathf.Max(fMax[i], y); //Same as above we want the highwest point, so wither the current target or the new data
		   
		   //Transform towards target
		   if (fCur[i] > fMax[i]){
		   	fCur[i] = Mathf.Clamp(fCur[i] - Time.deltaTime*60*rmsValue*5, fMax[i], fCur[i]);
		   } else {
		  	fCur[i] = Mathf.Clamp(fCur[i] + Time.deltaTime*100*rmsValue*5, fCur[i] , fMax[i]);
		   }
		   
		   fMax[i] -= Time.deltaTime*60; //Lower our max over time
		   y = fCur[i]; //Set the y value
		   
		   
		   // Apply Sine Wave
		   var seg : float = numSamples; //Convert to float for division
		   var segval : float = (sineFade-i)/sineFade; //How far are we from center?
		   segval = Mathf.Max(segval,.15); //Minimum value
		   
		   var y2 = Mathf.Sin(x+Time.time*speedOffset)*(segval*sc); //Get sine based on multiplier    
		   y+= y2; //Add sine value to existing value

		   //Apply calculated position to current point in Linerenderer
		   for(var l : LineRenderer in renderers){
           	l.SetPosition (i,new Vector3(x ,y,0));
           }
	}
	for(var l : LineRenderer in renderers){
		l.gameObject.renderer.material.mainTextureOffset.x-=Time.deltaTime*20*mCur; //scroll material over time
	}
}