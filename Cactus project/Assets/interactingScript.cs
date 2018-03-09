using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactingScript : MonoBehaviour {


	public bool canInteract = false;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			canInteract = true;
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			canInteract = false;
		}
	}
}
