#pragma strict
var speed : float = 22;

function Update () {
	//Just rotate around randomly
	transform.localEulerAngles+= Vector3(0, Random.value*speed*Time.deltaTime, Random.value*speed*Time.deltaTime);
}