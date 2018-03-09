using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatInSand : MonoBehaviour {

	public GameObject player;
	public Animator anim;
	public Collider2D interactingZone;
	public GameObject interactZone;
	public float jumpMultiplier = 2000f;
	public bool inTheAir;
	
	private Vector3 launchDir;
	private Vector3 mousePos;
	private float jumpingForce = 0f;
	private bool inSandZone = false;

	void Start () 
	{
		
	}

	void Update () 
	{
		Debug.DrawLine (player.transform.position, mousePos, Color.red);
		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		if(interactZone.GetComponent<interactingScript>().canInteract && player.GetComponent<PlayerBehavior>().pressingA == true)
		{
			inSandZone = true;
		}

		if(inSandZone)
		{
			interactingZone.enabled = false;
			player.GetComponent<Rigidbody2D> ().position = transform.position;
			player.GetComponent<PlayerBehavior> ().canMove = false;
			player.GetComponent<PlayerBehavior> ().canJump = false;
		}

		if(Input.GetKey(KeyCode.Space) && inSandZone)
		{
			launchDir = mousePos - player.transform.position;
			jumpingForce ++;
			player.GetComponent<Collider2D> ().enabled = false;
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

	IEnumerator LaunchingCactus()
	{
		inSandZone = false;
		player.GetComponent<Rigidbody2D> ().AddForce (-launchDir.normalized * jumpingForce* Time.deltaTime * jumpMultiplier, ForceMode2D.Impulse);
		jumpingForce = 0f;
		inTheAir = true;
		yield return new WaitForSeconds (0.25f);
		player.GetComponent<Collider2D> ().enabled = true;
		yield return new WaitForSeconds (0.25f);
		interactingZone.enabled = true;
		player.GetComponent<PlayerBehavior> ().canMove = true;
		player.GetComponent<PlayerBehavior> ().canJump = true;
		inTheAir = false;
	}

}

