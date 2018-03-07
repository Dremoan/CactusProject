using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchFlower : MonoBehaviour {

	public GameObject player;
	public Transform flowerPlace;
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
	}


	public void Launch()
	{
		lianeRend.SetPosition (0, transform.position);
		lianeRend.SetPosition (1, flowerPlace.transform.position);
		if(Input.GetMouseButtonDown(0) && !isLaunched && !isBacking && !isHooked)
		{
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
		if(coll.gameObject.tag == "Activation" && isLaunched && holdsWater == false)
		{
			isHooked = true;
			hookedThing = coll.gameObject;
			StartCoroutine (hookDelay ());
		}
	}
}
