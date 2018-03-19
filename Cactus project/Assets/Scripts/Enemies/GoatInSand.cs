using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatInSand : MonoBehaviour {

	public GameObject player;
	public Animator animPlayer;
	public Animator animGoatInSand;
	public Collider2D interactingZone;
	public GameObject interactZone;
	public GameObject[] lianeOnGoatPoints;

	[HideInInspector] public float launchAngle;
	[HideInInspector] public float goatDirAngle;
	public float jumpMultiplier = 2000f;
	public bool inTheAir;
	public bool isCharging;

	private Vector3 launchDir;
	private float jumpingForce = 0f;
	private bool isLaunching;
	private bool isNearGoat = false;

	void Start () 
	{
		
	}

	void Update () 
	{
		
		ChoosingAnchorPoint ();



		launchDir = transform.position - player.transform.position;
		goatDirAngle = Mathf.Atan2 (launchDir.x, launchDir.y) * Mathf.Rad2Deg + 180;




		animPlayer.SetBool ("IsCharging", isCharging);
		animPlayer.SetFloat ("LaunchingAngle", launchAngle);
		animPlayer.SetBool ("InTheAir", inTheAir);




		animGoatInSand.SetBool ("InTheAir", inTheAir);
		animGoatInSand.SetBool("IsNearGoat", isNearGoat);
		animGoatInSand.SetFloat("GoatDirAngle", goatDirAngle);

		if (interactZone.GetComponent<interactingScript> ().canInteract) 
		{
			isNearGoat = true;
			player.GetComponent<PlayerBehavior> ().canCharge= true;
		} 
		else if (interactZone.GetComponent<interactingScript> ().canInteract == false) 
		{
			isNearGoat = false;
			player.GetComponent<PlayerBehavior> ().canCharge = false;
		}


		if(Input.GetKey(KeyCode.Space) && isNearGoat)
		{
			launchAngle = Mathf.Atan2 (launchDir.x, launchDir.y) * Mathf.Rad2Deg + 180;
			player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
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
		isNearGoat = false;
		isCharging = false;
		interactingZone.enabled = false;
		player.GetComponent<Rigidbody2D> ().AddForce (launchDir.normalized * jumpingForce* Time.deltaTime * jumpMultiplier, ForceMode2D.Impulse);
		jumpingForce = 0f;
		inTheAir = true;
		yield return new WaitForSeconds (0.25f);
		player.GetComponent<Collider2D> ().enabled = true;
		yield return new WaitForSeconds (0.25f);
		player.GetComponent<PlayerBehavior> ().canJump = true;
		interactingZone.enabled = true;
		inTheAir = false;
	}

	IEnumerator Charging()
	{
		player.GetComponent<PlayerBehavior> ().canJump = false;
		jumpingForce ++;
		player.GetComponent<Collider2D> ().enabled = false;
		interactingZone.enabled =false;
		yield return new WaitForSeconds (1f);
		interactingZone.enabled = true;
	}

	void ChoosingAnchorPoint()
	{
		if(launchAngle > 0 && launchAngle < 90)
		{
			lianeOnGoatPoints [0].SetActive (true);
			lianeOnGoatPoints [1].SetActive (true);
			lianeOnGoatPoints [2].SetActive (false);
			lianeOnGoatPoints [3].SetActive (false);
			lianeOnGoatPoints [4].SetActive (false);
			lianeOnGoatPoints [5].SetActive (false);
			lianeOnGoatPoints [6].SetActive (false);
			lianeOnGoatPoints [7].SetActive (false);
		}
		else if(launchAngle > 90 && launchAngle < 180)
		{
			lianeOnGoatPoints [0].SetActive (false);
			lianeOnGoatPoints [1].SetActive (false);
			lianeOnGoatPoints [2].SetActive (true);
			lianeOnGoatPoints [3].SetActive (true);
			lianeOnGoatPoints [4].SetActive (false);
			lianeOnGoatPoints [5].SetActive (false);
			lianeOnGoatPoints [6].SetActive (false);
			lianeOnGoatPoints [7].SetActive (false);
		}
		else if(launchAngle > 180 && launchAngle < 270)
		{
			lianeOnGoatPoints [0].SetActive (false);
			lianeOnGoatPoints [1].SetActive (false);
			lianeOnGoatPoints [2].SetActive (false);
			lianeOnGoatPoints [3].SetActive (false);
			lianeOnGoatPoints [4].SetActive (true);
			lianeOnGoatPoints [5].SetActive (true);
			lianeOnGoatPoints [6].SetActive (false);
			lianeOnGoatPoints [7].SetActive (false);
		}
		else if(launchAngle > 270 && launchAngle < 360)
		{
			lianeOnGoatPoints [0].SetActive (false);
			lianeOnGoatPoints [1].SetActive (false);
			lianeOnGoatPoints [2].SetActive (false);
			lianeOnGoatPoints [3].SetActive (false);
			lianeOnGoatPoints [4].SetActive (false);
			lianeOnGoatPoints [5].SetActive (false);
			lianeOnGoatPoints [6].SetActive (true);
			lianeOnGoatPoints [7].SetActive (true);
		}
	}
}

