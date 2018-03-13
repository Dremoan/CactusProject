using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingThru : MonoBehaviour {


	public GameObject player;
	[HideInInspector] public bool isOnEdge;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isOnEdge && player.GetComponent<PlayerBehavior>().isJumping)
		{
			StartCoroutine (InactiveCollider ());
		}
	}

	IEnumerator InactiveCollider()
	{
		this.GetComponent<BoxCollider2D> ().enabled = false;
		yield return new WaitForSeconds (0.75f);
		this.GetComponent<BoxCollider2D> ().enabled = true;
	}
		

	void OnCollisionStay2D(Collision2D coll)
	{
		isOnEdge = true;
	}
}
