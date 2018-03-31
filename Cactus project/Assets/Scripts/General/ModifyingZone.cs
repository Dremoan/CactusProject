using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ModifyingZone : MonoBehaviour 
{

	public bool modify = false;
	private bool hasShake;
	public SpriteRenderer spriteRend;
	public Animator anim;

	void Start () 
	{
		
	}

	void Update () 
	{
		if (modify == true && !hasShake)
		{
			StartCoroutine (ShakeCamera ());
			anim.SetBool ("Modify", modify);
		}
	}

	public void Modified()
	{
		modify = true;
	}

	IEnumerator ShakeCamera()
	{
			CameraShaker.Instance.ShakeOnce (2f, 5f, 0.05f, 0.05f);
			yield return new WaitForSeconds (0.25f);
			CameraShaker.Instance.ShakeOnce (4f, 5f, 0.05f, 0.15f);
			yield return new WaitForSeconds (0.1f);
			CameraShaker.Instance.ShakeOnce (2f, 1f, 0.1f, 0.1f);
			hasShake = true;
	}
}
