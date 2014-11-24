#pragma strict
var style:GUIStyle;
var boxWidth:int = Screen.width/4;
var boxHeight:int = Screen.height/5;
var boxMiddle:int = Screen.width/8;
var aTexture:RenderTexture;
var hits:int = 0;
var song:String = "Song";
var songLength:int;
var timeM:int = 0;
var timeS:int = 0;
var timeSS:int = 0;
var tick:boolean = false;
public var audioClip:AudioClip;

function Start () {
	//this.gameObject.GetComponent(AudioSource).Play(audioClip);
	song = audioClip.name;
	songLength = audioClip.length;
}

function Update () 
{
	if(timeSS >= 10)
	{
		timeS++;
		timeSS = 0;
	}
	if(timeS >= 6)
	{
		timeM++;
		timeS = 0;
	}
	
	if(!tick)
	{
		tick = true;
		Tick();
	}
}

function Tick()
{
	yield WaitForSeconds(1);
	timeSS++;
	tick = false;
}

function OnGUI()
{
	GUI.BeginGroup(Rect(Screen.width - Screen.width/4 - 2,2,boxWidth,boxHeight));
		GUILayout.Box("");
		//GUILayout.BeginHorizontal();
			GUILayout.BeginVertical();
				GUILayout.Label("Song: " + song,style);
				GUILayout.Label("Time: " + timeM + ":" + timeS + timeSS + " / " + songLength/60 + ":" + songLength%60,style);
				GUILayout.Label("Hits: " + hits,style);
			GUILayout.EndVertical();
			//GUILayout.BeginVertical();
			//	GUILayout.Box(aTexture);
			//GUILayout.EndVertical();
		
		//GUILayout.EndHorizontal();
	GUI.EndGroup();

}

function OnTriggerEnter(collider:Collider)
{	
	if(collider.gameObject.tag == "column")
		hits++;
}