using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ButtonScript : MonoBehaviour {

	public GameObject player;
	public GameObject interrupteur1;
	public GameObject interrupteur2;
	public GameObject keyRock;
	public bool isActive;

	void Start () 
	{
		
	}

	void Update ()
	{
		if(interrupteur1.GetComponent<ButtonScript>().isActive && interrupteur2.GetComponent<ButtonScript>().isActive)
		{
			keyRock.SetActive (true);
		}
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.Equals(player))
		{
			isActive = true;
		}
	}
}
