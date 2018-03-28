using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

	public GameObject interactingZone;
	public GameObject player;
	public GameObject key;

	private bool isNearDoor;


		void Start () 
	{
		
	}



	void Update () 
	{
		if(interactingZone.GetComponent<InteractingScript>().canInteract)
		{
			isNearDoor = true;
		}
		else if(interactingZone.GetComponent<InteractingScript>().canInteract == false)
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
		player.GetComponent<PlayerBehavior> ().canJump = false;
		player.GetComponent<PlayerBehavior> ().isMoving = false;
		yield return new WaitForSeconds (1);
		player.GetComponent<PlayerBehavior> ().canJump = true;
		key.SetActive (false);
		player.GetComponent<PlayerBehavior> ().hasKey = false;
		this.gameObject.SetActive (false);
	}
}
