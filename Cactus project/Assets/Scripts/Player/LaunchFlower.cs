using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchFlower : MonoBehaviour {

	public GameObject player;
	public Animator animFlower;
	public Transform flowerPlace;
	public Transform lianePlace;
	public Rigidbody2D bodyFlower;
	public Rigidbody2D bodyPlayer;
	public LineRenderer lianeRend;
	public float flowerSpeed = 10f;
	public float flowerSpeedBack = 20f;
	public float maxDistanceToFlower = 1f;
	public float hookTime = 2f;

	private Vector2 mousePos;
	private GameObject hookedThing;
	private bool isLaunched = false;
	private bool isBacking = false;
	private bool isHooked = false;
	public bool holdsWater = false;

	void Update () 
	{
		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;

		if(!isLaunched && !isBacking && !isHooked)
		{
			transform.position = flowerPlace.position;
		}

		if(isBacking)
		{
			Back ();
		}

		if(maxDistanceToFlower < Vector2.Distance(player.transform.position, transform.position))
		{
			isLaunched = false;
			isBacking = true;
		}
		if(hookedThing!= null && isHooked)
		{
			HookEnemy (hookedThing);
		}

		if(Vector2.Distance(player.transform.position, transform.position) < maxDistanceToFlower && isHooked)
		{
			isLaunched = false;
			isBacking = true;
		}

		Launch ();
		Animations ();
	}


	public void Launch()
	{
		lianeRend.SetPosition (0, transform.position);
		lianeRend.SetPosition (1, lianePlace.transform.position);
		if(Input.GetMouseButtonDown(0) && !isLaunched && !isBacking && !isHooked)
		{
			lianeRend.enabled = true;
			bodyFlower.velocity = mousePos.normalized * flowerSpeed * Time.fixedDeltaTime;
			isLaunched = true;
		}
		if (maxDistanceToFlower < Vector2.Distance (player.transform.position, transform.position))
			{
				isLaunched = false;
				isBacking = true;
			}
	}

	IEnumerator hookDelay()
	{
		yield return new WaitForSeconds (hookTime);
		isHooked = false;
		isBacking = true;
	}


	public void Back()
	{
		Vector2 dirToPlace = flowerPlace.transform.position - transform.position;
		bodyFlower.velocity = dirToPlace.normalized * flowerSpeedBack * Time.fixedDeltaTime;
		if(Vector2.Distance(player.transform.position, transform.position)< 10f)
		{
			lianeRend.enabled = false;
			isBacking = false;
			isHooked = false;
			isLaunched = false;
		}
	}

	public void HookEnemy(GameObject hookThing)
	{
		transform.position = hookThing.transform.position;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "WaterSource" && isLaunched && holdsWater == false)
		{
			isHooked = true;
			hookedThing = coll.gameObject;
			StartCoroutine (hookDelay ());
			holdsWater = true;
		}
	}

	void Animations()
	{
		animFlower.SetFloat ("Horizontal", Input.GetAxisRaw ("Horizontal"));
		animFlower.SetFloat ("Vertical", Input.GetAxisRaw ("Vertical"));
		animFlower.SetFloat ("LastMoveX", player.GetComponent<PlayerBehavior> ().lastMove.x);
		animFlower.SetFloat ("LastMoveY", player.GetComponent<PlayerBehavior> ().lastMove.y);
		animFlower.SetBool ("IsMoving", player.GetComponent<PlayerBehavior>().isMoving);
	}
}
