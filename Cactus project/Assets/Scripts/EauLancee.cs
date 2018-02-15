using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EauLancee : MonoBehaviour {

	public float shootSpeed;
	public Rigidbody2D body;
	public Rigidbody2D playerBody;
	public GameObject player;
	public GameObject flowerBody;
	public GameObject waterTarget;

	[HideInInspector]public bool dispo = true;
	private float maxDist;
	private Vector2 dirToTarget;
	private Vector2 dirToEauPos;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
		dirToEauPos = waterTarget.transform.position - flowerBody.transform.position;
		Shoot ();
	}

	void Shoot()
	{
		/*if (Input.GetMouseButtonDown (1) && GetComponent<PlayerBehavior> ().isAiming == true)
		{
		dirToTarget = Camera.main.ScreenToWorldPoint (Input.mousePosition) - playerBody.transform.position;
			gameObject.SetActive (true);
			eauLancee.AddForce (dirToTarget.normalized * shootSpeed, ForceMode2D.Impulse);
		}*/
		body.velocity = transform.right * shootSpeed * Time.fixedDeltaTime;
		if(Vector3.Distance(transform.position, playerBody.transform.position) > dirToEauPos.magnitude)
		{
			DropManagerComponent.RemoveDrop (this);
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "ModifyingZone")
		{
			coll.GetComponent<ModifyingZone> ().Modified ();
			DropManagerComponent.RemoveDrop (this);
		}
	}
}
