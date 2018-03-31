using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ButtonScript : MonoBehaviour {

	public GameObject player;
	public GameObject interrupteur1;
	public GameObject interrupteur2;
	public GameObject keyRock;
	public bool isActive;

	void Start () 
	{
		
	}

	void Update ()
	{
		if(interrupteur1.GetComponent<ButtonScript>().isActive && interrupteur2.GetComponent<ButtonScript>().isActive)
		{
			keyRock.SetActive (true);
		}

		if(Input.GetMouseButtonDown(0))
		{
			StartCoroutine (Shake ());
		}
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.Equals(player))
		{
			isActive = true;
		}
	}


	IEnumerator Shake()
	{
		CameraShaker.Instance.ShakeOnce (2f, 10f, 0.2f, 0.5f);
		yield return new WaitForSeconds (0.5f);
		CameraShaker.Instance.ShakeOnce (5f, 10f, 0.2f, 0.5f);
	}
}
