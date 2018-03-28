using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchFlower : MonoBehaviour {

	public GameObject player;
	public GameObject liane;
	public GameObject flowerTarget;
	public Animator animFlower;
	public Transform flowerPlace;
	public Transform lianePlace;
	public Rigidbody2D bodyFlower;
	public Rigidbody2D bodyPlayer;
	public float flowerSpeed = 10f;
	public float flowerSpeedBack = 20f;
	public float maxDistanceToFlower = 1f;
	public float hookTime = 2f;

	private Vector2 mousePos;
	private GameObject hookedThing;
	[HideInInspector] public bool isLaunched = false;
	private bool lianeActive = false;
	private bool isBacking = false;
	private bool isHooked = false;
	private bool onWater = false;
	public bool canLaunch = true;
	public bool hitGoat = false;
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

		if(onWater && player.GetComponent<PlayerBehavior>().pressingA)
		{
			holdsWater = true;
			isHooked = false;
			isBacking = true;
		}

		Launch ();
		Animations ();
	}





	public void Launch()
	{
		if (lianeActive) 
		{
			StartCoroutine (lianeSprite ());
		}
			
		if(Input.GetMouseButtonDown(0) && !isLaunched && !isBacking && !isHooked && canLaunch)
		{
			this.GetComponent<CircleCollider2D> ().enabled = true;
			liane.SetActive (true);
			lianeActive = true;
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

	IEnumerator lianeSprite()
	{
		yield return new WaitForSeconds (0.05f);
		Vector2 dirToFlower = lianePlace.transform.position - transform.position;
		float rot_Z = Mathf.Atan2 (dirToFlower.y, dirToFlower.x) * Mathf.Rad2Deg;
		liane.transform.rotation = Quaternion.Euler (0f, 0f, rot_Z - 90f);
		liane.GetComponent<SpriteRenderer> ().size = new Vector2 (3, Vector2.Distance (lianePlace.transform.position, transform.position));
	}



	public void Back()
	{
		Vector2 dirToPlace = flowerPlace.transform.position - transform.position;
		bodyFlower.velocity = dirToPlace.normalized * flowerSpeedBack * Time.fixedDeltaTime;
		if(Vector2.Distance(player.transform.position, transform.position)< 10f)
		{
			this.GetComponent<CircleCollider2D> ().enabled = false;
			liane.SetActive (false);
			lianeActive = false;
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
			onWater = true;
		}
		if(coll.gameObject.tag == "GoatInSand")
		{
			coll.gameObject.GetComponent<GoatInSand> ().isCharging = true;
			lianeActive = false;
			hitGoat = true;
			isBacking = false;
			isHooked = false;
			isLaunched = false;
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
