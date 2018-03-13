using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSand : MonoBehaviour 
{
	public GameObject goat;
	public GameObject goatInSand;
	public Animator anim;
	private bool isInSand = false;

	void Start () 
	{
		
	}
	

	void Update () 
	{

		if(isInSand)
		{
			goat.SetActive (false);
			goatInSand.SetActive (true);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Goat")
		{
			isInSand = true;
		}
	}





}
