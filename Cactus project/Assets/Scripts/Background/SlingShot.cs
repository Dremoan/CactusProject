using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour {

	public GoatInSand goatScript;
	public GameObject flower;
	public LineRenderer lianeRend;


	void Start () 
	{
		
	}


	void Update () 
	{
		if(goatScript.GetComponent<GoatInSand>().isCharging)
		{
			DrawLine ();
		}
		if(goatScript.GetComponent<GoatInSand>().isCharging == false)
		{
			lianeRend.enabled = false;
		}
	}

	void DrawLine()
	{
		lianeRend.enabled = true;
		lianeRend.SetPosition (0, transform.position);
		lianeRend.SetPosition (1, flower.transform.position);
	}
}
