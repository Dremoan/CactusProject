using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatBehaviour : MonoBehaviour {

	public Transform[] patrolPoints;
	public Transform target;
	public Rigidbody2D body;
	public Rigidbody2D playerBody;
	public PlayerBehavior playerScript;
	public Animator ani;
	public float speed = 5f;
	public float chargingSpeed = 10f;
	public float waitTime = 1f;
	public float ejectSpeed;
	public float ejectingTime;
	public bool isMoving = true;

	private Transform currentPatrolPoint;
	private int currentPatrolIndex;
	private bool targetAcquired = false;
	private bool touchedPlayer = false;

	// Use this for initialization
	void Start () 
	{
		currentPatrolIndex = 0;
		currentPatrolPoint = patrolPoints [currentPatrolIndex];
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!targetAcquired)
		{
			Patrol ();
		}
		Animations ();

		if(body.velocity.x == 0 && body.velocity.y == 0)
		{
			isMoving = false;
		} else {
			isMoving = true;
		}
	}

	private void Animations()
	{
		ani.SetFloat("VelX", body.velocity.x);
		ani.SetFloat("VelY", body.velocity.y);
		ani.SetBool ("isMoving", isMoving);
	}

	private void Patrol()
	{
		if(Vector3.Distance(transform.position, currentPatrolPoint.position) < 1f)
		{
			if(currentPatrolIndex +1 < patrolPoints.Length)
			{
				currentPatrolIndex++;
			} else {
				currentPatrolIndex = 0;
			}
			currentPatrolPoint = patrolPoints [currentPatrolIndex];
		}

		Vector3 dirToNextPoint = currentPatrolPoint.position - transform.position;
		body.velocity = dirToNextPoint.normalized * speed * Time.fixedDeltaTime;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Player" && !targetAcquired)
		{
			target = col.transform;
			targetAcquired = true;
			StartCoroutine (ChargingGoat ());
		}
	}
	private void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Player" && targetAcquired)
		{
			StartCoroutine (EjectingPlayer ());

		}
		if(col.gameObject.tag == "Obstacle")
		{
			StopAllCoroutines ();
			body.velocity = Vector2.zero;
			StartCoroutine (ChargingGoat ());
		}
	}

	IEnumerator ChargingGoat()
	{
		body.velocity = Vector2.zero;
		yield return new WaitForSeconds (waitTime);
		Vector3 dirToTarget = target.position - transform.position;
		body.velocity = dirToTarget.normalized * chargingSpeed * Time.fixedDeltaTime;
		yield return new WaitForSeconds (waitTime * 2);
	}

	IEnumerator EjectingPlayer()
	{
		playerBody.velocity = Vector2.zero;
		body.velocity = Vector2.zero;
		yield return new WaitForSeconds (0.005f);
		playerScript.canMove = false;
		playerScript.canJump = false;
		Vector3 dirToGoat = playerBody.transform.position - body.transform.position;
		playerBody.AddForce (dirToGoat.normalized * ejectSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
		yield return new WaitForSeconds (ejectingTime);
		targetAcquired = false;
		touchedPlayer = false;
		playerScript.canMove = true;
		playerScript.canJump = true;
	}
}
