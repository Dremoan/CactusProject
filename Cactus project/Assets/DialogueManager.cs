using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {


	private Queue<string> sentences;



	void Start () 
	{
		sentences = new Queue<string> ();
	}

	public void StartDialogue(Dialogue dialogue)
	{

		sentences.Clear ();

		foreach(string sentence in dialogue.sentences)
		{
			sentences.Enqueue (sentence);
		}

		if (FindObjectOfType<PlayerBehavior> ().pressingA) 
		{
			DisplayNextSentence ();
		}
	}


	public void DisplayNextSentence()
	{
		if(sentences.Count == 0)
		{
			EndDialogue ();
			return;
		}

		string sentence = sentences.Dequeue ();
		Debug.Log (sentence);
	}

	void EndDialogue()
	{
		Debug.Log ("End of conversation");
	}

}
