using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiding : MonoBehaviour {

	public GameObject[] hidingZones;

	void Start () 
	{
		
	}
	

	void Update () 
	{
	
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			hidingZones [0].SetActive (true);
			hidingZones [1].SetActive (true);
			hidingZones [2].SetActive (false);
			hidingZones [3].SetActive (false);
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			hidingZones [0].SetActive (false);
			hidingZones [1].SetActive (false);
			hidingZones [2].SetActive (true);
			hidingZones [3].SetActive (true);
		}
	}
}
