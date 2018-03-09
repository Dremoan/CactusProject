using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterActivationRock : MonoBehaviour {

	public GameObject keyRock;
	public bool isActive = false;

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.Equals(keyRock))
		{
			isActive = true;
			keyRock.transform.position = transform.position;
			keyRock.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			keyRock.GetComponent<Rigidbody2D> ().isKinematic = true;
		}
	}
}
