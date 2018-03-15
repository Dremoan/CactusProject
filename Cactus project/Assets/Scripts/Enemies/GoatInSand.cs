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
	public bool isCharging;

	private float launchAngle;
	private Vector3 launchDir;
	private float jumpingForce = 0f;
	private bool inSandZone = false;

	void Start () 
	{
		
	}

	void Update () 
	{
		anim.SetBool ("IsCharging", isCharging);
		Debug.Log ("inSandZone" + inSandZone);

		if (interactZone.GetComponent<interactingScript> ().canInteract) {
			inSandZone = true;
		} 
//		else if (interactZone.GetComponent<interactingScript> ().canInteract == false) 
//		{
//			inSandZone = false;
//		}


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

		if(Input.GetKeyUp(KeyCode.Space))
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
		launchAngle = Mathf.Atan2 (launchDir.x, launchDir.y) * Mathf.Rad2Deg;
		launchDir = transform.position - player.transform.position;
		jumpingForce ++;
		player.GetComponent<Collider2D> ().enabled = false;
		interactingZone.enabled =false;
		yield return new WaitForSeconds (1f);
		interactingZone.enabled = true;
	}
}

