using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatInSand : MonoBehaviour {

	public GameObject player;
	public Animator anim;
	public float jumpingForce = 0f;

	private bool isInSand = false;
	private Vector2 launchDir;

	void Start () 
	{
		
	}

	void Update () 
	{
		Debug.Log (jumpingForce);
		Debug.Log (launchDir);
		if(player.GetComponent<PlayerBehavior>().isInteracting)
		{
			launchDir = Camera.main.ScreenToWorldPoint (Input.mousePosition) - player.transform.position;
			player.GetComponent<Rigidbody2D> ().position = transform.position;
			player.GetComponent<PlayerBehavior> ().canMove = false;
			player.GetComponent<PlayerBehavior> ().canJump = false;
			isInSand = true;

			if(Input.GetKey(KeyCode.Space) && isInSand)
			{
				jumpingForce++;
			}

			if (jumpingForce >= 100) 
			{
				jumpingForce = 100f;
			}

			if(Input.GetKeyUp(KeyCode.Space))
			{
				StartCoroutine (LaunchingCactus ());
			}
		}
	}

	IEnumerator LaunchingCactus()
	{
		player.GetComponent<Rigidbody2D> ().AddForce (-launchDir.normalized * jumpingForce * 10f, ForceMode2D.Impulse);
		player.GetComponent<PlayerBehavior> ().isInteracting = false;
		jumpingForce = 0f;
		yield return new WaitForSeconds (0.5f);
		player.GetComponent<PlayerBehavior> ().canMove = true;
		player.GetComponent<PlayerBehavior> ().canJump = true;
	}
}

//			anim.SetBool ("IsOnGoat", isOnGoat);