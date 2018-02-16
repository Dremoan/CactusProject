using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSand : MonoBehaviour 
{
	public GameObject goat;
	public GameObject attractingZone;
	public Animator anim;
	public PointEffector2D effect;
	private Vector2 dirToCenter;
	private bool isInSand = false;

	void Start () 
	{
		
	}
	

	void Update () 
	{
		Debug.Log (dirToCenter.magnitude);
		dirToCenter = goat.transform.position - transform.position;
		anim.SetBool ("isInSand", isInSand);
		if(dirToCenter.magnitude < 20f)
		{
			effect.enabled = false;
			attractingZone.transform.position = transform.position;
		}

		if(isInSand)
		{
			goat.GetComponent<GoatBehaviour> ().isMoving = false;
			goat.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Goat")
		{
			isInSand = true;
		}
	}





}
