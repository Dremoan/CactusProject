using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingUp : MonoBehaviour {


	public Collider2D highPlatform;
	public GameObject player;
	public GameObject flower;
	public Collider2D levelCollider;
	public float waitTime = 1f;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			StartCoroutine (ActiveCollider ());
			player.GetComponent<SpriteRenderer> ().sortingOrder += 8;
			flower.GetComponent<SpriteRenderer> ().sortingOrder += 9;

		}
	}

	IEnumerator ActiveCollider()
	{
		yield return new WaitForSeconds (waitTime);
		highPlatform.enabled = true;
		levelCollider.enabled = false;

	}
}
