using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatInSand : MonoBehaviour {

	public GameObject player;
	public Animator animPlayer;
	public Animator animGoat;
	public Collider2D interactingZone;
	public GameObject interactZone;

	public float jumpMultiplier = 2000f;
	public bool inTheAir;
	public bool isCharging;

	private float launchAngle;
	private Vector3 launchDir;
	private float jumpingForce = 0f;
	private bool isLaunching;
	private bool inSandZone = false;

	void Start () 
	{
		
	}

	void Update () 
	{
		Debug.Log (inTheAir);
		launchAngle = Mathf.Atan2 (launchDir.x, launchDir.y) * Mathf.Rad2Deg + 180;
		animPlayer.SetBool ("IsCharging", isCharging);
		animPlayer.SetFloat ("LaunchingAngle", launchAngle);
		animPlayer.SetBool ("IsLaunching", isLaunching);
		animPlayer.SetBool ("InTheAir", inTheAir);
		animGoat.SetBool ("InTheAir", inTheAir);

		if (interactZone.GetComponent<interactingScript> ().canInteract) 
		{
			inSandZone = true;
			player.GetComponent<PlayerBehavior> ().canCharge= true;
		} 
		else if (interactZone.GetComponent<interactingScript> ().canInteract == false) 
		{
			inSandZone = false;
			player.GetComponent<PlayerBehavior> ().canCharge = false;
		}


		if(Input.GetKey(KeyCode.Space) && inSandZone)
		{
			isCharging = true;
		}

		if(isCharging)
		{
			StartCoroutine (Charging ());
		}
		if (jumpingForce >= 100) 
		{
			jumpingForce = 100f;
		}

		if(Input.GetKeyUp(KeyCode.Space) && isCharging)
		{
			StartCoroutine (LaunchingCactus ());
		}

	}

	IEnumerator LaunchingCactus()
	{
		inSandZone = false;
		isCharging = false;
		inSandZone = false;
		player.GetComponent<Rigidbody2D> ().AddForce (launchDir.normalized * jumpingForce* Time.deltaTime * jumpMultiplier, ForceMode2D.Impulse);
		jumpingForce = 0f;
		inTheAir = true;
		yield return new WaitForSeconds (0.25f);
		player.GetComponent<Collider2D> ().enabled = true;
		yield return new WaitForSeconds (0.25f);
		player.GetComponent<PlayerBehavior> ().canJump = true;
		inTheAir = false;
	}

	IEnumerator Charging()
	{
		player.GetComponent<PlayerBehavior> ().canJump = false;
		launchDir = transform.position - player.transform.position;
		jumpingForce ++;
		player.GetComponent<Collider2D> ().enabled = false;
		interactingZone.enabled =false;
		yield return new WaitForSeconds (1f);
		interactingZone.enabled = true;
	}
}

