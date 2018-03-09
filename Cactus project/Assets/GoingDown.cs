using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingDown : MonoBehaviour 
{
	public GameObject player;
	public GameObject goatInSand;
	public GameObject flower;
	public GameObject goingUp;
	public Collider2D highPlatform;
	public Collider2D ladderCollider;
	public Collider2D levelCollider;
	public float waitTime = 0.25f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Player" && goingUp.GetComponent<GoingUp>().isHigh && goatInSand.GetComponent<GoatInSand>().inTheAir == false) 
		{
			StartCoroutine (ActiveCollider ());
		}
	}

	IEnumerator ActiveCollider()
	{
		player.GetComponent<SpriteRenderer> ().sortingOrder -= 8;
		flower.GetComponent<SpriteRenderer> ().sortingOrder -= 9;
		yield return new WaitForSeconds (waitTime);
		goingUp.GetComponent<GoingUp>().isHigh = false;
		highPlatform.enabled = false;
		levelCollider.enabled = true;
		ladderCollider.enabled = false;
		this.GetComponent<Collider2D> ().enabled = false;
		goingUp.GetComponent<Collider2D> ().enabled = true;
	}
}
