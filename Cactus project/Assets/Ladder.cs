﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour 
{
	public GameObject player;
	public GameObject text;
	public GameObject ladderPos2;
	public GameObject goingUp;

	private bool canInteract;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(this.GetComponent<interactingScript>().canInteract && goingUp.GetComponent<GoingUp>().isHigh)
		{
			text.SetActive (true);
		} 
		else
		{
			text.SetActive (false);
		}
		if(this.GetComponent<interactingScript>().canInteract && player.GetComponent<PlayerBehavior>().pressingA)
		{
			player.transform.Translate (ladderPos2.transform.position - this.transform.position);
		}
	}


}
