using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingThru : MonoBehaviour {


	public GameObject player;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(player.GetComponent<PlayerBehavior>().isJumping)
		{
			StartCoroutine (InactiveCollider ());
		}
	}

	IEnumerator InactiveCollider()
	{
		Physics2D.IgnoreCollision (player.GetComponent<Collider2D> (), this.GetComponent<Collider2D> (), ignore:true);
		yield return new WaitForSeconds (1f);
		Physics2D.IgnoreCollision (player.GetComponent<Collider2D> (), this.GetComponent<Collider2D> (), ignore:false);
	}
}
