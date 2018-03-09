using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

	public float speed = 100f;
	public float jumpSpeed = 10f;
	public float jumpTime = 0.5f;
	public float jumpCoolDown = 1f;
	public float actualSpeed;
	public float maxCibleDist = 20f;
	[HideInInspector] public bool canMove = true;
	[HideInInspector] public bool canJump = true;
	[HideInInspector] public bool isJumping = false;
	[HideInInspector] public bool pressingA = false;

	private bool isMoving;
	private bool holdsWater;
	[HideInInspector] public bool isAiming = false;

	public Transform player;
	public GameObject eau;
	public LaunchFlower Fleur;
	public Rigidbody2D body;
	public Vector2 move;
	public Vector3 eauPos;

	private Animator anim;
	private Vector2 mousePos;
	private Vector2 lastMove;

	void Start () 
	{
		anim = GetComponent<Animator> ();
	}

	void Update () 
	{
		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;

		if (canJump && canMove) 
		{
			Move ();
		}

			aimWater ();

		if (Input.GetKeyDown (KeyCode.A)) 
		{
			pressingA = true;
		}
		if (Input.GetKeyUp (KeyCode.A)) 
		{
			pressingA = false;
		}

		if (Input.GetKeyDown (KeyCode.Space) && canJump == true) 
		{
			isJumping = true;
			StartCoroutine (dashingDelay ());
		}

			
		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");
		anim.SetBool ("isMoving", isMoving);
		anim.SetFloat ("Horizontal", horizontal);
		anim.SetFloat ("Vertical", vertical);
		anim.SetFloat ("LastMoveX", lastMove.x);
		anim.SetFloat ("LastMoveY", lastMove.y);
	}
		
	IEnumerator dashingDelay()
	{
		canJump = false;
		body.velocity = Vector2.zero;
		body.velocity = lastMove.normalized * jumpSpeed * Time.fixedDeltaTime;
		yield return new WaitForSeconds (jumpTime);
		canJump = true;
		isJumping = false;
	}
		

	void aimWater()
	{
		if (Input.GetKey (KeyCode.A) && Fleur.holdsWater)
		{
			body.velocity = move.normalized * actualSpeed * 20 * Time.deltaTime;
			eauPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			eauPos.z = 0;
			if(Vector3.Distance(transform.position, eauPos) > maxCibleDist) 
			{
				eauPos = transform.position + ((eauPos - transform.position).normalized * maxCibleDist);
			}
			eau.transform.position = eauPos;
			eau.SetActive (true);
			if(Input.GetMouseButtonDown(1))
			{
				
				DropManagerComponent.SpawnDrop (Fleur.transform.position, Mathf.Atan2 (mousePos.y, mousePos.x) * Mathf.Rad2Deg);
				Fleur.holdsWater = false;
			}
		}
		if (Input.GetKeyUp (KeyCode.A)) 
		{
			eau.SetActive (false);
			isAiming = false;
		}
	}

	void Move()
	{
		isMoving = false;
		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");
		if(horizontal > 0.1f || horizontal < -0.1f)
		{
			isMoving = true;
			lastMove = new Vector2(horizontal, vertical);
		}
		if(vertical > 0.1f || vertical < -0.1f)
		{
			isMoving = true;
			lastMove = new Vector2(horizontal, vertical);
		}

		move = new Vector2(horizontal, vertical);
		actualSpeed = speed;
		body.velocity = move.normalized * actualSpeed;
	}


}
