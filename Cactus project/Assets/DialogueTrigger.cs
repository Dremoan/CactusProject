using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {


	public GameObject interactingZone;
	public Dialogue dialogue;

	void Update()
	{
		if(interactingZone.GetComponent<InteractingScript>().canInteract)
		{
			TriggerDialogue ();
		}
	}
	public void TriggerDialogue()
	{
		FindObjectOfType<DialogueManager> ().StartDialogue (dialogue);
	}
}
