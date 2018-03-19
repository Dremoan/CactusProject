using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour {

	public GameObject keyOnScreen;
	public GameObject player;
	public GameObject interactingZone;
	public Animator chestAnim;

	private bool isNearChest = false;
	private bool isOpened = false;

	void Start () 
	{
		
	}

	void Update () 
	{
		chestAnim.SetBool ("IsOpened", isOpened);

		if(interactingZone.GetComponent<interactingScript>().canInteract)
		{
			isNearChest = true;
		}
		else if(interactingZone.GetComponent<interactingScript>().canInteract == false)
		{
			isNearChest = false;
		}

		if(isNearChest && player.GetComponent<PlayerBehavior>().pressingA)
		{
			keyOnScreen.SetActive (true);
			player.GetComponent<PlayerBehavior> ().hasKey = true;
			isOpened = true;
		}
	}
}
