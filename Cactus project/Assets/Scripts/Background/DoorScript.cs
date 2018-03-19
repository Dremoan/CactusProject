using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

	public GameObject interactingZone;
	public GameObject player;

	private bool isNearDoor;


		void Start () 
	{
		
	}



	void Update () 
	{
		if(interactingZone.GetComponent<interactingScript>().canInteract)
		{
			isNearDoor = true;
		}
		else if(interactingZone.GetComponent<interactingScript>().canInteract = false)
		{
			isNearDoor = true;
		}

		if(isNearDoor && player.GetComponent<PlayerBehavior>().pressingA && player.GetComponent<PlayerBehavior> ().hasKey)
		{
			StartCoroutine (ActiveDoor ());
		}
	}

	IEnumerator ActiveDoor()
	{
		player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		yield return new WaitForSeconds (1);
		this.gameObject.SetActive (false);
	}
}
