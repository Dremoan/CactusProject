using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSand : MonoBehaviour 
{
	public GameObject goat;
	public GameObject goatInSand;
	public Animator anim;
	private Vector2 dirToCenter;
	private bool isInSand = false;

	void Start () 
	{
		
	}
	

	void Update () 
	{
		dirToCenter = goat.transform.position - transform.position;
//		anim.SetBool ("isInSand", isInSand);

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
