#pragma strict

public var speed:int;

function Start () {

}

function Update () 
{
	this.transform.Rotate(Vector3(0,0,speed * Time.deltaTime));
}