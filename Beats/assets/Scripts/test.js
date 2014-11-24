var blocks:GameObject[];
var sample:float[] = new float[512];
var values:float[] = new float[8];


function Start () {
}

function Update () {

	var count:int = 0;
	var diff:float = 0;
	
	AudioListener.GetSpectrumData(sample, 0, FFTWindow.BlackmanHarris);
	for (var i = 0; i < 8; i++)
	{
		var average:float = 0;
		var sampleCount:int = Mathf.Pow(2,i)*2;
		
		for(var j:int = 0; j < sampleCount; j++)
		{
			average += sample[count]*(count + 1);
			count++;
		}
		
		average/=count;
		
		
		//diff = Mathf.Clamp(average * 10 - values[i%8], 0, 4);
		
		values[i] =  Mathf.Clamp(average * 20, 1,8);
		var script:EQColumn;
		script = blocks[i].gameObject.GetComponent(EQColumn);
		script.updateBlocks(values[i]);
		
	}
}