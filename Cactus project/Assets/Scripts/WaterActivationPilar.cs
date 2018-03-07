using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterActivationPilar : MonoBehaviour {

	public GameObject pilar;
	public GameObject flower;
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
		if(pilar.GetComponent<WaterActivationPilar>().isActive == true && rockSlot.GetComponent<WaterActivationRock>().isActive == true)
		{
			waterSource.SetActive(true);
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.Equals(flower))
		{
			isActive = true;
		}
	}
}
