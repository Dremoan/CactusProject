using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatInSand : MonoBehaviour {

	public GameObject player;
	public Animator anim;
	public Collider2D interactingZone;
	public GameObject interactZone;

	public GameObject upLeft;
	public GameObject up;
	public GameObject upRight;
	public GameObject right;
	public GameObject downRight;
	public GameObject down;
	public GameObject downLeft;
	public GameObject left;

	public float jumpMultiplier = 2000f;
	public bool inTheAir;

	private float launchAngle;
	private Vector3 launchDir;
	private Vector3 mousePos;
	private float jumpingForce = 0f;
	private bool inSandZone = false;

	void Start () 
	{
		
	}

	void Update () 
	{
		Debug.Log (launchAngle);
		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		if(interactZone.GetComponent<interactingScript>().canInteract && player.GetComponent<PlayerBehavior>().pressingA == true)
		{
			inSandZone = true;
		}

		if(inSandZone)
		{
			interactingZone.enabled = false;
			player.GetComponent<PlayerBehavior> ().canMove = false;
			player.GetComponent<PlayerBehavior> ().canJump = false;
			LaunchingPos ();
		}

		if(Input.GetKey(KeyCode.Space) && inSandZone)
		{
			launchAngle = Mathf.Atan2 (launchDir.x, launchDir.y) * Mathf.Rad2Deg;
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

	void LaunchingPos()
	{
		if(-180<=launchAngle<-120)
		{
			player.transform.position = downLeft.transform.position;
		}
		else if(-120<=launchAngle<-60)
		{
			player.transform.position = left.transform.position;
		}
		else if(-60<=launchAngle<0)
		{
			player.transform.position = upLeft.transform.position;
		}
		else if(-22.5f<= launchAngle <= 0 || 0<=launchAngle<22.5f)
		{
			player.transform.position = up.transform.position;
		}
		else if(0<=launchAngle<60)
		{
			player.transform.position = up.transform.position;
		}
		else if(60<=launchAngle<120)
		{

		}
		else if(120<=launchAngle<180)
		{

		}
	}
}

