using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour {

	public GoatInSand goatScript;
	public GameObject flower;
	public GameObject liane;
	public GameObject flowerTarget;
	private bool lianeActive;

	void Start () 
	{
		
	}


	void Update () 
	{
		if(goatScript.GetComponent<GoatInSand>().isCharging && flower.GetComponent<LaunchFlower>().hitGoat == true)
		{
			DrawLine ();
		}
		if(goatScript.GetComponent<GoatInSand>().inTheAir == true)
		{
			liane.SetActive (false);
			lianeActive = false;
		}
	}

	void DrawLine()
	{
		liane.SetActive (true);
		lianeActive = true;

		if (lianeActive) 
		{
			Vector2 dirToFlower = transform.position - flower.transform.position;
			float rot_Z = Mathf.Atan2 (dirToFlower.y, dirToFlower.x) * Mathf.Rad2Deg;
			liane.transform.rotation = Quaternion.Euler (0f, 0f, rot_Z - 90f);
			liane.GetComponent<SpriteRenderer> ().size = new Vector2 (3, Vector2.Distance (transform.position, flower.transform.position));
		}

	}
}
