using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour {

	private Vector2 distanceToProp;
	private GameObject detectedObject;


	public float distCold;
	public float distHot;
	public GameObject detector;

	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log (distanceToProp.magnitude);
		distanceToProp = detectedObject.transform.position - transform.position;
		ColdHot ();
	}
		
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "HiddenObject")
		{
			detector.SetActive (true);
			detectedObject = col.gameObject;
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if(col.gameObject.tag == "HiddenObject")
		{
			detector.SetActive (false);
		}
	}

	private void ColdHot()
	{
		if (distanceToProp.magnitude <= distCold)
		{
			detector.GetComponent<SpriteRenderer> ().color = Color.red;
		}
			
		if (distanceToProp.magnitude >= distHot)
		{
			detector.GetComponent<SpriteRenderer> ().color = Color.blue;
		}

		if (distanceToProp.magnitude < distHot && distanceToProp.magnitude > distCold)
		{
			detector.GetComponent<SpriteRenderer> ().color = Color.magenta;
		}
	}
}
