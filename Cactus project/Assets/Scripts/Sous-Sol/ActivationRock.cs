using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ActivationRock : MonoBehaviour {

	public GameObject keyRock;
	public GameObject keyRockPlace;
	public bool isActive = false;
	private bool hasShake;
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
		if(coll.gameObject.Equals(keyRock) && !hasShake)
		{
			isActive = true;
			ShakeCamera ();
			keyRock.transform.position = keyRockPlace.transform.position;
			keyRock.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			keyRock.GetComponent<Rigidbody2D> ().isKinematic = true;
		}
	}


	void ShakeCamera()
	{
		CameraShaker.Instance.ShakeOnce (1f, 10f, 0.5f, 0.5f);
		hasShake = true;
	}
}
