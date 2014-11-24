#pragma strict

//Store colors for lerping
private var startColor1 : Color;
private var endColor1 : Color;
private var startColor2 : Color;
private var endColor2 : Color;

//Arrays of colors (there are two so that you can have multiple colors at once)
var colors1 : Color[];
var colors2 : Color[];

//Arrays of renderers
var renderers1 : Renderer[];
var renderers2 : Renderer[];

var fadeTime : float;//How long does it take to transition between colors
var baseTransitionTime : float; //base minimum time between transitions
var transitionTimeVariance : float; //random variance in time (value between 0 and this will be added to base)
private var i : int = 0; //current color index;
private var nextTransition : float; //time at which next transition begins


function Start() {
	//Initialize start colors and counter
	startColor1 = colors1[0];
	startColor2 = colors2[0];
	endColor1 = colors1[1];
	endColor2 = colors2[1];
	i = 1;
	//Initialize next transition time
	nextTransition = Time.time+baseTransitionTime + Random.value*transitionTimeVariance;
}

function Update () {

	//Lerp from startColor to endColor for each set of renderers
	for(var r : Renderer in renderers1){
		r.material.SetColor("_Emission", Color.Lerp(startColor1, endColor1, (Time.time-nextTransition)/fadeTime));
	}
	for(var r : Renderer in renderers2){
		r.material.SetColor("_Emission", Color.Lerp(startColor2, endColor2, (Time.time-nextTransition)/fadeTime));
	}
	
	//If we've hit the transition time
	if(Time.time > nextTransition+fadeTime){
		//Cycle through our colors array
		startColor1 = colors1[i];
		startColor2 = colors2[i];
		i++;
		if(i >= colors1.Length)
			i = 0;
		endColor1 = colors1[i];
		endColor2 = colors2[i];
		
		//Set the next transition time
		nextTransition = Time.time+baseTransitionTime + Random.value*transitionTimeVariance;
		
	}
}