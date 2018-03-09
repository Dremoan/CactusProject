using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatInSand : MonoBehaviour {

	public GameObject player;
	public Animator anim;

	private Vector2 launchDir;
	private Vector3 mousePos;
	private float jumpingForce = 0f;
	private bool isInSand = false;

	void Start () 
	{
		
	}

	void Update () 
	{
		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if(player.GetComponent<PlayerBehavior>().isInteracting)
		{
//			GetComponent<CircleCollider2D> ().enabled = false;
			player.GetComponent<Rigidbody2D> ().position = transform.position;
			player.GetComponent<PlayerBehavior> ().canMove = false;
			player.GetComponent<PlayerBehavior> ().canJump = false;
			isInSand = true;

			if(Input.GetKey(KeyCode.Space) && isInSand)
			{
				launchDir = mousePos - transform.position;
				jumpingForce ++;
			}

			if (jumpingForce >= 100) 
			{
				jumpingForce = 100f;
			}

			if(Input.GetKeyUp(KeyCode.Space))
			{
				StartCoroutine (LaunchingCactus ());
//				GetComponent<CircleCollider2D> ().enabled = true;
			}
		}
	}

	IEnumerator LaunchingCactus()
	{
		player.GetComponent<PlayerBehavior> ().isInteracting = false;
		player.GetComponent<Rigidbody2D> ().AddForce (-launchDir * jumpingForce* Time.deltaTime * 5, ForceMode2D.Impulse);
		jumpingForce = 0f;
		yield return new WaitForSeconds (0.5f);
		player.GetComponent<PlayerBehavior> ().canMove = true;
		player.GetComponent<PlayerBehavior> ().canJump = true;
	}
}

//			anim.SetBool ("IsOnGoat", isOnGoat);