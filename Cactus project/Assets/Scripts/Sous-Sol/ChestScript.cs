using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour {

	public GameObject keyOnScreen;
	public GameObject player;
	public GameObject interactingZone;
	public GameObject key;
	public Collider2D blockingWay;

	private bool isNearChest = false;
	private bool isOpened = false;

	void Start () 
	{
		
	}

	void Update () 
	{

		if(interactingZone.GetComponent<InteractingScript>().canInteract)
		{
			isNearChest = true;
		}
		else if(interactingZone.GetComponent<InteractingScript>().canInteract == false)
		{
			isNearChest = false;
		}

		if(isNearChest && player.GetComponent<PlayerBehavior>().pressingA)
		{
			keyOnScreen.SetActive (true);
			player.GetComponent<PlayerBehavior> ().hasKey = true;
			isOpened = true;
		}
		if(isOpened)
		{
			key.SetActive (false);
			blockingWay.enabled = false;
		}
	}
}
