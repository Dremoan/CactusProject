using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterActivation : MonoBehaviour {

	public GameObject rockSlot;
	public GameObject waterSource;
	public bool isActive = false;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(rockSlot.GetComponent<WaterActivationRock>().isActive == true)
		{
			waterSource.SetActive(true);
		}
	}
}